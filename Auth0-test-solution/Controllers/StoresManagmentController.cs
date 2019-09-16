using System.Collections.Generic;
using Auth0_test.BL.Services;
using Auth0_test_solution.BL.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth0_test_solution.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresManagmentController : ControllerBase
    {  
        private IStoreManagment StoreManagmentService;
        public StoresManagmentController([FromServices] IStoreManagment service)
        {
            StoreManagmentService = service;
        }
        [HttpPost("{id}")]
        [Authorize("update:store")]
        public IEnumerable<Product> UpdateProductList(int id, [FromBody] Product product)
        {
            var store = StoreManagmentService.GetStore(id);
            store.AddOrUpdateProduct(product);
            return store.GetProductList();
        }
    }
}