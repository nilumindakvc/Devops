data "aws_instances" "existing_instances" {
  instance_state_names = ["running"]
  filter {
    name   = "tag:Project"
    values = [var.project_name]
  }
}

locals {
  skip_creation = length(data.aws_instances.existing_instances.ids) > 0
  existing_instance_id = length(data.aws_instances.existing_instances.ids) > 0 ? data.aws_instances.existing_instances.ids[0] : null
}

data "aws_instance" "existing_instance" {
  count       = local.skip_creation ? 1 : 0
  instance_id = local.existing_instance_id
}

output "existing_instances_count" {
  value = length(data.aws_instances.existing_instances.ids)
}

output "existing_instances_ids" {
  value = data.aws_instances.existing_instances.ids
}

output "should_skip_creation" {
  value = local.skip_creation
}

output "existing_instance_ip" {
  value = local.skip_creation ? data.aws_instance.existing_instance[0].public_ip : null
}