namespace GatherApp.Contracts.Requests
{
    public class ResetPasswordRequest : IRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}