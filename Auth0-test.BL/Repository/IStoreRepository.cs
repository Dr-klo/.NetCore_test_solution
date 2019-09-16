using System.Collections.Generic;
using Auth0_test_solution.BL.Domain;

namespace Auth0_test.BL.Repository
{
    public interface IStoreRepository
    {
        IEnumerable<IStore> GetAll();
    }
}