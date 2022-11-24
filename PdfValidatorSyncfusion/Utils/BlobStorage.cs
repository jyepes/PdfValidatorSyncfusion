using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace PdfValidatorSyncfusion.Utils
{
    public class BlobStorage
    {
        private readonly BlobServiceClient serviceClient;
        private readonly BlobContainerClient containerClient;

        public BlobStorage(string connectionString, string containerName)
        {
            // Create a BlobClient representing the source blob.
            serviceClient = new BlobServiceClient(connectionString);

            // Create a BlobContainer representing the source blob.
            containerClient = serviceClient.GetBlobContainerClient(containerName);
        }
        public async Task<List<string>> GetAllDocuments(string connectionString, string containerName)
        {

            if (!await containerClient.ExistsAsync())
            {
                return new List<string>();
            }

            List<string> blobs = new();

            await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
            {
                if (Path.GetExtension(blobItem.Name).ToLower() == ".pdf")
                {
                    blobs.Add(blobItem.Name);
                }

            }
            return blobs;
        }

        public async Task<Stream> GetDocument(string connectionString, string containerName, string fileName)
        {

            if (await containerClient.ExistsAsync())
            {
                var blobClient = containerClient.GetBlobClient(fileName);
                if (blobClient.Exists())
                {
                    var content = await blobClient.DownloadContentAsync();
                    return content.Value.Content.ToStream();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new FileNotFoundException("El contenedor no existe");
            }

        }
    }
}
