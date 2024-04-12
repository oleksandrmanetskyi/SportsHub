using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SportsHub.Services.Services.Realizations
{
    public class BlobStorage : IBlobStorage
    {
        protected readonly BlobContainerClient container;
        public BlobStorage(IOptions<BlobStorageOptions> options)
        {
            container = new BlobContainerClient(options.Value.ConnectionString, options.Value.ContainerName);
            container.CreateIfNotExists();
        }

        protected BlobStorage(BlobContainerClient container)
        {
            this.container = container;
        }

        public async Task UploadBlobAsync(string name, string base64)
        {
            BlobClient blob = container.GetBlobClient(name);
            var encodedImage = base64.Split(',')[1];
            var bytes = Convert.FromBase64String(encodedImage);// without data:image/jpeg;base64 prefix, just base64 string
            using (var stream = new MemoryStream(bytes))
            {
                await blob.UploadAsync(stream, true);
            }
        }

        public async Task<string> DownloadBlobAsync(string name)
        {
            try
            {
                return await DownloadAsync(name);
            }
            catch (RequestFailedException)
            {
                return await DownloadAsync("default.png");
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }
        private async Task<string> DownloadAsync(string name)
        {
            BlobClient blob = container.GetBlobClient(name);
            Response<BlobDownloadInfo> download = await blob.DownloadAsync();
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                await download.Value.Content.CopyToAsync(memoryStream);
                bytes = memoryStream.ToArray();
            }

            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public async Task DeleteBlobAsync(string name)
        {
            BlobClient blob = container.GetBlobClient(name);
            await blob.DeleteIfExistsAsync();
        }

    }
}
