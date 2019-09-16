using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using Auth0_test.BL.Services;
using Auth0_test_solution.BL.Domain;
using Auth0_test_solution.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Auth0_test_solution.Controllers
{
    [Route("api/stores/{storeId}/orders")]   
    [ApiController]
    public class OrdersController: ControllerBase
    {
        public OrdersController(IStoreManagment storeManagment)
        {
            this._storeManagment = storeManagment;
        }

        private IStoreManagment _storeManagment;

        [HttpGet]
        [Authorize("read:store")]
        public PagedResult<IOrder> GetOrders(int storeId)
        {
            var store = this._storeManagment.GetStore(storeId);
            if (store == null) throw new Exception(Store.Errors.NotFound);
            return store.GetOrdersPage(0, 100);
        }
        [HttpGet("{id}")]
        [Authorize]
        public IOrder GetOpenOrder(int storeId, int id)
        {
            return FindOrderInStore(storeId, id);
        }

        [HttpPost("{id}")]
        [Authorize]
        public IOrder AddProductToOrder(int storeId, int id, [FromBody] Product product)
        {
            var order = FindOrderInStore(storeId, id);
            order.AddToList(product);
            return order;
        }
        
        [HttpPost("{id}/product")]
        [Authorize]
        public IOrder UpdateOrder(int storeId, int id, [FromBody] UpdateOrderProduct data)
        {
            if (!data.Validate()) throw new Exception(UpdateOrderProduct.Errors.InvalidRequest);
            var order = FindOrderInStore(storeId, id);
            order.UpdateProductCount(data.article, data.count);
            return order;
        }

        [HttpDelete("{id}/product")]
        [Authorize]
        public IOrder RemoveArticleFromOrder(int storeId, int id, [FromBody] UpdateOrderProduct data)
        {
            if (data.article <=0 ) throw new Exception(Product.Errors.InvalidArticle);
            var order = FindOrderInStore(storeId, id);
            order.RemoveFromList(data.article);
            return order;
        }

        [HttpPut("{id}")]
        [Authorize]
        public IOrder SubmitOrder(int storeId, int id)
        {
            var order = FindOrderInStore(storeId, id);
            order.Apply();
            return order;
        }

        [HttpPut]
        public IOrder CreateOrder(int storeId)
        {    
            var store = _storeManagment.GetStore(storeId);
            if (store == null) throw new Exception(Store.Errors.NotFound);
            return new Order(store);
        }

        private IOrder FindOrderInStore(int storeId, int id)
        {
            var store = this._storeManagment.GetStore(storeId);
            if (store == null) throw new Exception(Store.Errors.NotFound);
            var order = store.GetOrder(id);
            if (order == null) throw new Exception(Order.Errors.NotFound);
            return order;
        }
    }
}