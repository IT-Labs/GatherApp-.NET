namespace GatherApp.Contracts.Requests
{
    public class LoginUserSSORequest
    {
        public required string IdToken { get; set; } = string.Empty;
    }
}
