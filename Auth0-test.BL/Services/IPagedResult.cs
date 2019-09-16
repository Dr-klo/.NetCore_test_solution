using System.Collections.Generic;
using System.Xml.Schema;
using Auth0_test_solution.BL.Domain;

namespace Auth0_test.BL.Services
{
    public class PagedResult<T>
    {
        public struct Errors
        {
            public const string InvalidPageSettings = "Page size or index of page is wrong.";
        }
        public PagedResult(IEnumerable<T> items, int total)
        {
            this.Items = items;
            this.Total = total;
        }
        public int Total { get;}
        public IEnumerable<T> Items { get;}
    }
}