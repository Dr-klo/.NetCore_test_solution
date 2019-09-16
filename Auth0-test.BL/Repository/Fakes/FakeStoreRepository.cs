using System.Collections.Generic;
using Auth0_test_solution.BL.Domain;

namespace Auth0_test.BL.Repository.Fakes
{
    public class FakeStoreRepository : IStoreRepository
    {
        private IList<IStore>  stores = new List<IStore>()
        {
            new Store("Ebay", "US", 1),
            new Store("Aliexpress", "China", 2)
        };
        public IEnumerable<IStore> GetAll()
        {
            return stores;
        }
    }
}