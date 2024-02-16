using Microsoft.AspNetCore.Http;

namespace GatherApp.Contracts.Requests
{
    public class UploadFileRequest
    {
        public IFormFile ImageFile { get; set; }
    }
}
