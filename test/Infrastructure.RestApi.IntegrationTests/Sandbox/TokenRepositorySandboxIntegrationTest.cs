using System;
using System.Net.Http;
using System.Threading.Tasks;
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
    public class TokenRepositorySandboxIntegrationTest
    {
        public TokenRepositorySandboxIntegrationTest()
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
        public async Task TokenRepositorySandboxCreateAsync_ReturnToken()
        {
            // Arrange
            var repository = BuildRepository();

            // Act
            var output = await repository.CreateAsync();

            // Assert
            output.Should().NotBeNull();
            output.Value.Should().NotBeNullOrEmpty();
            output.ExpiredDate.Should().BeAfter(DateTime.Now);
        }

        private ITokenRepository BuildRepository()
        {
            var logger = ServiceProvider.GetService<ILogger<TokenRepository>>();
            var httpClientFactory = ServiceProvider.GetService<IHttpClientFactory>();

            return new TokenRepository(Configuration, logger, httpClientFactory);
        }
    }
}
