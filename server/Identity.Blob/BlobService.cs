using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Identity.Blob
{
    public class BlobService : IBlobService
    {
        private readonly IConfigurationRoot _configuration;
        private readonly string _containerName = "fotosidentity";

        public BlobService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetBlobUrlWithSas(string blobName, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("blobstorage");
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            // Cria um objeto CloudBlobClient
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_containerName);
            var blob = container.GetBlobReference(blobName);

            var exists = await blob.ExistsAsync();
            if (!exists) return string.Empty;

            var sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1),
                Permissions = SharedAccessBlobPermissions.Read
            });

            return blob.Uri + sasToken;
        }

        public List<string> GetMany(string stringBlobNameList)
        {
            var blobNames = stringBlobNameList.Split(',').ToList();
            var blobUrls = new List<string>();

            foreach (var blobName in blobNames)
            {
                var blobUrl = GetBlobUrlWithSas(blobName, _configuration).Result;
                if (!string.IsNullOrEmpty(blobUrl))
                {
                    blobUrls.Add(blobUrl);
                }
            }

            return blobUrls;
        }

        public string GetSpecific(string blobName)
        {
            var blobUrl = GetBlobUrlWithSas(blobName, _configuration).Result;
            return blobUrl;
        }

        public async Task<string> Upload(IFormFile formFile)
        {
            var connectionString = _configuration.GetConnectionString("blobstorage");
            var storageAccount = CloudStorageAccount.Parse(connectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(_containerName);
            await container.CreateIfNotExistsAsync();

            var blobName = Guid.NewGuid().ToString(); // Gera um ID único
            var blob = container.GetBlockBlobReference(blobName);

            using var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();

            await blob.UploadFromByteArrayAsync(fileBytes, 0, fileBytes.Length);

            // Gera o SAS Token para o blob
            var sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1), // Expira em 1 hora
                Permissions = SharedAccessBlobPermissions.Read // Apenas leitura
            });

            // Retorna a URL completa com o SAS Token
            var blobUrlWithSas = blob.Uri + sasToken + formFile.ContentType;
            return blobUrlWithSas;
        }
    }

}
