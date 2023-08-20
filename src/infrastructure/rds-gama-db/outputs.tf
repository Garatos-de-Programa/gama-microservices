output "security_group_rds" {
  description = "Security group of gama RDS"
  value = aws_security_group.gama_rds_security_group.id
}

output "rds_instance_name" {
  description = "instance name of pagarme database RDS"
  value = aws_db_instance.gama_rds.id
}

output "rds_endpoint" {
  description = "rds endpoint"
  value =  aws_db_instance.gama_rds.address
}

output "rds_endpoint_b" {
  description = "rds endpoint"
  value =  aws_db_instance.gama_rds.endpoint
}

output "rds_username" {
  description = "rds username"
  value = aws_db_instance.gama_rds.username
}

output "rds_password" {
  description = "sensitive rds password"
  value = aws_db_instance.gama_rds.password
  sensitive = true
}

output "rds_port" {
  description = "port"
  value = aws_db_instance.gama_rds.port
}