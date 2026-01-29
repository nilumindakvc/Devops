pipeline {
    agent any

    environment {
        FRONTEND_IMAGE = "kvcn/agents_frontend"
        BACKEND_IMAGE = "kvcn/agents_backend"
        GIT_REPO = "https://github.com/nilumindakvc/Devops.git"
        AWS_REGION = "eu-north-1"
        EC2_USER = "ubuntu"
        PROJECT_NAME = "devops-final-project"
        SKIP_INFRASTRUCTURE = "false"
        SKIP_ANSIBLE = "false"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git branch: 'main', url: "${GIT_REPO}"
            }
        }

        stage('Check Existing Infrastructure') {
            steps {
                script {
                    sh '''
                        echo "Checking for existing EC2 instances..."
                        
                        # Initialize default values
                        echo "SKIP_INFRASTRUCTURE=false" > infrastructure_check.env
                        echo "SKIP_ANSIBLE=false" >> infrastructure_check.env
                        
                        # Check if AWS credentials are available
                        if ! aws sts get-caller-identity > /dev/null 2>&1; then
                            echo "⚠️  WARNING: AWS credentials not configured in Jenkins!"
                            echo ""
                            echo "AWS CLI authentication failed. To fix this:"
                            echo "1. Configure AWS credentials in Jenkins:"
                            echo "   - Go to Jenkins → Manage Jenkins → Credentials"
                            echo "   - Add AWS Access Key ID and Secret Access Key"
                            echo "   - Or configure IAM role for Jenkins EC2 instance"
                            echo "2. Or install AWS CLI plugin in Jenkins"
                            echo ""
                            echo "🚀 For now, proceeding with infrastructure creation..."
                            echo "💡 TIP: Configure AWS credentials to enable infrastructure detection"
                            echo ""
                            cat infrastructure_check.env
                            exit 0
                        fi
                        
                        echo "✅ AWS credentials verified. Checking for existing instances..."
                        
                        # Check if there are running EC2 instances with our project tag
                        RUNNING_INSTANCES=$(aws ec2 describe-instances \
                            --region ${AWS_REGION} \
                            --filters "Name=instance-state-name,Values=running" "Name=tag:Project,Values=${PROJECT_NAME}" \
                            --query 'Reservations[*].Instances[*].InstanceId' \
                            --output text 2>/dev/null || echo "")
                        
                        if [ ! -z "$RUNNING_INSTANCES" ] && [ "$RUNNING_INSTANCES" != "" ] && [ "$RUNNING_INSTANCES" != "None" ]; then
                            echo "Found running EC2 instances: $RUNNING_INSTANCES"
                            echo "SKIP_INFRASTRUCTURE=true" > infrastructure_check.env
                            
                            # Get the IP of the first running instance for deployment
                            EXISTING_IP=$(aws ec2 describe-instances \
                                --region ${AWS_REGION} \
                                --instance-ids $(echo $RUNNING_INSTANCES | awk '{print $1}') \
                                --query 'Reservations[*].Instances[*].PublicIpAddress' \
                                --output text 2>/dev/null || echo "")
                            
                            if [ ! -z "$EXISTING_IP" ] && [ "$EXISTING_IP" != "" ] && [ "$EXISTING_IP" != "None" ]; then
                                echo "EC2_IP=$EXISTING_IP" > ec2_info.env
                                echo "EC2_IP=$EXISTING_IP" >> infrastructure_check.env
                                echo "✅ Using existing EC2 instance with IP: $EXISTING_IP"
                                
                                # Check if Docker is installed on the existing instance
                                echo "🔍 Checking if Docker is installed on existing instance..."
                                if ssh -i ssh-keys/my-key-pair.pem -o StrictHostKeyChecking=no -o ConnectTimeout=10 ${EC2_USER}@$EXISTING_IP "docker --version" >/dev/null 2>&1; then
                                    echo "✅ Docker is installed. Skipping Ansible configuration."
                                    echo "SKIP_ANSIBLE=true" >> infrastructure_check.env
                                    echo "⏭️  Infrastructure creation and Ansible configuration will be skipped."
                                else
                                    echo "❌ Docker not found. Ansible configuration will run to install Docker."
                                    echo "SKIP_ANSIBLE=false" >> infrastructure_check.env
                                    echo "⏭️  Infrastructure creation skipped, but Ansible will configure the server."
                                fi
                            else
                                echo "⚠️  Could not retrieve IP for existing instance. Will create new infrastructure."
                                echo "SKIP_INFRASTRUCTURE=false" > infrastructure_check.env
                                echo "SKIP_ANSIBLE=false" >> infrastructure_check.env
                            fi
                        else
                            echo "No running EC2 instances found. Infrastructure will be created."
                            echo "SKIP_INFRASTRUCTURE=false" > infrastructure_check.env
                            echo "SKIP_ANSIBLE=false" >> infrastructure_check.env
                        fi
                        
                        echo ""
                        echo "=== Infrastructure Check Results ==="
                        cat infrastructure_check.env
                        echo "==================================="
                    '''
                }
            }
        }

        stage('Provision Infrastructure') {
            when {
                expression {
                    def skipInfra = sh(returnStdout: true, script: 'grep "SKIP_INFRASTRUCTURE=true" infrastructure_check.env || echo "not_found"').trim()
                    return skipInfra == 'not_found'
                }
            }
            steps {
                script {
                    sh '''
                        echo "Creating new infrastructure..."
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
            when {
                expression {
                    def skipAnsible = sh(returnStdout: true, script: 'tail -n 1 infrastructure_check.env | grep "SKIP_ANSIBLE=true" || echo "not_found"').trim()
                    return skipAnsible == 'not_found'
                }
            }
            steps {
                script {
                    sh '''
                        echo "Installing Ansible..."
                        # Install Ansible if not already installed
                        if ! command -v ansible-playbook &> /dev/null; then
                            echo "Ansible not found. Installing via pip with --break-system-packages..."
                            
                            # Install pip3 if not available
                            if ! command -v pip3 &> /dev/null; then
                                echo "Installing pip3..."
                                wget -q https://bootstrap.pypa.io/get-pip.py
                                python3 get-pip.py --user --break-system-packages
                                export PATH="$HOME/.local/bin:$PATH"
                            fi
                            
                            # Install Ansible using pip3
                            $HOME/.local/bin/pip3 install --break-system-packages ansible || pip3 install --break-system-packages ansible
                            
                            # Add local bin to PATH
                            export PATH="$HOME/.local/bin:$PATH"
                        else
                            echo "Ansible is already installed"
                            ansible --version
                        fi
                        
                        echo "Configuring server with Ansible..."
                        # Wait for EC2 to be ready (SSH)
                        echo "Waiting for EC2 instance to be ready..."
                        sleep 60
                        
                        # Create inventory file from template since Terraform was skipped
                        echo "Creating inventory file from template..."
                        cd automation/ansible
                        EC2_IP=$(grep 'EC2_IP=' ../../infrastructure_check.env | cut -d'=' -f2)
                        sed "s/\\\${ec2_public_ip}/\$EC2_IP/g; s|\\\${ssh_key_path}|../../ssh-keys/my-key-pair.pem|g" inventory/inventory.ini.tpl > inventory/inventory.ini
                        
                        echo "Generated inventory file:"
                        cat inventory/inventory.ini
                        
                        # Run Ansible playbook
                        export PATH="$HOME/.local/bin:$PATH"
                        ansible-playbook playbooks/configure-ec2.yml
                    '''
                }
            }
        }

        stage('Infrastructure Status') {
            steps {
                script {
                    sh '''
                        if [ -f infrastructure_check.env ]; then
                            . ./infrastructure_check.env
                            if [ -f ec2_info.env ]; then
                                . ./ec2_info.env
                            fi
                            
                            echo "=== INFRASTRUCTURE STATUS ==="
                            if [ "$SKIP_INFRASTRUCTURE" = "true" ]; then
                                echo "✅ Used existing EC2 instance: $EC2_IP"
                                echo "⏭️  Skipped infrastructure creation"
                            else
                                echo "🆕 Created new infrastructure with EC2: $EC2_IP"
                            fi
                            
                            if [ "$SKIP_ANSIBLE" = "true" ]; then
                                echo "⏭️  Skipped Ansible configuration (using existing setup)"
                            else
                                echo "⚙️  Configured server with Ansible"
                            fi
                            echo "=============================="
                        else
                            echo "❌ infrastructure_check.env file not found"
                        fi
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
                        . ./ec2_info.env
                        
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
                            
                            # Wait for any remaining processes to clean up
                            sleep 5
                            
                            # Run backend container - maps container port 8080 to host port 8080
                            echo "Starting backend container..."
                            docker run -d \\
                                --name agent-backend \\
                                -p 8080:8080 \\
                                -e ASPNETCORE_ENVIRONMENT=Development \\
                                -e "ASPNETCORE_URLS=http://+:8080" \\
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
                            
                            # Test backend connectivity
                            echo "Testing backend connectivity..."
                            sleep 5
                            curl -f http://localhost:8080/swagger/index.html || echo "Swagger not accessible"
                            curl -f http://localhost:8080/api/Agency || echo "API not accessible"
                            
                            # Show container logs (last 10 lines)
                            echo "Backend logs:"
                            docker logs --tail 10 agent-backend
                            echo "Frontend logs:"
                            docker logs --tail 5 agent-frontend
                            
                            echo "Application deployed successfully!"
                            echo "Frontend: http://$EC2_IP:3000"
                            echo "Backend API: http://$EC2_IP:8080"
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
                    . ./ec2_info.env
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
