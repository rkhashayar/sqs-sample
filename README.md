# Resources
[AWS Services](https://youtu.be/FDEpdNdFglI?si=jiF7XpqQoxbzWe4J)
# DNC
## AWS
### Route 53 
### API Gateway 
# Load Balancer
## AWS
### Elastic Load Balancer
    Application Load Balancer L7 Layer
    Network Load Balancer L4 Layer
# Compute
## AWS
### EC2 (Elastic Compute Cloud)
### Lambda
### ECS (Elastic Container Service)
### EKS (Elastic Kubernetis Service)
# User Pools
## AWS
### Cognito
# Databases
## AWS
### Elastic Cache
#### Memcache
#### Redis
### Relational
#### Aurora
#### RDS (Relational Database Service)
### No SQL
#### DynamoDB
#### DocumentDB
#### Open Search
# Packaged Services
## AWS
### Elastic Beanstalk
### App Runner
### Lightsail
### AppSync
### CloudFront
# Deployment
## AWS
### Code Commit
### Code Build
### Code Deploy
### Code Pipeline
# Monitoring
## AWS
### CloudWatch
### CloudTrail
# Access Management
## Aws
### IAM (Identity and Access Management)
# Infrastructure in Code
## AWS
### CloudFormation
### CDK (Cloud Development Kit)
# Rapid Development
## AWS
### Amplify
### SAM (Serverless Application Model)
# Event Coordination
## AWS
### SNS (Simple Notification Service)
### SQS (Simple Queue Service)
### EventBridge
### Step Function
# Object Storage
## AWS
### S3 (Simple Storage Service)
# Analytical Processing
## AWS
### EMR (Elastic MapReduce)
### Athena
# Data Warehouse
## AWS
### Redshift
# Dashboarding
## AWS
### QuickSight
# Network Boundary
## AWS
### VPC (Virtual Private Cloud)
# AI
## AWS
### Amazon Bedrock
https://youtu.be/ab1mbj0acDo?si=2Qjdmm7tr1b0MQHC






# sqs-sample
## resources
[youtube](https://www.youtube.com/watch?v=3tua8d9_t5k&list=WL&index=5&t=1671s)
## dev box specs
OS: windows
## tools
[chocolatey](https://chocolatey.org/install) to install packages
install aws cli `choco install awscli`
install dotnet `choco install dotnet`
add terraform files
cd terraform
`terraform init`
`terraform plan`
`terraform apply`
create projects
`dotnet add package AWSSDK.SQS --version 3.7.300.17`
`dotnet add .\SenderWebApi\SenderWebApi.csproj reference .\Contracts\Contracts.csproj`
`dotnet add .\ReceiverWebApi\ReceiverWebApi.csproj reference .\Contracts\Contracts.csproj`
`dotnet new sln -n SqsSample`
`dotnet sln .\SqsSample.sln  add  .\src\ReceiverWebApi\ReceiverWebApi.csproj .\src\SenderWebApi\SenderWebApi.csproj .\src\Contracts\Contracts.csproj`
