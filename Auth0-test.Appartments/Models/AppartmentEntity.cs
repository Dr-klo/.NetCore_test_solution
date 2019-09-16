using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Auth0_test.Appartments.Models
{
    public abstract class AppartmentEntity
    {
        public enum AppartmentTypeEnum
        {
            Property,
            ManagmentCompany
        }

        public AppartmentEntity(int id, string name, string market)
        {
            this.Id = id;
            this.Name = name;
            this.Market = market;
        }
        [Required]
        public abstract int Id { get; protected set; }
        [Required]
        [MaxLength(50)]    
        [JsonProperty(PropertyName = "name")]
        public string Name { get; protected set; }
        [Required]
        [JsonProperty(PropertyName = "market")]
        public string Market { get; private set; }
    }
}