namespace GatherApp.Contracts.Configuration
{
    public class AzureKeyVaultSettings
    {
        public string KeyVaultName { get; set; } = "";
        public string AzureTenantId { get; set; } = "";
        public string AzureClientId { get; set; } = "";
        public string AzureClientSecretId { get; set; } = "";
        public bool UseAzureKeyVault { get; set; }
    }
}
