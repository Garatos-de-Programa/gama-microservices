
data "aws_ami" "ubuntu" {
  most_recent = true

  filter {
    name   = "name"
    values = ["ubuntu/images/hvm-ssd/ubuntu-focal-20.04-amd64-server-*"]
  }

  filter {
    name   = "virtualization-type"
    values = ["hvm"]
  }

  owners = ["099720109477"] # Canonical
}

resource "aws_instance" "gama_microservice_ec2" {
  ami                           = data.aws_ami.ubuntu.id
  instance_type                 = "t2.micro"
  key_name                      = aws_key_pair.default.key_name
  user_data_base64              = filebase64("${path.module}/user-data.sh")
  vpc_security_group_ids        = [ aws_security_group.gama_microservice_security_group.id ]
  subnet_id                     = aws_subnet.gama_subnet.id
  tags = {
    Name = "gama-microservice-01"
  }
}

resource "aws_eip" "eip" {
  count            = 1
  instance         = aws_instance.gama_microservice_ec2.id
  public_ipv4_pool = "amazon"
  vpc              = true

  tags = {
    "Name" = "EIP-gama"
  }
}

resource "aws_eip_association" "eip_association" {
  count         = 1
  instance_id   = aws_instance.gama_microservice_ec2.id
  allocation_id = aws_eip.eip[count.index].id
}