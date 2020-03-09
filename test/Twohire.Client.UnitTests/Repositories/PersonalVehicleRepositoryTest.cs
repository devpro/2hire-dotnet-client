using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Devpro.Twohire.Abstractions.Models;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Repositories;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;
using Xunit;

namespace Devpro.Twohire.Client.UnitTests.Repositories
{
    [Trait("Category", "UnitTests")]
    public class PersonalVehicleRepositoryTest : RepositoryTestBase
    {
        [Fact]
        public async Task PersonalVehicleRepositoryFindAllAsync_ReturnData()
        {
            // Arrange
            var fixture = new Fixture();
            var responseModel = fixture.Create<ResponseModel<List<object>>>();
            var httpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseModel.ToJson())
            };
            var repository = BuildRepository(httpResponseMessage, HttpMethod.Get, $"http://does.not.exist/v42/admin/api/personal/vehicle");

            // Act
            var output = await repository.FindAllAsync();

            // Assert
            output.Should().NotBeNull();
            output.Should().BeEquivalentTo(responseModel);
        }

        private IPersonalVehicleRepository BuildRepository(HttpResponseMessage httpResponseMessage, HttpMethod httpMethod, string absoluteUri)
        {
            var logger = ServiceProvider.GetService<ILogger<PersonalVehicleRepository>>();
            var httpClientFactoryMock = BuildHttpClientFactory(httpResponseMessage, httpMethod, absoluteUri);
            TokenProviderMock.Setup(x => x.Token).Returns("loveDotNet");

            return new PersonalVehicleRepository(Configuration, logger, httpClientFactoryMock.Object, TokenProviderMock.Object);
        }
    }
}
