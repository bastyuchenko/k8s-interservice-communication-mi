using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

namespace Service1;

public class AuthenticationHandler : DelegatingHandler
{
    private readonly IAccessTokenProvider _tokenProvider;
    private readonly HttpClientSettings _settings;

    public AuthenticationHandler(IAccessTokenProvider tokenProvider, HttpClientSettings settings)
    {
        _tokenProvider = tokenProvider;
        _settings = settings;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var authToken = await _tokenProvider.GetAccessToken(_settings.Resource);

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

        return await base.SendAsync(request, cancellationToken);
    }
    
    public static DelegatingHandler AuthenticationHandlerFactory(IServiceProvider serviceProvider)
    {
        var provider = serviceProvider.GetRequiredService<IAccessTokenProvider>();
        var s = serviceProvider.GetRequiredService<IOptions<HttpClientSettings>>().Value;
        return new AuthenticationHandler(provider, s);
    }
}