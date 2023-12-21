using Domain.User;
using Persistence.Infrastructure;
using Services.Interfaces;
using Azure.Storage.Blobs;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Domain.User;
using Persistence.Infrastructure;
using Services.Interfaces;

namespace Services.Implementation
{
    public class ImageService : IImageService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IWebHostEnvironment _webHostEnvironment;

        private const string CurrentUrlToEndpoint = "https://avatarstoredroid.blob.core.windows.net/avatars";

        public ImageService(IRepository<User> userRepository, IWebHostEnvironment environment, IWebHostEnvironment webHostEnvironment)
        {
            _userRepository = userRepository;
            _environment = environment;
            _webHostEnvironment = webHostEnvironment;
        }

        private async Task<BlobContainerClient> GetCloudBlobContainer()
        {
            BlobServiceClient serviceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=avatarstoredroid;AccountKey=FfFGbJmGL04qbow1LGgO1fvjK53umuUrTY+cR0/FzrCzwdEufhxx6IJtkgZVU5S2Eu2tcLfiMuT++AStOylkgg==;EndpointSuffix=core.windows.net");
            BlobContainerClient containerClient = serviceClient.GetBlobContainerClient("avatars");
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        public async Task<FileStream?> GetImageByUserId(Guid id)
        {

            BlobContainerClient containerClient = await GetCloudBlobContainer();

            var user = await _userRepository.GetByIdAsync(id);
            var blobReference = containerClient.GetBlobClient($"{user.Id}.jpg");

            var memoryStream = new MemoryStream();
            await blobReference.DownloadToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            var tempFilePath = Path.GetTempFileName();
            File.WriteAllBytes(tempFilePath, memoryStream.ToArray());

            var fileStream = new FileStream(tempFilePath, FileMode.Open);

            return fileStream;
        }

        public async Task SetAvatar(User user, IFormFile avatar)
        {
            if (avatar == null || avatar.Length <= 0)
            {
                throw new IOException(nameof(avatar));
            }

            BlobContainerClient containerClient = await GetCloudBlobContainer();

            var blobReference = containerClient.GetBlobClient($"{user.Id}.jpg");

            await blobReference.UploadAsync(avatar.OpenReadStream(), true);

            user.Avatar = CurrentUrlToEndpoint + user.Id.ToString();
        }

        public void ValidateImage(IFormFile avatar)
        {
        }
    }
}
