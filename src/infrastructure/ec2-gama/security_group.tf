resource "aws_vpc" "gama_vpc" {
  cidr_block = "172.16.0.0/16"
  enable_dns_support   = true
  enable_dns_hostnames = true

  tags = {
    Name = "gama-microservice-vpc"
  }
}

resource "aws_subnet" "gama_subnet" {
  vpc_id            = aws_vpc.gama_vpc.id
  cidr_block        = "172.16.10.0/24"
  availability_zone = "us-east-2a"

  tags = {
    Name = "gama-microservice-subnet"
  }
}

resource "aws_security_group" "gama_microservice_security_group" {
  name        = "gama-microservice-security-group"
  description = "Allow SSH and HTTP on EC2 instances"
  vpc_id      = aws_vpc.gama_vpc.id
  
  ingress {
    description = "SSH to EC2"
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks      = [aws_vpc.gama_vpc.cidr_block]
  }

  ingress {
    description = "HTTP to EC2"
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks      = [aws_vpc.gama_vpc.cidr_block]
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
  count          = 1
  route_table_id = aws_route_table.public_rt.id
  subnet_id      = aws_subnet.gama_subnet.id
}
