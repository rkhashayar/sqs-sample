using Amazon.S3;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAmazonS3, AmazonS3Client>();
builder.Services.AddTransient<IS3FileServices, S3FileServices>();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPost(
    "user/{id:guid?}/upload-image",
    async ([FromForm(Name = "Data")] IFormFile file, [FromRoute] Guid? id)
    => await app.Services.GetRequiredService<IS3FileServices>().UploadImage(file, id ?? Guid.NewGuid()))
.WithName("UploadUserImage")
.WithOpenApi()
.DisableAntiforgery();

app.MapGet(
    "user/{id:guid}/image",
    async ([FromRoute] Guid id) =>
    {
        var response = await app.Services.GetRequiredService<IS3FileServices>().GetImage(id);
        return Results.File(response.ResponseStream, response.Headers.ContentType);
    });

app.MapDelete(
    "user/{id:guid}/delete-image",
    async ([FromRoute] Guid id)
    => await app.Services.GetRequiredService<IS3FileServices>().DeleteImage(id));

app.Run();