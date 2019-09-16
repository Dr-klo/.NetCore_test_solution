using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using Auth0_test.BL.Services;

namespace Auth0_test_solution.BL.Domain
{
    public class Store : IStore
    {
        public struct Errors
        {
            public const string NotFound = "Store doesn't found.";
        }
        public Store(string name, string address, int id)
        {
            this.Name = name;
            this.Address = address;
            this.Id = id;
            // stub fill order
            this.OrderList.Add(new Order(this));
        }

        public string Name { get; }
        public string Address { get; }
        public int Id { get; }

        public bool HasDelivery { get; set; }
        private IList<IOrder> OrderList = new List<IOrder>();
        private IList<Product> productList = new List<Product>
        {
            new Product("Apple", 1, 4, 5),
            new Product("Banana", 2, 20, 3),
            new Product("Pizza", 3, 5, 20),
            new Product("Ice-Cream", 4, 10, 5)
        };
        public Product[] GetProductList()
        {
            // todo get it from DAL
            return productList.ToArray();
        }

        public void AddNewOrder(IOrder order)
        {
            this.OrderList.Add(order);
        }

        public void AddOrUpdateProduct(Product product)
        {
            var existing = this.productList.FirstOrDefault(x => x.Article == product.Article);
            if (existing!=null)
            {
                existing.Update(product.Count, product.Price);
            }
            else
            {
                this.productList.Add(product);
            }
        }

        public IOrder GetOrder(int id)
        {
            return this.OrderList.FirstOrDefault(x => x.Id == id);
        }

        public PagedResult<IOrder> GetOrdersPage(int page, int size)
        {
            if (page >= 0 && size > 0)
            {
                return new PagedResult<IOrder>(OrderList.Skip(page * size).Take(size), OrderList.Count);
            }
            throw new ArgumentOutOfRangeException(PagedResult<IOrder>.Errors.InvalidPageSettings);
        }

        public Recepeit MakeOrder(IOrder order)
        {
            this.UpdateStoreProducts(order);
            if (this.HasDelivery)
            {
                // todo emit delivery service
            }

            return new Recepeit(order);
        }

        private void UpdateStoreProducts(IOrder order)
        {
            var products = this.GetProductList();
            order.ShoppingList.ForEach(x =>
            {
                var product = products.FirstOrDefault(p => p.Article == x.Article);
                if (product == null)  throw new Exception(Product.Errors.InvalidArticle);
                if ( x.Count > product.Count )  throw new Exception(Product.Errors.CountMoreThanExists);
                    // todo check that price correct.
                    product.Count -= x.Count;
            });
            // todo update DB;
        }
    }
}