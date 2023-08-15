output "url" {
  value = aws_eip.eip[0].public_dns
}

output "private_pem" {
  value = tls_private_key.key.private_key_pem
  sensitive = true
}