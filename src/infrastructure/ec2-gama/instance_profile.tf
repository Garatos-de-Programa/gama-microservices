
resource "aws_iam_role" "ecr_access_role" {
  name = "ECRAccessRole"
  assume_role_policy = jsonencode({
    Version = "2012-10-17",
    Statement = [{
      Action = "sts:AssumeRole",
      Effect = "Allow",
      Principal = {
        Service = "ec2.amazonaws.com"
      }
    }]
  })
}

resource "aws_iam_policy" "s3_access_policy" {
  name = "S3AccessPolicy"

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Action = [
          "s3:GetObject",
          "s3:PutObject",
        ],
        Effect   = "Allow",
        Resource = "arn:aws:s3:::gama.bucket.com.br/*"  # Replace with your S3 bucket ARN
      }
    ]
  })
}

resource "aws_iam_policy" "ecr_access_policy" {
  name = "ECRAccessPolicy"

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Action = [
          "ecr:GetAuthorizationToken",
          "ecr:BatchCheckLayerAvailability",
          "ecr:GetDownloadUrlForLayer",
          "ecr:BatchGetImage"
        ],
        Effect   = "Allow",
        Resource = "*"
      }
    ]
  })
}

resource "aws_iam_policy" "sqs_policy" {
  name = "SQSPolicy"

  policy = jsonencode({
    Version = "2012-10-17",
    Statement = [
      {
        Effect   = "Allow",
        Action   = [
          "sqs:SendMessage",
          "sqs:ReceiveMessage",
          "sqs:DeleteMessage",
          "sqs:GetQueueAttributes"
        ],
        Resource = "*"
      }
    ]
  })
}

resource "aws_iam_role_policy_attachment" "s3_access_attachment" {
  policy_arn = aws_iam_policy.s3_access_policy.arn
  role       = aws_iam_role.ecr_access_role.name
}

resource "aws_iam_role_policy_attachment" "ecr_access_attachment" {
  policy_arn = aws_iam_policy.ecr_access_policy.arn
  role       = aws_iam_role.ecr_access_role.name
}

resource "aws_iam_role_policy_attachment" "sqs_access_attachment" {
  policy_arn = aws_iam_policy.sqs_policy.arn
  role       = aws_iam_role.ecr_access_role.name
}

resource "aws_iam_instance_profile" "ecr_access_profile" {
  name = "ECRAccessProfile"
  role = aws_iam_role.ecr_access_role.name
}