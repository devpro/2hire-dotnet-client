using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Devpro.Twohire.Client.IntegrationTests.Sandbox
{
    [Trait("Environment", "Sandbox")]
    public class PersonalVehicleRepositorySandboxIntegrationTest : RepositoryIntegrationTestBase
    {
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
