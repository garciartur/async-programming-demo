using Microsoft.AspNetCore.Mvc;

namespace SyncAppDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SyncAppController : ControllerBase
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly ILogger<SyncAppController> _logger;

        public SyncAppController(ILogger<SyncAppController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/GetOrder")]
        public string Get()
        {
            var responseTask = client.GetStringAsync($"http://localhost:9001");
            responseTask.Wait();
            var content = responseTask.Result;

            return content;
        }
    }
}