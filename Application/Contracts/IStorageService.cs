using Microsoft.AspNetCore.Http;

namespace Application.Contracts;

public interface IStorageService
{
    Task<string> UploadAsync(IFormFile file, string folderName);
}