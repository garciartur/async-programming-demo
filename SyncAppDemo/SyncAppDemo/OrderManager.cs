using System.Text.Json;

namespace SyncAppDemo
{
    public class OrderManager
    {
        public List<Item> _itemsList { get; set; }
        public List<Price> _pricesList { get; set; }
        public List<Quantity> _quantitiesList { get; set; }
        public List<Product> _productsList { get; set; }
        private static readonly HttpClient _client = new HttpClient();

        public void GetItems()
        {
            var responseItemsTask = _client.GetStringAsync($"http://localhost:9001/GetItems");
            responseItemsTask.Wait();
            var itemsContent = responseItemsTask.Result;

            var itemsList = JsonSerializer.Deserialize<List<Item>>(itemsContent);

            _itemsList = itemsList;
        }

        public void GetPrices()
        {
            var responsePricesTask = _client.GetStringAsync($"http://localhost:9001/GetPrices");
            responsePricesTask.Wait();
            var pricesContent = responsePricesTask.Result;

            var pricesList = JsonSerializer.Deserialize<List<Price>>(pricesContent);

            _pricesList = pricesList;
        }

        public void GetQuantities()
        {
            var responseQtyTask = _client.GetStringAsync($"http://localhost:9001/GetQuantities");
            responseQtyTask.Wait();
            var qtyContent = responseQtyTask.Result;

            var quantitiesList = JsonSerializer.Deserialize<List<Quantity>>(qtyContent);
            _quantitiesList = quantitiesList;
        }

        public List<Product> GetProducts()
        {
            List<Product> productsList = new List<Product>();
            foreach (var item in _itemsList)
            {                
                Product product = new Product();
                product.id = item.id;
                product.item = item.value;

                foreach (var price in _pricesList)
                {
                    if (price.id == product.id)
                    {
                        product.price = price.value;
                    }
                }

                foreach (var qty in _quantitiesList)
                {
                    if (qty.id == product.id)
                    {
                        product.qty = qty.value;
                    }
                }


                productsList.Add(product);
            }

            _productsList = productsList;

            return productsList;
        }

        public string GetTotal()
        {
            float totalPrice = 0;

            foreach(Product product in _productsList)
            {
                totalPrice += (product.qty * product.price);
            }

            return totalPrice.ToString("0.00");
        }
    }
}
