using Devpro.Twohire.Client.Infrastructure.RestApi.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.IntegrationTests.Sandbox
{
    public class RepositoryIntegrationTestBase
    {
        protected RepositoryIntegrationTestBase()
        {
            Configuration = new Sandbox2hireRestApiConfiguration();

            var services = new ServiceCollection()
                .AddLogging()
                .Add2hireRestApi(Configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected Sandbox2hireRestApiConfiguration Configuration { get; private set; }
    }
}
