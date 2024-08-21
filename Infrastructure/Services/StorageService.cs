using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class StorageService : IStorageService
{
    public async Task<string> UploadAsync(IFormFile file, string folderName)
    {
        return string.Empty;
    }
}