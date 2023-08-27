
output "private_pem" {
  value = tls_private_key.key.private_key_pem
  sensitive = true
}

output "instance_public_ip" {
  value = aws_instance.gama_microservice_ec2.public_ip
}

output "instance_public_dns" {
  value = aws_instance.gama_microservice_ec2.public_dns
}
