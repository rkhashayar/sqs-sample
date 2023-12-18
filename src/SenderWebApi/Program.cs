using System.Dynamic;
using Amazon.SQS;
using Contracts.Messages;
using Microsoft.AspNetCore.Mvc;
using SenderWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
builder.Services.AddTransient<ISqsPublisher, SqsPublisher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost("/publish", async ([FromBody] SampleMessage message) =>
{
    message.Id = Guid.NewGuid();
    var publisher = app.Services
    .GetRequiredService<ISqsPublisher>();
    await publisher.PublishAsync("comp1_queue", message);
})
.WithName("Publish")
.WithOpenApi();

app.Run();
