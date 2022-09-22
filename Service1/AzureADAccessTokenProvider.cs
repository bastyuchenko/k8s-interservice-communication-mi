using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Options;
using Service1.Configuration;

namespace Service1;

public class AzureAdAccessTokenProvider : IAccessTokenProvider
{
    private readonly AzureServiceTokenProvider _client;
    private readonly AzureAdSettings _settings;

    public AzureAdAccessTokenProvider(IOptions<AzureAdSettings> settings)
    {
        _settings = settings.Value;
        _client = CreateClient();
    }

    public async Task<string> GetAccessToken()
    {
        return await _client.GetAccessTokenAsync(_settings.Resource);
    }

    private AzureServiceTokenProvider CreateClient()
    {
        return new(GetConnectionString());
    }

    private string GetConnectionString()
    {
        return $"RunAs=App; AppId={_settings.ClientId}; AppKey={_settings.ClientSecret}; TenantId={_settings.TenantId}";
    }
}