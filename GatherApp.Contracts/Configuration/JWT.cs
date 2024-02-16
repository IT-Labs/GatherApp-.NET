namespace GatherApp.Contracts.Configuration
{
    public class JWT
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string AccessExpiresInMinutes { get; set; }
        public string RefreshExpiresInMinutes { get; set; }
        public string SecretKey { get; set; }
    }
}
