namespace Service1;

public class AzureAd
{
    public string Authority { get; set; } = null!;
    public string Instance { get; set; } = null!;
    public string Domain { get; set; } = null!;
    public string TenantId { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string Scopes { get; set; } = null!;
    public string CallbackPath { get; set; } = null!;
    public string Resource { get; set; } = null!;
}