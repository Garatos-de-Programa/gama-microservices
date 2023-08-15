resource "aws_s3_bucket" "gama_bucket" {
  bucket = "gama.bucket.com.br"

  tags = {
    Name        = "Bucket for occurrences and traffic fines"
    Environment = "Production"
  }
}