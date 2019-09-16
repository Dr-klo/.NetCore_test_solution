using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Auth0_test.Appartments.Models;

namespace Auth0_test.SearchEngine.Models.AWSDataContract
{
    [DataContract]
    public class BatchDocumentItem
    {
        [DataMember] public string type { get; set; }
        [DataMember] public string id { get; set; }
        [DataMember] public SearchEngineItem fields { get; set; }

        public BatchDocumentItem(SearchEngineItem item)
        {
            this.fields = item;
            this.id = item.entity_id.ToString();
            this.type = "add";
        }
    }

    [DataContract]
    public class SearchEngineItem
    {
        [DataMember] public AppartmentEntity.AppartmentTypeEnum type { get; set; }
        [DataMember] public int entity_id { get; set; }
        [DataMember(EmitDefaultValue = false)] public string name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string former_name { get; set; }
        [DataMember(EmitDefaultValue = false)] public string street_address { get; set; }
        [DataMember(EmitDefaultValue = false)] public string city { get; set; }
        [DataMember(EmitDefaultValue = false)] public string state { get; set; }
        [DataMember(EmitDefaultValue = false)] public string market { get; set; }

        public struct FieldRetriever
        {
            private Dictionary<string, List<string>> dictionary;

            public FieldRetriever(Dictionary<string, List<string>> dictionary)
            {
                this.dictionary = dictionary;
            }

            public T RetrieveFromFields<T>(string fieldname)
            {
                return dictionary != null && dictionary.ContainsKey(fieldname)
                    ? (T) Convert.ChangeType(dictionary[fieldname][0], typeof(T))
                    : default(T);
            }
        }
/// <summary>
/// 
/// </summary>
/// <param name="fields"></param>
        public SearchEngineItem(Dictionary<string, List<string>> fields)
        {
            var fieldsRetreiver = new FieldRetriever(fields);
            this.type = (AppartmentEntity.AppartmentTypeEnum) fieldsRetreiver.RetrieveFromFields<int>(nameof(type));
            this.name = fieldsRetreiver.RetrieveFromFields<string>(nameof(name));
            this.entity_id = fieldsRetreiver.RetrieveFromFields<int>(nameof(entity_id));
            this.former_name = fieldsRetreiver.RetrieveFromFields<string>(nameof(former_name));
            this.street_address = fieldsRetreiver.RetrieveFromFields<string>(nameof(street_address));
            this.city = fieldsRetreiver.RetrieveFromFields<string>(nameof(city));
            this.state = fieldsRetreiver.RetrieveFromFields<string>(nameof(state));
            this.market = fieldsRetreiver.RetrieveFromFields<string>(nameof(market));
        }

        public SearchEngineItem(Property property) : this((AppartmentEntity) property)
        {
            this.type = AppartmentEntity.AppartmentTypeEnum.Property;
            this.former_name = property.FormerName;
            this.street_address = property.StreetAddress;
            this.city = property.City;
            this.state = property.State;
        }

        public SearchEngineItem(AppartmentEntity entity) : this()
        {
            this.entity_id = entity.Id;
            this.name = entity.Name;
            this.market = entity.Market;
        }

        public SearchEngineItem()
        {
        }

        public SearchEngineItem(ManagmentCompany market) : this((AppartmentEntity) market)
        {
            this.type = AppartmentEntity.AppartmentTypeEnum.ManagmentCompany;
            this.state = market.State;
        }
    }
}