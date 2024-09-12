using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Contracts;
using Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services;

public class StorageService(IAmazonS3 s3Client, IConfiguration configuration) : IStorageService
{
    public async Task<string> UploadAsync(IFormFile file, string folderName)
    {
        var fileName = file.FileName.Slugify(whitelistChars: ".");
        var request = new PutObjectRequest
        {
            Key = $"{folderName}/{fileName}",
            InputStream = file.OpenReadStream(),
            BucketName = "portal-unah-copan",
            DisablePayloadSigning = true
        };

        try
        {
            await s3Client.PutObjectAsync(request);
        }
        catch (Exception)
        {
            return string.Empty;
        }
        
        return $"{configuration["AWS:BucketUrl"]}/{request.Key}";
    }
}