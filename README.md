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
