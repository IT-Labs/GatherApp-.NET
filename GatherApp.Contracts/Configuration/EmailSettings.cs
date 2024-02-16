namespace GatherApp.Contracts.Configuration
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public bool EnableSsl { get; set; }
    }
}
