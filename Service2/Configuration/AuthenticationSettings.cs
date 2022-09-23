namespace Service2.Configuration;

public class AuthenticationSettings
{
    public string TenantId { get; init; } = null!;
    public string Audience { get; init; } = null!;
}