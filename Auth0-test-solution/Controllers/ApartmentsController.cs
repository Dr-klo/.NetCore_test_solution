using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Auth0_test.Appartments;
using Auth0_test.Appartments.Models;
using Auth0_test.Appartments.Utils;
using Auth0_test.SearchEngine.Models.AWSDataContract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;

namespace Auth0_test_solution.Controllers
{
    [ApiController]
    [Route("api/apartments")]
    public class ApartmentsController : ControllerBase
    {
        private ISearchEngine engine;

        public ApartmentsController(ISearchEngine searchEngine)
        {
            this.engine = searchEngine;
            this.engine.Init();
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> Search(string query, string market = null, int limit = 25)
        {
            var data = await this.engine.Search(query, market, limit);
            return Ok(data);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(IFormFile file)
        {
            if (file == null) return BadRequest();
            IEnumerable<SearchEngineItem> formedData;
            try
            {
                formedData = new ApartmentSerealizer().Deserialize(file.OpenReadStream());
                await this.engine.UpdateIndex(formedData);
            }
            catch (JsonReaderException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return BadRequest(ApartmentSerealizer.Errors.BadFile);
            }
            catch (ApplicationDomainException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}