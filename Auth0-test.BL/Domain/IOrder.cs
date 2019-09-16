using System.Collections.Generic;

namespace Auth0_test_solution.BL.Domain
{
    public interface IOrder
    { 
        int Id { get; }
        List<Product> ShoppingList { get; }
        IStore Store { get; }
        Order.StatusEnum Status { get; }
        void AddToList(Product product);
        void UpdateProductCount(int article, int count);
        void RemoveFromList(int article);
        void Apply();
    }
}