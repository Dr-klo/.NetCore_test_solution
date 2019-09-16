using Auth0_test_solution.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Auth0_test_solution.Controllers
{
    [Route("api")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration _config;
        public AccountController(IConfiguration config)
        {
            _config = config;
        }
        [HttpPost("login")]
        public string Login([FromBody] Credentials credentials)
        {
           var user = new User(credentials.apiId, credentials.apiKey, _config);
           return user.Authentificate();
        }
    }
}