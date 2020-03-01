using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Providers;
using Devpro.Twohire.Client.Domain.Repositories;
using Devpro.Twohire.Client.Infrastructure.RestApi.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.IntegrationTests.Sandbox
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
