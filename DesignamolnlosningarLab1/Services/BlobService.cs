using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
namespace DesignamolnlosningarLab1.Services
{
    public class BlobService
    {

        private readonly BlobContainerClient _container;

        public BlobService(IConfiguration config)
        {
            var connectionString = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Azure Blob Storage connection string  must be provided in configuration.");
            }

            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new InvalidOperationException("Azure Blob Storage container name must be provided in configuration.");
            }
            var blobServicesClinet = new BlobServiceClient(connectionString);
            _container = blobServicesClinet.GetBlobContainerClient(containerName);

            _container.CreateIfNotExists();

        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var safeFileName = Path.GetFileName(file.FileName);
            var blobName = $"{Guid.NewGuid()}_{safeFileName}";

            var blobClient = _container.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream,
                new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                },
                cancellationToken: default);

            return blobClient.Uri.ToString();
        }
    }
}
