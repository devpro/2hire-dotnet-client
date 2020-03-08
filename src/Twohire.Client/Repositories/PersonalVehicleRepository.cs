using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Providers;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Dto;
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

        public async Task<List<object>> FindAllAsync()
        {
            var url = GenerateUrl();
            var output = await GetAsync<ResponseDto<List<object>>>(url);
            return output.Data;
        }
    }
}
