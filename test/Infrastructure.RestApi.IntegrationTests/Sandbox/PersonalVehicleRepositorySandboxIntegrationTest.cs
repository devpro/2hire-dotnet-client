using System;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Providers;
using Devpro.Twohire.Client.Domain.Repositories;
using Devpro.Twohire.Client.Infrastructure.RestApi.DependencyInjection;
using Devpro.Twohire.Client.Infrastructure.RestApi.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.IntegrationTests.Sandbox
{
    [Trait("Environment", "Sandbox")]
    public class PersonalVehicleRepositorySandboxIntegrationTest
    {
        public PersonalVehicleRepositorySandboxIntegrationTest()
        {
            Configuration = new Sandbox2hireRestApiConfiguration();

            var services = new ServiceCollection()
                .AddLogging()
                .Add2hireRestApi(Configuration);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected ServiceProvider ServiceProvider { get; private set; }

        protected Sandbox2hireRestApiConfiguration Configuration { get; private set; }

        [Fact]
        public async Task PersonalVehicleRepositorySandboxFindAllAsync_ReturnToken()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = await repository.FindAllAsync();

            // Assert
            output.Should().NotBeNullOrEmpty();
        }

        private IPersonalVehicleRepository BuildRepository()
        {
            var logger = ServiceProvider.GetService<ILogger<PersonalVehicleRepository>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();
            var tokenProvider = ServiceProvider.GetService<ITokenProvider>();

            return new PersonalVehicleRepository(Configuration, logger, httpClientFactory, tokenProvider);
        }
    }
}
