# Configure the AWS Provider
terraform {
  required_version = ">= 0.14"
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 5.0"
    }
  }
}

# Configure the AWS Provider region
provider "aws" {
  region = var.aws_region
}

# Create AWS key pair automatically
resource "aws_key_pair" "devops_key" {
  key_name   = var.key_pair_name
  public_key = tls_private_key.devops_key.public_key_openssh

  tags = {
    Name    = "${var.project_name}-key-pair"
    Project = var.project_name
  }
}

# Generate SSH key pair
resource "tls_private_key" "devops_key" {
  algorithm = "RSA"
  rsa_bits  = 4096
}

# Save private key to local file
resource "local_file" "private_key" {
  content         = tls_private_key.devops_key.private_key_pem
  filename        = "${path.root}/../../ssh-keys/${var.key_pair_name}.pem"
  file_permission = "0600"
  
  depends_on = [tls_private_key.devops_key]
}

# Variables
variable "aws_region" {
  description = "AWS region"
  type        = string
  default     = "eu-north-1"  # Stockholm region
}

variable "instance_type" {
  description = "EC2 instance type - t2.micro is free tier eligible with 1GB RAM"
  type        = string
  default     = "t2.micro"  # 1GB RAM - Free tier (not 2GB as required)
}

variable "key_pair_name" {
  description = "Name of the AWS key pair"
  type        = string
  default     = "my-key-pair"
}

variable "project_name" {
  description = "Name of the project for tagging"
  type        = string
  default     = "devops-final-project"
}

# Data source to get the latest Ubuntu AMI
data "aws_ami" "ubuntu" {
  most_recent = true
  owners      = ["099720109477"] # Canonical

  filter {
    name   = "name"
    values = ["ubuntu/images/hvm-ssd/ubuntu-22.04-amd64-server-*"]
  }

  filter {
    name   = "virtualization-type"
    values = ["hvm"]
  }
}

# Create a VPC
resource "aws_vpc" "main" {
  cidr_block           = "10.0.0.0/16"
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = {
    Name    = "${var.project_name}-vpc"
    Project = var.project_name
  }
}

# Create an Internet Gateway
resource "aws_internet_gateway" "main" {
  vpc_id = aws_vpc.main.id

  tags = {
    Name    = "${var.project_name}-igw"
    Project = var.project_name
  }
}

# Create a public subnet
resource "aws_subnet" "public" {
  vpc_id                  = aws_vpc.main.id
  cidr_block              = "10.0.1.0/24"
  availability_zone       = "${var.aws_region}a"  # eu-north-1a
  map_public_ip_on_launch = true

  tags = {
    Name    = "${var.project_name}-public-subnet"
    Project = var.project_name
  }
}

# Create a route table for the public subnet
resource "aws_route_table" "public" {
  vpc_id = aws_vpc.main.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.main.id
  }

  tags = {
    Name    = "${var.project_name}-public-rt"
    Project = var.project_name
  }
}

# Associate the route table with the public subnet
resource "aws_route_table_association" "public" {
  subnet_id      = aws_subnet.public.id
  route_table_id = aws_route_table.public.id
}

# Create a security group for the EC2 instance
resource "aws_security_group" "web_server" {
  name_prefix = "${var.project_name}-web-sg"
  vpc_id      = aws_vpc.main.id

  # SSH access
  ingress {
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "SSH access"
  }

  # HTTP access
  ingress {
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "HTTP web traffic"
  }

  # HTTPS access
  ingress {
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "HTTPS web traffic"
  }

  # Application ports
  ingress {
    from_port   = 3000
    to_port     = 5000
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    description = "Application ports (frontend and backend)"
  }

  # All outbound traffic
  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
    description = "All outbound traffic"
  }

  tags = {
    Name    = "${var.project_name}-web-sg"
    Project = var.project_name
  }
}

# Create EC2 instance - Ubuntu Free Tier (t2.micro)
# Only create if no existing instances are running
resource "aws_instance" "web_server" {
  count = local.skip_creation ? 0 : 1
  
  ami                     = data.aws_ami.ubuntu.id
  instance_type           = var.instance_type  # t2.micro (1GB RAM - Free tier)
  key_name                = aws_key_pair.devops_key.key_name
  vpc_security_group_ids  = [aws_security_group.web_server.id]
  subnet_id               = aws_subnet.public.id
  disable_api_termination = false

  tags = {
    Name    = "${var.project_name}-ubuntu-server"
    Project = var.project_name
    OS      = "Ubuntu 22.04 LTS"
    RAM     = "1GB"
    Tier    = "Free Tier Eligible"
  }
}

# Outputs
output "instance_id" {
  description = "ID of the EC2 instance"
  value       = local.skip_creation ? data.aws_instance.existing_instance[0].id : aws_instance.web_server[0].id
}

output "instance_public_ip" {
  description = "Public IP address of the EC2 instance"
  value       = local.skip_creation ? data.aws_instance.existing_instance[0].public_ip : aws_instance.web_server[0].public_ip
}

output "instance_public_dns" {
  description = "Public DNS name of the EC2 instance"
  value       = local.skip_creation ? data.aws_instance.existing_instance[0].public_dns : aws_instance.web_server[0].public_dns
}

output "ssh_connection_command" {
  description = "SSH command to connect to the instance"
  value       = "ssh -i ${var.key_pair_name}.pem ubuntu@${local.skip_creation ? data.aws_instance.existing_instance[0].public_ip : aws_instance.web_server[0].public_ip}"
}

# Generate Ansible inventory file automatically
resource "local_file" "ansible_inventory" {
  content = templatefile("../../automation/ansible/inventory/inventory.ini.tpl", {
    ec2_public_ip = local.skip_creation ? data.aws_instance.existing_instance[0].public_ip : aws_instance.web_server[0].public_ip
    ssh_key_path  = "${path.root}/../../ssh-keys/${var.key_pair_name}.pem"
  })
  filename = "../../automation/ansible/inventory/inventory.ini"

  depends_on = [local_file.private_key]
}

