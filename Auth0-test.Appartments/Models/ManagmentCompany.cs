using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Auth0_test.Appartments.Models
{
    public class ManagmentCompany : AppartmentEntity
    {
        [JsonConstructor]
        public ManagmentCompany(int id, string name, string market) : base(id, name, market)
        {
        }

        [Required] [MaxLength(3)] public string State { get; private set; }


        [JsonProperty(PropertyName = "mgmtID")]
        public override int Id { get; protected set; }
    }
}