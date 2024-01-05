using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Util;
using Amazon.S3.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Advanced;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ImageOptimizer;

public class Function
{
    IAmazonS3 S3Client = new AmazonS3Client();

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        var eventRecords = evnt.Records ?? new List<S3Event.S3EventNotificationRecord>();
        foreach (var record in eventRecords)
        {
            var s3Event = record.S3;
            if (s3Event == null)
            {
                continue;
            }

            try
            {
                var response = await S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);
                await using var itemStream = await S3Client.GetObjectStreamAsync(s3Event.Bucket.Name, s3Event.Object.Key, new Dictionary<string, object>());

                if (response.Metadata["x-amz-meta-resized"].Equals(true.ToString()))
                {
                    context.Logger.LogInformation($"user image {JsonSerializer.Serialize(s3Event.Object)}");
                    continue;
                }

                using var outStream = new MemoryStream();
                using (var image = await Image.LoadAsync(itemStream))
                {
                    image.Mutate(x => x.Resize(500, 500, LanczosResampler.Lanczos3));
                    var originalName = response.Metadata["x-amz-meta-originalname"];
                    await image.SaveAsync(outStream, image.DetectEncoder(originalName));
                }

                var request = new PutObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key,
                    ContentType = response.Headers.ContentType,
                    InputStream = outStream,
                    Metadata = {
                        ["x-amz-meta-originalname"] = response.Metadata["x-amz-meta-originalname"],
                        ["x-amz-meta-extension"] = response.Metadata["x-amz-meta-extension"],
                        ["x-amz-meta-resized"] = true.ToString()
                    }
                };

                await S3Client.PutObjectAsync(request);
            }
            catch (Exception e)
            {
                context.Logger.LogError($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogError(e.Message);
                context.Logger.LogError(e.StackTrace);
                throw;
            }
        }
    }
}