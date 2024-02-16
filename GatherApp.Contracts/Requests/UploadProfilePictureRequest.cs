
namespace GatherApp.Contracts.Requests
{
    public class UploadProfilePictureRequest
    {
        public string Id { get; set; }
        public UploadFileRequest? Picture { get; set; }
    }
}