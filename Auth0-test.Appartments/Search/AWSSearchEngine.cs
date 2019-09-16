using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Amazon.CloudSearchDomain;
using Amazon.CloudSearchDomain.Model;
using Auth0_test.Appartments.Models;
using Auth0_test.SearchEngine.Models.AWSDataContract;
using Microsoft.Extensions.Configuration;

namespace Auth0_test.Appartments
{
    public class AWSSearchEngine : ISearchEngine
    {
        private AmazonCloudSearchDomainClient searchClient;
        private AmazonCloudSearchDomainClient documentClient;
        private IConfiguration _configuration;

        public AWSSearchEngine(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public void Init()
        {
            var apiKey = _configuration["AWS:apiKey"];
            var apiSecret = _configuration["AWS:apiSecret"];
            var searchUrl = _configuration["AWS:searchUrl"];
            var docUrl = _configuration["AWS:docUrl"];
            this.searchClient = this.searchClient ?? new AmazonCloudSearchDomainClient(apiKey, apiSecret, searchUrl);
            this.documentClient = this.documentClient ?? new AmazonCloudSearchDomainClient(apiKey, apiSecret, docUrl);
        }

        public async Task<bool> UpdateIndex(IEnumerable<SearchEngineItem> items)
        {
            using (var memoryStream = new MemoryStream())
            {
                var jsonSerializer = new DataContractJsonSerializer(typeof(BatchDocumentItem[]));
                var batch = items.Select(x => new BatchDocumentItem(x)).ToArray();
                jsonSerializer.WriteObject(memoryStream, batch);
                memoryStream.Position = 0; // reset cursor to able pass stream to AWS
                var upload = new UploadDocumentsRequest
                {
                    ContentType = ContentType.ApplicationJson,
                    Documents = memoryStream
                };
                var result = await documentClient.UploadDocumentsAsync(upload);
                return true; // todo handle response message from AWS.
            }
        }

        public async Task<IEnumerable<SearchEngineItem>> Search(string SearchStr, string market, int limit)
        {
            SearchRequest searchRequest = new SearchRequest();
            searchRequest.Query = SearchStr;
            searchRequest.Size = limit;
            searchRequest.FilterQuery = BuildMarketFilterQuery(market);
            searchRequest.QueryParser = QueryParser.Lucene;

            try
            {
                SearchResponse searchResponse = await searchClient.SearchAsync(searchRequest);
                return ConvertSearchResult(searchResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private IEnumerable<SearchEngineItem> ConvertSearchResult(SearchResponse searchResponse)
        {
            var hits = searchResponse.Hits.Hit.ToArray();
            var result = hits.Select(document =>
            {
                var b = document.Fields;
                return new SearchEngineItem(b);
            });
            return result;
        }

        private string BuildMarketFilterQuery(string market)
        {
            if (string.IsNullOrWhiteSpace(market)) return default(string);
            var parts = market.Split(',').Select(x => $"market:'{x}'").Aggregate((agg, next) => agg + " " + next);
            var filterQuery = $"(or {parts})";
            return filterQuery;
        }

        public void Dispose()
        {
            searchClient?.Dispose();
            documentClient?.Dispose();
        }
    }
}