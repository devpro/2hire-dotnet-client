using System;
using Devpro.Twohire.Abstractions.Models;
using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Providers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Devpro.Twohire.Client.UnitTests.Providers
{
    [Trait("Category", "UnitTests")]
    public class TokenProviderTest
    {
        [Fact]
        public void TokenProviderGet_ReturnToken()
        {
            // Arrange
            var tokenModel = new TokenModel { ExpiredDate = DateTime.UtcNow.AddDays(1), Value = "MyToken42" };
            var provider = BuildProvider(tokenModel);

            // Act
            var output = provider.Token;

            // Assert
            output.Should().Be(tokenModel.Value);
        }

        private ITokenProvider BuildProvider(TokenModel tokenModel)
        {
            var services = new ServiceCollection()
                .AddLogging();
            var serviceProvider = services.BuildServiceProvider();

            var logger = serviceProvider.GetService<ILogger<TokenProvider>>();

            var repositoryMock = new Mock<ITokenRepository>();
            repositoryMock.Setup(x => x.CreateAsync())
                .ReturnsAsync(tokenModel);

            return new TokenProvider(logger, repositoryMock.Object);
        }
    }
}
