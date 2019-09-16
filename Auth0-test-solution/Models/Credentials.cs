using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using RestSharp;

namespace Auth0_test_solution.Models
{
   
    public class Credentials
    {
        public  string apiId { get;  set; }
        public  string apiKey { get;  set; }
    }

    public class User : Credentials
    {
        public User(string apiId, string apiKey, IConfiguration configuration)
        {
            this.apiId = apiId;
            this.apiKey = apiKey;
            this._config = configuration;
        }

        private IConfiguration _config;
        public string[] permissions { get; set; }

        public string Authentificate()
        {
            var domain = $"https://{_config["Auth0:Domain"]}/";
            var client = new RestClient($"{domain}oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/json");
            request.AddParameter("application/json", $"{{\"client_id\":\"{apiId}\",\"client_secret\":\"{apiKey}\",\"audience\":\"{_config["Auth0:ApiIdentifier"]}\",\"grant_type\":\"client_credentials\"}}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            dynamic token = JsonConvert.DeserializeObject(response.Content);
            return token.access_token;
        }
    }
}