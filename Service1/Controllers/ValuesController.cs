using Microsoft.AspNetCore.Mvc;

namespace Service1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;
    private IHttpClientFactory _httpClientFactory;

    public ValuesController(ILogger<ValuesController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<string?> Get()
    {
        var httpClient = _httpClientFactory.CreateClient("GitHub");
        var response = await httpClient.GetAsync($"api/values", CancellationToken.None);

        if (response.IsSuccessStatusCode)
        {
            var bytes = await response.Content.ReadAsByteArrayAsync();
                    var str = System.Text.Encoding.Default.GetString(bytes);
                    return str;
        }

        return response.ReasonPhrase;
    }
}