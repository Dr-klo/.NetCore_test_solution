using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Amazon.Runtime.Internal;
using Auth0_test.Appartments.Models;
using Auth0_test.SearchEngine.Models;
using Auth0_test.SearchEngine.Models.AWSDataContract;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;

namespace Auth0_test.Appartments.Utils
{
    public class ApartmentSerealizer
    {
        public struct Errors
        {
            public const string BadFile = "File cannot be parsed.";
            public const string ValidationErrors = "Data validation problems.";
        }

        public IEnumerable<SearchEngineItem> Deserialize(Stream stream)
        {
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return ParseJSONFile(jsonTextReader);
            }
        }    

        private IEnumerable<SearchEngineItem> ParseJSONFile(JsonTextReader jsonTextReader)
        {
            using (JSchemaValidatingReader validatingReader = new JSchemaValidatingReader(jsonTextReader))
            {
                var messages = SubscribeValidators(validatingReader);
                var serializer = new JsonSerializer();
                var data = serializer.Deserialize<IEnumerable<ApartmentItem>>(validatingReader);
                if (messages.Count > 0)
                {
                    messages.ToList().ForEach(x => System.Diagnostics.Debug.WriteLine(x));
                    // todo handle verification errors.
//                        throw new ApplicationDomainException(ApartmentSerealizer.Errors.ValidationErrors, messages.ToArray());
                }
                // todo move it to AWS specific service.
                return data.Select(x =>
                {
                    return x.mgmt != null ? new SearchEngineItem(x.mgmt) : new SearchEngineItem(x.property);
                }).ToArray();
            }
        }

        private IList<string> SubscribeValidators(JSchemaValidatingReader validatingReader)
        {
            JSchemaGenerator generator = new JSchemaGenerator();
            JSchema schema = generator.Generate(typeof(ApartmentItem[]));
            IList<string> messages = new List<string>();
            validatingReader.Schema = schema;
            validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);
            return messages;
        }
    }
}