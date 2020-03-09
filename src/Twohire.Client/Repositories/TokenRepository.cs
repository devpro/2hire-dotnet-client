using System;
using System.Net.Http;
using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Models;
using Devpro.Twohire.Abstractions.Repositories;
using Devpro.Twohire.Client.Dto;
using Microsoft.Extensions.Logging;

namespace Devpro.Twohire.Client.Repositories
{
    public class TokenRepository : RepositoryBase, ITokenRepository
    {
        public TokenRepository(ITwohireRestApiConfiguration configuration, ILogger<TokenRepository> logger, IHttpClientFactory httpClientFactory)
            : base(configuration, logger, httpClientFactory, null)
        {
        }

        protected override string ResourceName => "admin/login";

        public async Task<TokenModel> CreateAsync()
        {
            var url = GenerateUrl();
            var input = new
            {
                username = Configuration.Username,
                password = Configuration.Password
            };
            var output = await PostAsync<ResponseModel<TokenDataDto>>(url, input);
            return new TokenModel
            {
                Value = output.Data.Token.Code,
                ExpiredDate = output.Data.Token.CreatedAt.Add(new TimeSpan(output.Data.Token.Expire))
            };
        }
    }
}
