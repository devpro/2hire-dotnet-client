using System.Net.Http;
using Devpro.Twohire.Client.Domain.Providers;
using Devpro.Twohire.Client.Domain.Repositories;
using Devpro.Twohire.Client.Infrastructure.RestApi.DependencyInjection;
using Devpro.Twohire.Client.Infrastructure.RestApi.UnitTests.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void Add2hireRestApi_ShouldProvideRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.Add2hireRestApi(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<IPersonalVehicleRepository>().Should().NotBeNull();
            services.GetRequiredService<ITokenRepository>().Should().NotBeNull();
        }
        [Fact]
        public void Add2hireRestApi_ShouldProvideProviders()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.Add2hireRestApi(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<ITokenProvider>().Should().NotBeNull();
        }

        [Fact]
        public void Add2hireRestApi_ShouldProvideHttpClient()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.Add2hireRestApi(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClientFactory.Should().NotBeNull();
            var client = httpClientFactory.CreateClient(configuration.HttpClientName);
            client.Should().NotBeNull();
        }
    }
}
