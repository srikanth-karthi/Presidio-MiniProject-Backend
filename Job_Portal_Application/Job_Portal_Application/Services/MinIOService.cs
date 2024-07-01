using Amazon.S3.Model;
using Amazon.S3;

public class MinIOService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public MinIOService(IConfiguration configuration)
    {
        var awsOptions = new AmazonS3Config
        {
            ServiceURL = configuration["MinIO:ServiceUrl"],
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"], awsOptions);
        _bucketName = configuration["MinIO:BucketName"];
    }

    public string GetServiceUrl()
    {
        return _s3Client.Config.ServiceURL;
    }

    public string GetBucketName()
    {
        return _bucketName;
    }

    public async Task UploadFileAsync(string key, Stream fileStream)
    {
        var putRequest = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = fileStream
        };

        await _s3Client.PutObjectAsync(putRequest);
    }

    public async Task<Stream> DownloadFileAsync(string key)
    {
        var getRequest = new GetObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        var response = await _s3Client.GetObjectAsync(getRequest);
        return response.ResponseStream;
    }

    public async Task DeleteFileAsync(string key)
    {
        var deleteRequest = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = key
        };

        await _s3Client.DeleteObjectAsync(deleteRequest);
    }
}
