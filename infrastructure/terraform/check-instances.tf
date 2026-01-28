# Data source to check for existing running instances with our project tag
data "aws_instances" "existing_instances" {
  instance_state_names = ["running"]
  
  filter {
    name   = "tag:Project"
    values = [var.project_name]
  }
}

# Local value to determine if we should skip resource creation
locals {
  skip_creation = length(data.aws_instances.existing_instances.ids) > 0
  existing_instance_id = length(data.aws_instances.existing_instances.ids) > 0 ? data.aws_instances.existing_instances.ids[0] : null
}

# Data source to get details of existing instance if any
data "aws_instance" "existing_instance" {
  count       = local.skip_creation ? 1 : 0
  instance_id = local.existing_instance_id
}

# Output information about existing instances
output "existing_instances_count" {
  description = "Number of existing running instances found"
  value       = length(data.aws_instances.existing_instances.ids)
}

output "existing_instances_ids" {
  description = "IDs of existing running instances"
  value       = data.aws_instances.existing_instances.ids
}

output "should_skip_creation" {
  description = "Whether to skip infrastructure creation"
  value       = local.skip_creation
}

output "existing_instance_ip" {
  description = "Public IP of the first existing instance (if any)"
  value       = local.skip_creation ? data.aws_instance.existing_instance[0].public_ip : null
}