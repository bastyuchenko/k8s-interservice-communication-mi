using Microsoft.Extensions.Options;
using Microsoft.Azure.Services.AppAuthentication;

namespace Service1
{
    public class AzureAdAccessTokenProvider : IAccessTokenProvider
    {
        private readonly AzureAd _settings;
        private readonly AzureServiceTokenProvider _client;

        public AzureAdAccessTokenProvider(IOptions<AzureAd> settings) : this(settings.Value)
        {
        }

        private AzureAdAccessTokenProvider(AzureAd settings)
        {
            _settings = settings;
            _client = CreateClient();
        }

        public async Task<string> GetAccessToken(string resource) 
            => await _client.GetAccessTokenAsync(resource);

        private AzureServiceTokenProvider CreateClient() 
            => new AzureServiceTokenProvider(GetConnectionString());

        private string? GetConnectionString()
        {
            return $"RunAs=App; AppId={_settings.ClientId}; AppKey={_settings.ClientSecret}; TenantId={_settings.TenantId}";
        }
    }
}