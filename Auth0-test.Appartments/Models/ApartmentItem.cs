using Auth0_test.Appartments.Models;
using Newtonsoft.Json;

namespace Auth0_test.SearchEngine.Models
{
    public class ApartmentItem
    {
        [JsonProperty(PropertyName = "property")]
        public Property property { get; private set; }
        [JsonProperty(PropertyName = "mgmt")]
        public ManagmentCompany mgmt { get; private set; }

        public override string ToString()
        {
            return $"Appartment : ManagmentCompany {this.mgmt?.Id} , Property {property?.Id}";
        }
    }
}