namespace GatherApp.Contracts.Dtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public SingleUserResponse User { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
