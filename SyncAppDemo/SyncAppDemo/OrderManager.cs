namespace SyncAppDemo
{
    public class OrderManager
    {
        public Item[] _itemsList { get; set; }
        public Price[] _pricesList { get; set; }
        public Quantity[] _qtyList { get; set; }

        public List<Product> _productsList { get; set; }

        public OrderManager(Item[] itemsList, Price[] pricesList, Quantity[] qtyList)
        {
            _itemsList = itemsList;
            _pricesList = pricesList;
            _qtyList = qtyList;
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

                foreach (var qty in _qtyList)
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
