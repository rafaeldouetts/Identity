using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Identity.Blob
{
    public interface IBlobService
    {
        Task<string> Upload(IFormFile formFile);
        string GetSpecific(string blobName);
        List<string> GetMany(string stringBlobNameList);
        Task<string> GetBlobUrlWithSas(string blobName, IConfigurationRoot configuration);
    }
}