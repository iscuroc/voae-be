using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class StorageService : IStorageService
{
    private static IAmazonS3 s3Client;

    public static void Main(string[] args)
    {
        var accessKey = "bd6e7fc8f72d9b9b807563a0468811f7";
        var secretKey = "bc37418b65d7c926b7d52bbf15b9256bffa2e56ed0f546c0ffdfca371bcd6b49";
        var credentials = new BasicAWSCredentials(accessKey, secretKey);
        s3Client = new AmazonS3Client(credentials, new AmazonS3Config
        {
            ServiceURL = "https://6d43b1e4ee31a6ec278a30fa271507f1.r2.cloudflarestorage.com"
        });
    }

    static async Task ListObjectsV2()
    {
        var request = new ListObjectsV2Request()
        {
            BucketName = "portal-unah-copan"
        };

        var response = await s3Client.ListObjectsV2Async(request);

        foreach (var s3Object in response.S3Objects)
        {
            Console.WriteLine("{0}", s3Object.Key);
        }
    }

    public async Task<string> UploadAsync(IFormFile file, string folderName)
    {
        var request = new PutObjectRequest
        {
            FilePath = folderName + "/" + file.FileName,
            BucketName = "portal-unah-copan",
            DisablePayloadSigning = true
        };

        var response = await s3Client.PutObjectAsync(request);

        Console.WriteLine("ETag: {0}", response.ETag);

        return response.ToString();
    }
}