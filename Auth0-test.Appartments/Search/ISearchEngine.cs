using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Auth0_test.Appartments.Models;
using Auth0_test.SearchEngine.Models.AWSDataContract;

namespace Auth0_test.Appartments
{
    public interface ISearchEngine: IDisposable
    {
        void Init();
        Task<bool> UpdateIndex(IEnumerable<SearchEngineItem> items);
        Task<IEnumerable<SearchEngineItem>> Search(string SearchStr, string market, int limit);
    }
}