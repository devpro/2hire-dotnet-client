using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Exceptions;
using Devpro.Twohire.Client.Domain.Providers;
using Microsoft.Extensions.Logging;
using Withywoods.Serialization.Json;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly ITokenProvider _tokenProvider;

        protected RepositoryBase(
            ITwohireRestApiConfiguration configuration,
            ILogger logger,
            IHttpClientFactory httpClientFactory,
            ITokenProvider tokenProvider)
        {
            Configuration = configuration;
            Logger = logger;
            HttpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        protected ITwohireRestApiConfiguration Configuration { get; private set; }

        protected ILogger Logger { get; private set; }

        protected IHttpClientFactory HttpClientFactory { get; private set; }

        protected abstract string ResourceName { get; }

        protected string GenerateUrl(string arguments = "")
        {
            return $"{Configuration.BaseUrl}/{Configuration.Version}/{ResourceName}{arguments}";
        }

        protected virtual async Task<T> GetAsync<T>(string url) where T : class
        {
            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);
            SetBearerToken(client);

            Logger.LogDebug($"Async GET call initiated [HttpRequestUrl={url}]");
            var response = await client.GetAsync(url);
            Logger.LogDebug($"Async GET call completed [HttpRequestUrl={url}] [HttpResponseStatus={response.StatusCode}]");

            var stringResult = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringResult))
            {
                throw new ConnectivityException($"Empty response received while calling {url}");
            }

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpResponseContent={stringResult}]");
                response.EnsureSuccessStatusCode();
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize GET call response content [HttpRequestUrl={url}] [HttpResponseContent={stringResult}] [SerializationType={typeof(T).ToString()}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }

        protected virtual async Task<T> PostAsync<T>(string url, object body) where T : class
        {
            var client = HttpClientFactory.CreateClient(Configuration.HttpClientName);
            SetBearerToken(client);

            Logger.LogDebug($"Async POST call initiated [HttpRequestUrl={url}]");
            var response = await client.PostAsync(url, new StringContent(body.ToJson(), Encoding.UTF8, "application/json"));
            Logger.LogDebug($"Async POST call completed [HttpRequestUrl={url}] [HttpResponseStatus={response.StatusCode}]");

            var stringResult = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(stringResult))
            {
                throw new ConnectivityException($"Empty response received while calling {url}");
            }

            if (!response.IsSuccessStatusCode)
            {
                Logger.LogDebug($"Status code doesn't indicate success [HttpRequestUrl={url}] [HttpStatusCode={response.StatusCode}] [HttpResponseContent={stringResult}]");
                response.EnsureSuccessStatusCode();
            }

            try
            {
                return stringResult.FromJson<T>();
            }
            catch (Exception exc)
            {
                Logger.LogWarning($"Cannot deserialize POST call response content [HttpRequestUrl={url}] [HttpResponseContent={stringResult}] [SerializationType={typeof(T).ToString()}] [ExceptionMessage={exc.Message}]");
                Logger.LogDebug($"[Stacktrace={exc.StackTrace}]");
                throw new ConnectivityException($"Invalid data received when calling \"{url}\". {exc.Message}.", exc);
            }
        }

        private void SetBearerToken(HttpClient client)
        {
            client.DefaultRequestHeaders.Add("x-service-token", Configuration.ServiceToken);
            if (_tokenProvider?.Token != null)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _tokenProvider.Token);
            }
        }
    }
}
