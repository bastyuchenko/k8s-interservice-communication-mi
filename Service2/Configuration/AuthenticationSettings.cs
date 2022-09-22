namespace Service2.Configuration;

public class AuthenticationSettings
{
    public string Authority { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public string Issuer { get; init; } = null!;
}