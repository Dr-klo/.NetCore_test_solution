using System.Collections.Generic;
using System.Linq;

namespace Auth0_test_solution.BL.Domain
{
    public class Recepeit
    {
        public Recepeit(IOrder order)
        {
            this.StoreName = order.Store.Name;
            this.StoreAddress = order.Store.Address;
            this.Products = order.ShoppingList;
        }

        public string StoreName { get; }
        public string StoreAddress { get; }
        public int Total
        {
            get
            {
                return this.Products != null
                    ? this.Products.Select(x => { return x.Count * x.Price; }).Aggregate(0, (acc, x) => acc + x)
                    : 0;
            }
        }
        public IEnumerable<Product> Products { get; set; }
    }
}