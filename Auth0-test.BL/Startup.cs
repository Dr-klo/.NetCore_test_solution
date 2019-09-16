using Auth0_test.BL.Repository;
using Auth0_test.BL.Repository.Fakes;
using Auth0_test.BL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Auth0_test.BL
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStoreRepository, FakeStoreRepository>();
            services.AddSingleton<IStoreManagment, StoreManagment>();
        }
    }
}