
data "aws_ami" "amazon_linux_2" {
  most_recent = true

  filter {
    name   = "owner-alias"
    values = ["amazon"]
  }

  filter {
   name   = "name"
   values = ["amzn2-ami-hvm*"]
 }

  owners = ["amazon"] 
}

resource "aws_instance" "gama_microservice_ec2" {
  ami                           = data.aws_ami.amazon_linux_2.id
  instance_type                 = "t2.micro"
  key_name                      = aws_key_pair.default.key_name
  user_data                     = file("./user-data.sh")
  vpc_security_group_ids        = [ aws_security_group.gama_microservice_security_group.id ]
  subnet_id                     = aws_subnet.gama_subnet.id
  iam_instance_profile          = aws_iam_instance_profile.ecr_access_profile.name
  tags = {
    Name = "gama-microservice-01"
  }
}
