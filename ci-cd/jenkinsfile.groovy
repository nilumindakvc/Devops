pipeline {
    agent any

    environment {
        FRONTEND_IMAGE = "kvcn/agents_frontend"
        BACKEND_IMAGE = "kvcn/agents_backend"
        GIT_REPO = "https://github.com/nilumindakvc/Devops.git"
        AWS_REGION = "ap-south-1"
        EC2_USER = "ubuntu"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: "${GIT_REPO}"
            }
        }

        stage('Provision Infrastructure') {
            steps {
                script {
                    sh '''
                        cd infrastructure/terraform
                        
                        terraform init
                        terraform plan -out=tfplan
                        terraform apply -auto-approve tfplan
                        
                        # Get EC2 IP for later use
                        EC2_IP=$(terraform output -raw instance_public_ip)
                        echo "EC2_IP=${EC2_IP}" > ../../ec2_info.env
                        echo "Infrastructure provisioned. EC2 IP: $EC2_IP"
                    '''
                }
            }
        }

        stage('Configure Server with Ansible') {
            steps {
                script {
                    sh '''
                        # Wait for EC2 to be ready (SSH)
                        echo "Waiting for EC2 instance to be ready..."
                        sleep 60
                        
                        # Terraform has already generated the inventory file with actual IP
                        # Run Ansible playbook
                        cd automation/ansible
                        ansible-playbook playbooks/configure-ec2.yml
                    '''
                }
            }
        }

        stage('Build Docker Images') {
            parallel {
                stage('Build Frontend') {
                    steps {
                        script {
                            sh "docker build -t ${FRONTEND_IMAGE}:latest agentFrontEnd/"
                        }
                    }
                }
                stage('Build Backend') {
                    steps {
                        script {
                            sh "docker build -t ${BACKEND_IMAGE}:latest agentBackEnd/"
                        }
                    }
                }
            }
        }

        stage('Push Docker Images') {
            steps {
                withCredentials([usernamePassword(credentialsId: 'dockerhub', usernameVariable: 'DOCKER_USER', passwordVariable: 'DOCKER_PASS')]) {
                    sh '''
                        echo "$DOCKER_PASS" | docker login -u "$DOCKER_USER" --password-stdin
                        docker push ${FRONTEND_IMAGE}:latest
                        docker push ${BACKEND_IMAGE}:latest
                    '''
                }
            }
        }

        stage('Deploy to EC2 with Docker Commands') {
            steps {
                script {
                    sh '''
                        # Load EC2 IP
                        source ec2_info.env
                        
                        # SSH to EC2 and deploy with individual Docker commands
                        ssh -i ssh-keys/my-key-pair.pem -o StrictHostKeyChecking=no ${EC2_USER}@$EC2_IP << ENDSSH
                            # Stop and remove existing containers if they exist
                            docker stop agent-backend agent-frontend || true
                            docker rm agent-backend agent-frontend || true
                            
                            # Remove existing images to ensure latest versions
                            docker rmi ${BACKEND_IMAGE}:latest ${FRONTEND_IMAGE}:latest || true
                            
                            # Pull latest images
                            docker pull ${BACKEND_IMAGE}:latest
                            docker pull ${FRONTEND_IMAGE}:latest
                            
                            # Run backend container - maps container ports 8080,8443 to host ports 8080,8443
                            echo "Starting backend container..."
                            docker run -d \\
                                --name agent-backend \\
                                -p 8080:8080 \\
                                -p 8443:8443 \\
                                -e ASPNETCORE_ENVIRONMENT=Production \\
                                -e ASPNETCORE_URLS=http://+:8080;https://+:8443 \\
                                --restart unless-stopped \\
                                ${BACKEND_IMAGE}:latest
                            
                            # Wait for backend to start
                            sleep 10
                            
                            # Run frontend container - maps container port 80 to host port 3000
                            echo "Starting frontend container..."
                            docker run -d \\
                                --name agent-frontend \\
                                -p 3000:80 \\
                                -e REACT_APP_API_BASE_URL=http://$EC2_IP:8080 \\
                                --restart unless-stopped \\
                                ${FRONTEND_IMAGE}:latest
                            
                            # Show running containers
                            docker ps
                            
                            # Show container logs (last 5 lines)
                            echo "Backend logs:"
                            docker logs --tail 5 agent-backend
                            echo "Frontend logs:"
                            docker logs --tail 5 agent-frontend
                            
                            echo "Application deployed successfully!"
                            echo "Frontend: http://$EC2_IP:3000"
                            echo "Backend API: http://$EC2_IP:8080"
                            echo "Backend HTTPS: https://$EC2_IP:8443"
ENDSSH
                    '''
                }
            }
        }
    }

    post {
        always {
            sh "docker logout"
        }
        success {
            script {
                sh '''
                    source ec2_info.env
                    echo "Deployment Successful!"
                    echo "Frontend URL: http://$EC2_IP:3000"
                   
                '''
            }
        }
        failure {
            echo "Deployment failed. Check logs for details."
        }
    }
}
