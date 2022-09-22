using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Service1.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ValuesController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Service2Service _service2;

    public ValuesController(IHttpClientFactory httpClientFactory, Service2Service service2)
    {
        _httpClientFactory = httpClientFactory;
        _service2 = service2;
    }

    [HttpGet]
    public async Task<string?> Get()
    {
        return await _service2.GetValues();
    }
}