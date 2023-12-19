provider "aws" {
  region = "us-east-1"
}

resource "aws_sqs_queue" "sqs_sample_queue" {
  name = var.queue_name
  message_retention_seconds = var.retention_period
  visibility_timeout_seconds = var.visibility_timeout
  // dead-letter queue
  redrive_policy = jsonencode({
    "deadLetterTargetArn" = aws_sqs_queue.sqs_sample_dl_queue.arn
"maxReceiveCount" = var.recieve_count
  })
}

resource "aws_sqs_queue" "sqs_sample_dl_queue" {
   name = "${var.queue_name}-dl"
  message_retention_seconds = var.retention_period
  visibility_timeout_seconds = var.visibility_timeout
}