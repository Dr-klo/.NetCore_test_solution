using System.Collections.Generic;
using System.Linq;
using Auth0_test.BL.Repository;
using Auth0_test_solution.BL.Domain;

namespace Auth0_test.BL.Services
{
    public interface IStoreManagment
    {
        IEnumerable<IStore> GetStores();
        IStore GetStore(int id);
    }

    public class StoreManagment: IStoreManagment
    {
        public StoreManagment(IStoreRepository repository)
        {
            this.StoreRepository = repository;
        }

        protected IStoreRepository StoreRepository;

        public IEnumerable<IStore> GetStores()
        {
            return StoreRepository.GetAll();
        }

        public IStore GetStore(int id)
        {
            return this.GetStores().FirstOrDefault(x => x.Id == id);
        }
    }
}