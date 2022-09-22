using System.Text;
using Service1.Configuration;

namespace Service1;

public class Service2Service
{
    private readonly HttpClient _httpClient;

    // public Service2Service()
    // {
    //     
    // }
    
    // public Service2Service(HttpClient httpClient)
    // {
    //     
    // }
    
    public Service2Service(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        var httpClientSettings = configuration.GetSection("HttpClientSettings").Get<HttpClientSettings>();

        _httpClient.BaseAddress = new Uri(httpClientSettings.ServiceUrl);
    }

    public async Task<string?> GetValues()
    {
        var response = await _httpClient.GetAsync("api/values", CancellationToken.None);

        if (!response.IsSuccessStatusCode) return response.ReasonPhrase;

        var bytes = await response.Content.ReadAsByteArrayAsync();
        var str = Encoding.Default.GetString(bytes);
        return str;
    }
}