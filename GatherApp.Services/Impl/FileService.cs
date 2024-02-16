using Azure.Storage.Blobs;
using GatherApp.Contracts.Constants;
using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;
using GatherApp.Services.Extensions;
using System.Net;

namespace GatherApp.Services.Impl
{
    public class FileService : IFileService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILoggingService _loggingService;

        public FileService(BlobServiceClient blobServiceClient, ILoggingService loggingService)
        {
            _blobServiceClient = blobServiceClient;
            _loggingService = loggingService ;
        }

        public Response<bool> Upload(UploadFileRequest image, string path)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = blobContainer.GetBlobClient(path);
            try
            {
                var status = blobClient.Upload(image.ImageFile.OpenReadStream(), overwrite: true);

                if (status == null)
                {
                    return CustomResponseExtension.ResponseBadRequest<bool>(Errors.UploadImage);
                }
            }
            catch (Exception ex)
            {
                return CustomResponseExtension.ResponseInternalServerError<bool>(ex.Message);
            }

            return CustomResponseExtension.ResponseDataObject(HttpStatusCode.Created, Messages.SuccessfulImageUpload, true);
        }
    }
}
