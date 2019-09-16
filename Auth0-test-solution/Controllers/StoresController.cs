using System;
using System.Collections.Generic;
using System.Linq;
using Auth0_test.BL.Repository;
using Auth0_test.BL.Services;
using Auth0_test_solution.BL.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Auth0_test_solution.Controllers
{
    [Route("api/stores")]
    [ApiController]
    public class StoresController: ControllerBase
    {
        private IStoreManagment _storeManagmentService;
        public StoresController([FromServices] IStoreManagment service)
        {
            _storeManagmentService = service;
        }
        [HttpGet]
        public PagedResult<IStore> GetStoreList()
        {
            return  new PagedResult<IStore>(_storeManagmentService.GetStores(), 2);
        }
        [HttpGet("{id}")]
        public IStore GetStore(int id)
        {
            return _storeManagmentService.GetStore(id);
        }
        [HttpGet("{id}/products")]
        public PagedResult<Product> GetStoreProducts(int id)
        {
            return new PagedResult<Product>(_storeManagmentService.GetStore(id).GetProductList(), 4);
        }
    }
}