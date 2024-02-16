namespace GatherApp.Contracts.Dtos
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
