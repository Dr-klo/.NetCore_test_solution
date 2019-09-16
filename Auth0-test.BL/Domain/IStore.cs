using System.Collections.Generic;
using Auth0_test.BL.Services;

namespace Auth0_test_solution.BL.Domain
{
    public interface IStore
    {
        string Name { get; }
        string Address { get; }
        int Id { get; }
        bool HasDelivery { get; set; }
        Product[] GetProductList();
        Recepeit MakeOrder(IOrder order);
        void AddNewOrder(IOrder order);
        void AddOrUpdateProduct(Product product);
        IOrder GetOrder(int id);
        PagedResult<IOrder> GetOrdersPage(int page, int size);
    }
}