using System;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Repositories;
using Devpro.Twohire.Client.Infrastructure.RestApi.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.IntegrationTests.Sandbox
{
    [Trait("Environment", "Sandbox")]
    public class TokenRepositorySandboxIntegrationTest : RepositoryIntegrationTestBase
    {
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
