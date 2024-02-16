namespace GatherApp.Contracts.Requests
{
    public class LoginUserRequest
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}
