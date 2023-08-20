data "aws_vpc" "vpc" {
  filter {
    name = "tag:Name"
    values = [local.vpc_tag]
  }
}

data "aws_subnet_ids" "public_subnets" {
  vpc_id = data.aws_vpc.vpc.id

  filter {
    name = "tag:Name"
    values = [local.subnet_tag, local.subnet_tag_b]
  }
}

resource "aws_security_group" "gama_rds_security_group" {
  name        = "gama-db-security-group"
  description = "Security group RDS instances"
  vpc_id      = data.aws_vpc.vpc.id
  
  ingress {
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  egress {
    from_port   = 5432
    to_port     = 5432
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name = "gama-rds-security-group"
  }
}
