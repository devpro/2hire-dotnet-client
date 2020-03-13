using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Devpro.Twohire.Abstractions.Models;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Dto;
using Devpro.Twohire.Client.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;
using Xunit;

namespace Devpro.Twohire.Client.UnitTests.Repositories
{
    [Trait("Category", "UnitTests")]
    public class TokenRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task TokenRepositoryCreateAsync_ReturnToken()
        {
            // Arrange
            var fixture = new Fixture();
            var responseDto = fixture.Create<ResponseModel<TokenDataDto>>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseDto.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Post, $"http://does.not.exist/v42/admin/login");

            // Act
            var output = await repository.CreateAsync();

            // Assert
            output.Should().NotBeNull();
            output.Value.Should().Be(responseDto.Data.Token.Code);
            output.ExpiredDate.Should().Be(responseDto.Data.Token.CreatedAt.Add(new TimeSpan(responseDto.Data.Token.Expire)));
        }

        private ITokenRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
        {
            var logger = ServiceProvider.GetService<ILogger<TokenRepository>>();
            var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, "Fake", absoluteUri);

            return new TokenRepository(Configuration, logger, httpClientFactoryMock.Object);
        }
    }
}
