using System;
using System.Collections.Generic;
using System.Linq;

namespace Auth0_test_solution.BL.Domain
{
    public class Order : IOrder
    {
        public struct Errors
        {
            public const string NotFound = "Order doesn't found";
        }
        public enum StatusEnum
        {
            Pending,
            Approved,
            Delivered,
            Done,
            Rejected,
            Cancelled
        }
        public Order(IStore store)
        {
            this.Id = new Random().Next(1, 1000);
            this.Store = store;
            this.ShoppingList = new List<Product> {};
            this.Status = StatusEnum.Pending;
            this.Store.AddNewOrder(this);
        }
        // todo make readonly collection. this collection are able to change by reference.
        public int Id { get; }
        public List<Product> ShoppingList { get; }
        public IStore Store { get; }
        public StatusEnum Status { get; protected set; }
        public void AddToList(Product product)
        {
           
            if (this.ShoppingList.Find(x => x.Article == product.Article)!= null)
            {
               this.UpdateProductCount(product.Article, product.Count);
            }
            else
            {
                this.ShoppingList.Add(product);
            }
        }

        public void UpdateProductCount(int article, int count)
        {
            var existing = this.ShoppingList.Find(x => x.Article == article);
            if (existing == null) throw new Exception(Errors.NotFound);
            existing.Count += count;
            if (existing.Count == 0) {this.ShoppingList.Remove(existing);}
        }

        public void RemoveFromList(int article)
        {
            this.ShoppingList.RemoveAll(x => x.Article == article);
        }

        public void Apply()
        {
            this.Status = StatusEnum.Approved;
            var receipt = this.Store.MakeOrder(this);
        }
    }
}