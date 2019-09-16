
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Auth0_test.Appartments.Models
{
    [JsonObject]
    public class Property: AppartmentEntity
    {
        [JsonProperty(PropertyName = "propertyID")]
        public override int Id { get; protected set; }
        [MaxLength(50)]
        [JsonProperty(PropertyName = "formerName")]
        public string FormerName { get; private set; }
        [Required]
        [MaxLength(150)]
        [JsonProperty(PropertyName = "streetAddress")]
        public string StreetAddress { get; private set; }
        
        [Required]
        [MaxLength(20)]
        [JsonProperty(PropertyName = "city")]
        public string City { get; private set; }

        [Required]
        [MaxLength(3)]
        [JsonProperty(PropertyName = "state")]
        public string State { get; private set; }
        
        [Required]
        [JsonProperty(PropertyName = "lat")]
        public double Lat { get; private set; }
        
        [Required]
        [JsonProperty(PropertyName = "lng")]
        public double Lng { get; set; }

        /*
         
      "propertyID": 70034, //int
      "name": "Sage at 1825 Place", //string
      "formerName": "1825 Place 2", //string
      "streetAddress": "15835 Foothill Farms Loop", //string
      "city": "Pflugerville", //string
      "market": "Austin", //string
      "state": "TX", //string
      "lat": 3.044956000000000e+001, //float
      "lng": -9.765073000000000e+001 //float

         */
        [JsonConstructor]
        public Property(int propertyId, string market, string name) : base(propertyId, name, market)
        {
            
        }
    }
}