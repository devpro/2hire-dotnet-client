using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Models;
using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Abstractions.Repositories;
using Microsoft.Extensions.Logging;

namespace Devpro.Twohire.Client.Repositories
{
    public class PersonalVehicleRepository : RepositoryBase, IPersonalVehicleRepository
    {
        public PersonalVehicleRepository(
            ITwohireRestApiConfiguration configuration,
            ILogger<PersonalVehicleRepository> logger,
            IHttpClientFactory httpClientFactory,
            ITokenProvider tokenProvider)
            : base(configuration, logger, httpClientFactory, tokenProvider)
        {
        }

        protected override string ResourceName => "admin/api/personal/vehicle";

        public async Task<ResponseModel<List<object>>> FindAllAsync()
        {
            var url = GenerateUrl();
            var output = await GetAsync<ResponseModel<List<object>>>(url);
            return output;
        }
    }
}
