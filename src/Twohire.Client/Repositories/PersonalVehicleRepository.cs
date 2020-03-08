using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Providers;
using Devpro.Twohire.Client.Domain.Repositories;
using Devpro.Twohire.Client.Infrastructure.RestApi.Dto;
using Microsoft.Extensions.Logging;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.Repositories
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

        public async Task<List<object>> FindAllAsync()
        {
            var url = GenerateUrl();
            var output = await GetAsync<ResponseDto<List<object>>>(url);
            return output.Data;
        }
    }
}
