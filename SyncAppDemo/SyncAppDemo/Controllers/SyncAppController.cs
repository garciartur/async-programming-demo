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
        public Order GetOrder()
        {
            //items
            //items call
            var responseItemsTask = client.GetStringAsync($"http://localhost:9001/GetItems");
            responseItemsTask.Wait();
            var itemsContent = responseItemsTask.Result;

            //items deserialization
            var deserializeItemsTask = Task.Factory.StartNew(() => JsonSerializer.Deserialize<Item[]>(itemsContent));
            deserializeItemsTask.Wait();
            var itemsList = deserializeItemsTask.Result;

            //prices
            //prices call
            var responsePricesTask = client.GetStringAsync($"http://localhost:9001/GetPrices");
            responsePricesTask.Wait();
            var pricesContent = responsePricesTask.Result;

            //prices deserialization
            var deserializePricesTask = Task.Factory.StartNew(() => JsonSerializer.Deserialize<Price[]>(pricesContent));
            deserializePricesTask.Wait();
            var pricesList = deserializePricesTask.Result;

            //quantity
            //quantity call
            var responseQtyTask = client.GetStringAsync($"http://localhost:9001/GetQtys");
            responseQtyTask.Wait();
            var qtyContent = responseQtyTask.Result;

            //quantity deserialization
            var deserializeQtyTask = Task.Factory.StartNew(() => JsonSerializer.Deserialize<Quantity[]>(qtyContent));
            deserializeQtyTask.Wait();
            var qtyList = deserializeQtyTask.Result;

            //instanciating Order return
            OrderManager orderManager = new OrderManager(itemsList, pricesList, qtyList);
            Order orderReturn = new Order()
            {
                _products = orderManager.GetProducts(),
                _total = orderManager.GetTotal()
            };

            return orderReturn;
        }
    }
}