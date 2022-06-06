using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace SyncAppDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/GetOrder")]
        public Order GetOrder()
        {
            OrderManager orderManager = new OrderManager();

            orderManager.GetItems();
            orderManager.GetPrices();
            orderManager.GetQuantities();


            Order orderReturn = new Order()
            {
                _products = orderManager.GetProducts(),
                _total = orderManager.GetTotal()
            };

            return orderReturn;
        }
    }
}