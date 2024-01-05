using Amazon.S3;
using Amazon.S3.Model;

public interface IS3FileServices
{
    Task<PutObjectResponse> UploadImage(IFormFile file, Guid id);
    Task<GetObjectResponse> GetImage(Guid id);
    Task<DeleteObjectResponse> DeleteImage(Guid id);
}

public class S3FileServices : IS3FileServices
{
    const string BucketName = "kash-try-1";
    private readonly IAmazonS3 _s3;

    public S3FileServices(IAmazonS3 s3)
    {
        _s3 = s3;
    }

    public async Task<DeleteObjectResponse> DeleteImage(Guid id)
    => await _s3.DeleteObjectAsync(BucketName, GetUserImageBucketKey(id));

    public async Task<GetObjectResponse> GetImage(Guid id) 
    => await _s3.GetObjectAsync(BucketName, GetUserImageBucketKey(id));

    public async Task<PutObjectResponse> UploadImage(IFormFile file, Guid id)
    {
        var request = new PutObjectRequest
        {
            BucketName = BucketName,
            Key = GetUserImageBucketKey(id),
            ContentType = file.ContentType,
            InputStream = file.OpenReadStream(),
            Metadata = {
                ["x-amz-meta-originalname"] = file.FileName,
                ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
            }
        };

        return await _s3.PutObjectAsync(request);
    }

    private string GetUserImageBucketKey(Guid id) => $"user-images/{id}";
}