using Microsoft.Extensions.DependencyInjection;

namespace Auth0_test.Appartments
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISearchEngine, AWSSearchEngine>();
        }
    }
}