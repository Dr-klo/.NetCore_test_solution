using System.Collections.Generic;
using System.Threading.Tasks;
using Auth0_test.Appartments.Models;
using Auth0_test.SearchEngine.Models.AWSDataContract;

namespace Auth0_test.Appartments
{
    public class AzureSearchEngine: ISearchEngine
    {
        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Init()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateIndex(IEnumerable<SearchEngineItem> items)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SearchEngineItem>> Search(string SearchStr, string market, int limit)
        {
            throw new System.NotImplementedException();
        }
    }
}