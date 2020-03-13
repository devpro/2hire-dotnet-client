using System;
using System.Net.Http;
using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.DependencyInjection;
using Devpro.Twohire.Client.UnitTests.Fakes;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Devpro.Twohire.Client.UnitTests.DependencyInjection
{
    [Trait("Category", "UnitTests")]
    public class ServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddTwohireClient_ShouldProvideRepositories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.AddTwohireClient(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<IPersonalVehicleRepository>().Should().NotBeNull();
            services.GetRequiredService<ITokenRepository>().Should().NotBeNull();
        }

        [Fact]
        public void AddTwohireClient_ShouldProvideProviders()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.AddTwohireClient(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            services.GetRequiredService<ITokenProvider>().Should().NotBeNull();
        }

        [Fact]
        public void AddTwohireClient_ShouldProvideHttpClient()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = new FakeConfiguration();

            // Act
            serviceCollection.AddTwohireClient(configuration);

            // Assert
            var services = serviceCollection.BuildServiceProvider();
            var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();
            httpClientFactory.Should().NotBeNull();
            var client = httpClientFactory.CreateClient(configuration.HttpClientName);
            client.Should().NotBeNull();
        }

        [Fact]
        public void AddTwohireClient_ShouldThrowExceptionIfServiceCollectionIsNull()
        {
            // Arrange
            var serviceCollection = (ServiceCollection)null;
            var configuration = new FakeConfiguration();

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddTwohireClient(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'services')");
        }

        [Fact]
        public void AddTwohireClient_ShouldThrowExceptionIfConfigurationIsNull()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();
            var configuration = (FakeConfiguration)null;

            // Act
            var exc = Assert.Throws<ArgumentNullException>(() => serviceCollection.AddTwohireClient(configuration));

            // Assert
            exc.Should().NotBeNull();
            exc.Message.Should().Be("Value cannot be null. (Parameter 'configuration')");
        }
    }
}
