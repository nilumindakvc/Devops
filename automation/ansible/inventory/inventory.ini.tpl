[webservers]
ec2-instance ansible_host=${ec2_public_ip} ansible_user=ubuntu ansible_ssh_private_key_file=${ssh_key_path}

[webservers:vars]
ansible_python_interpreter=/usr/bin/python3
ansible_ssh_common_args='-o StrictHostKeyChecking=no'