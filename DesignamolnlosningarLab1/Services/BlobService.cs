using Azure.Storage.Blobs;
namespace DesignamolnlosningarLab1.Services
{
    public class BlobService
    {

        private readonly BlobContainerClient _container;

        public BlobService(IConfiguration config)
        {
            var connectionString = config["AzureBlobStorage:ConnectionString"];
            var containerName = config["AzureBlobStorage:ContainerName"];

            var blobServicesClinet = new BlobServiceClient(connectionString);
            _container = blobServicesClinet.GetBlobContainerClient(containerName);

        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobClient = _container.GetBlobClient(file.FileName);

            using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }
    }
}
