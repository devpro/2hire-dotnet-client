using System.Net.Http;
using System.Net.Http.Headers;
using Devpro.Twohire.Abstractions.Providers;
using Microsoft.Extensions.Logging;
using Withywoods.Net.Http;

namespace Devpro.Twohire.Client.Repositories
{
    public abstract class RepositoryBase : HttpRepositoryBase
    {
        private readonly ITokenProvider _tokenProvider;

        protected RepositoryBase(
            ITwohireRestApiConfiguration configuration,
            ILogger logger,
            IHttpClientFactory httpClientFactory,
            ITokenProvider tokenProvider)
            : base(logger, httpClientFactory)
        {
            Configuration = configuration;
            _tokenProvider = tokenProvider;
        }

        protected ITwohireRestApiConfiguration Configuration { get; }

        protected abstract string ResourceName { get; }

        protected override string HttpClientName => Configuration.HttpClientName;

        protected string GenerateUrl(string arguments = "")
        {
            return $"{Configuration.BaseUrl}/{Configuration.Version}/{ResourceName}{arguments}";
        }

        protected override void EnrichRequestHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("x-service-token", Configuration.ServiceToken);
            if (_tokenProvider?.Token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.Token);
            }
        }
    }
}
