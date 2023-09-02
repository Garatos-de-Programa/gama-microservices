terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 3.0"
    }
  }
}

provider "aws" {
    region = "us-east-2"
}

resource "aws_sqs_queue" "occurrences_queue" {
  name                          = "gama-api-occurrences.fifo"
  fifo_queue                    = true
  content_based_deduplication   = true

  tags = {
    "Name" = "Gama-API-Occurrences-queue"
  }
}

output "queue_url" {
  description = "URL SQS queue"
  value       = try(aws_sqs_queue.occurrences_queue.url, null)
}

output "queue_name" {
  description = "The name of the SQS queue"
  value       = try(aws_sqs_queue.occurrences_queue.name, null)
}