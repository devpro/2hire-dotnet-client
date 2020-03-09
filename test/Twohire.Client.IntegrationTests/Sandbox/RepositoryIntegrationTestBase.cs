using Devpro.Twohire.Client.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Devpro.Twohire.Client.IntegrationTests.Sandbox
{
    public class RepositoryIntegrationTestBase
    {
        protected RepositoryIntegrationTestBase()
        {
            Configuration = new Sandbox2hireRestApiConfiguration();

            var services = new ServiceCollection()
                .AddLogging()
                .AddTwohireClient(Configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected Sandbox2hireRestApiConfiguration Configuration { get; private set; }
    }
}
