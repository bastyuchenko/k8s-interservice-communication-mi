namespace Service1.Configuration;

public class AzureAdSettings
{
    public string Resource { get; init; } = null!;
    public string TenantId { get; init; } = null!;
    public string ClientId { get; init; } = null!;
    public string ClientSecret { get; init; } = null!;
}