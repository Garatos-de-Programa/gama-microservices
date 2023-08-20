resource "random_password" "password" {
  length = 10
  special = false
  numeric = true
  upper = true
}

resource "aws_db_subnet_group" "rds_subnet_group" {
  name = "gama_rds_subnet_group"
  subnet_ids = data.aws_subnet_ids.public_subnets.ids
}

resource "aws_db_instance" "gama_rds" {
  identifier = local.identifier
  allocated_storage = local.context.prd.allocated_storage
  engine = "postgres"
  engine_version = "15.3"
  instance_class = local.context.prd.instance_class
  name = "gama"
  username = local.context.prd.db_name
  password = random_password.password.result
  skip_final_snapshot = true
  apply_immediately = true
  storage_type = local.context.prd.storage_type
  vpc_security_group_ids = [aws_security_group.gama_rds_security_group.id]
  auto_minor_version_upgrade = true
  tags = {
    auto-start = "true"
  }

  multi_az = false
  iam_database_authentication_enabled = true
  storage_encrypted = true
  db_subnet_group_name = aws_db_subnet_group.rds_subnet_group.id
  publicly_accessible = true
}