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
        public string Get([FromHeader] string id)
        {
            var responseTask = client.GetStringAsync($"https://api.agify.io?name={id}");
            responseTask.Wait();
            var content = responseTask.Result;

            return content;
        }
    }
}