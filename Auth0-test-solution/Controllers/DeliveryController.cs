using System;
using Auth0_test_solution.BL.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth0_test_solution.Controllers
{
    [ApiController]
    [Route("api/delivery/{storeId}")]
    public class DeliveryController: ControllerBase
    {
        
        [HttpPost("{id}")]
        [Authorize("update:delivery")]
        public IOrder UpdateDeliveryStatus()
        {
            throw new NotImplementedException();
        }
        
    }
}