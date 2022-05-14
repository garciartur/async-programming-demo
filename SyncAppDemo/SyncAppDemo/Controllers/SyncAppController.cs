using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


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
        public Product Get()
        {
            var responseProductTask = client.GetStringAsync($"http://localhost:9001/GETProducts");
            responseProductTask.Wait();
            var productsContent = responseProductTask.Result;

            //var productList = JsonSerializer.Deserialize<Product[]>(productsContent);
            var productTask = Task.Factory.StartNew(() => JsonSerializer.Deserialize<Product[]>(productsContent));
            productTask.Wait();
            var productList = productTask.Result; 

            return productList[0];
        }
    }
}