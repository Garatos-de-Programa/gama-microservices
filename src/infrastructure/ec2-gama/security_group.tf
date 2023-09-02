resource "aws_vpc" "gama_vpc" {
  cidr_block = "10.0.0.0/16"
  enable_dns_support   = true
  enable_dns_hostnames = true

  tags = {
    Name = "gama-microservice-vpc"
  }
}

resource "aws_subnet" "gama_subnet" {
  vpc_id            = aws_vpc.gama_vpc.id
  cidr_block        = "10.0.1.0/24"
  availability_zone = "us-east-2a"
  map_public_ip_on_launch = true
  
  tags = {
    Name = "gama-microservice-subnet"
  }
}

resource "aws_subnet" "gama_subnet_b" {
  vpc_id            = aws_vpc.gama_vpc.id
  cidr_block        = "10.0.2.0/24"
  availability_zone = "us-east-2b"
  map_public_ip_on_launch = true

  tags = {
    Name = "gama-microservice-subnet-b"
  }
}

resource "aws_security_group" "gama_microservice_security_group" {
  name        = "gama_microservice_security_group"
  description = "Allow SSH and HTTP on EC2 instances"
  vpc_id      = aws_vpc.gama_vpc.id
  
  ingress {
    description = "SSH to EC2"
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "HTTP to EC2"
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "SignalR to EC2"
    from_port   = 8080
    to_port     = 8080
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "Rule to allow access to gama sqs"
    from_port   = 0
    to_port     = 65535
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "Rule to socket"
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name = "gama-microservice-security-group"
  }
}

data "aws_security_group" "gama_rds_sg" {
  filter {
    name = "tag:Name"
    values = ["gama-rds-security-group"]
  }
}

resource "aws_security_group_rule" "gama_rds_rule" {
  description                       = "Rule to allow access to gama db"
  type                              = "ingress"
  from_port                         = "5432"
  to_port                           = "5432"
  protocol                          = "tcp"
  security_group_id                 = aws_security_group.gama_microservice_security_group.id
  source_security_group_id          = data.aws_security_group.gama_rds_sg.id
}

resource "aws_internet_gateway" "igw" {
  vpc_id = aws_vpc.gama_vpc.id

  tags = {
    "Name" = "Internet-Gateway"
  }
}

resource "aws_route_table" "public_rt" {
  vpc_id = aws_vpc.gama_vpc.id

  tags = {
    "Name" = "Public-RouteTable"
  }
}

resource "aws_route" "public_route" {
  route_table_id         = aws_route_table.public_rt.id
  destination_cidr_block = "0.0.0.0/0"
  gateway_id             = aws_internet_gateway.igw.id
}

resource "aws_route_table_association" "public_rt_association" {
  count          = 2
  route_table_id = aws_route_table.public_rt.id
  subnet_id      = count.index == 0 ? aws_subnet.gama_subnet.id : aws_subnet.gama_subnet_b.id
}
