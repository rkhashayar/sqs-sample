provider "aws" {
  region = "us-west-2"
}

resource "aws_s3_bucket" "smaple_bucket" {
    bucket = "${var.bucket_name}" 
}