using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Devpro.Twohire.Client.Infrastructure.RestApi.UnitTests.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException("This code shouldn't be executed, the method muct be mocked");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}
