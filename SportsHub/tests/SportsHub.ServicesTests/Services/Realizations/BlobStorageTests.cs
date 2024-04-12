using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using SportsHub.Services.DTO;
using SportsHub.Services.Services.Realizations;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SportsHub.ServicesTests.Services.Realizations
{
    [TestFixture]
    public class BlobStorageTests
    {
        private Mock<IOptions<BlobStorageOptions>> _mockOptions;
        private ExposedBlobStorage _blobStorage;
        private Mock<BlobContainerClient> _mockBlobContainerClient;
        private Mock<BlobClient> _mockBlobClient;

        [SetUp]
        public void SetUp()
        {
            _mockOptions = new Mock<IOptions<BlobStorageOptions>>();
            _mockOptions.SetupGet(x => x.Value).Returns(new BlobStorageOptions
            {
                ConnectionString = "your-connection-string",
                ContainerName = "your-container-name"
            });

            _mockBlobContainerClient = new Mock<BlobContainerClient>();
            _mockBlobClient = new Mock<BlobClient>();

            _blobStorage = new ExposedBlobStorage(_mockBlobContainerClient.Object);
        }

        [Test]
        public async Task UploadBlobAsync_ValidInput_SuccessfullyUploaded()
        {
            // Arrange
            var name = "test.jpg";
            var base64 = "base64-encoded-image-data,data";

            _mockBlobContainerClient.Setup(x => x.GetBlobClient(name)).Returns(_mockBlobClient.Object);
            _mockBlobClient.Setup(x => x.UploadAsync(It.IsAny<Stream>(), true, default)).Verifiable();

            // Act
            await _blobStorage.UploadBlobAsync(name, base64);

            // Assert
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(name), Times.Once);
            _mockBlobClient.Verify(x => x.UploadAsync(It.IsAny<Stream>(), true, default), Times.Once);
        }

        [Test]
        public async Task DownloadBlobAsync_BlobExists_ReturnsBase64EncodedImage()
        {
            // Arrange
            var name = "test.jpg";
            var bytes = new byte[] { 0x1, 0x2, 0x3 }; // Example byte array
            var base64 = Convert.ToBase64String(bytes);

            _mockBlobContainerClient.Setup(x => x.GetBlobClient(name)).Returns(_mockBlobClient.Object);
            _mockBlobClient.Setup(x => x.DownloadAsync()).ReturnsAsync(ReturnBlob(bytes));

            // Act
            var result = await _blobStorage.DownloadBlobAsync(name);

            // Assert
            Assert.That(result, Is.EqualTo(base64));
        }

        [Test]
        public async Task DownloadBlobAsync_BlobDoesNotExist_ReturnsDefaultBlob()
        {
            // Arrange
            var name = "non-existent-blob.jpg";
            var defaultBlobBytes = new byte[] { 0x4, 0x5, 0x6 }; // Example byte array for default blob
            var defaultBlobBase64 = Convert.ToBase64String(defaultBlobBytes);

            _mockBlobContainerClient.Setup(x => x.GetBlobClient(name)).Returns(_mockBlobClient.Object);
            _mockBlobContainerClient.Setup(x => x.GetBlobClient("default.png")).Returns(_mockBlobClient.Object);
            _mockBlobClient.SetupSequence(x => x.DownloadAsync())
                .ThrowsAsync(new RequestFailedException(404, "Blob not found"))
                .ReturnsAsync(ReturnBlob(defaultBlobBytes));

            // Act
            var result = await _blobStorage.DownloadBlobAsync(name);

            // Assert
            Assert.That(result, Is.EqualTo(defaultBlobBase64));
        }

        [Test]
        public async Task DeleteBlobAsync_ValidInput_SuccessfullyDeleted()
        {
            // Arrange
            var name = "test.jpg";

            _mockBlobContainerClient.Setup(x => x.GetBlobClient(name)).Returns(_mockBlobClient.Object);
            _mockBlobClient.Setup(x => x.DeleteIfExistsAsync(It.IsAny<DeleteSnapshotsOption>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>())).Verifiable();

            // Act
            await _blobStorage.DeleteBlobAsync(name);

            // Assert
            _mockBlobContainerClient.Verify(x => x.GetBlobClient(name), Times.Once);
            _mockBlobClient.Verify(x => x.DeleteIfExistsAsync(It.IsAny<DeleteSnapshotsOption>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        private static Func<Response<BlobDownloadInfo>> ReturnBlob(byte[] blobContent) => () =>
        {
            var response = new Mock<Response<BlobDownloadInfo>>();

            response.SetupGet(s => s.Value)
                .Returns(BlobsModelFactory.BlobDownloadInfo(content: new MemoryStream(blobContent)));

            return response.Object;
        };
    }

    internal class ExposedBlobStorage : BlobStorage
    { 
        public ExposedBlobStorage(BlobContainerClient container) : base(container)
        { }
    }
}
