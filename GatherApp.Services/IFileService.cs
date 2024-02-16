using GatherApp.Contracts.Requests;
using GatherApp.Contracts.Responses;

namespace GatherApp.Services
{
    public interface IFileService
    {
        /// <summary>
        /// Uploads an image file to the specified path.
        /// </summary>
        /// <param name="image">The request containing the image file to be uploaded.</param>
        /// <param name="path">The destination path where the image file should be uploaded.</param>
        /// <returns>A <see cref="Response{T}"/> containing the result of the operation, with a <see cref="bool"/> indicating whether the upload was successful.</returns>
        Response<bool> Upload(UploadFileRequest image, string path);
    }
}
