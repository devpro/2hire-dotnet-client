using System;

namespace Devpro.Twohire.Client.IntegrationTests.Sandbox
{
    public class Sandbox2hireRestApiConfiguration : ITwohireClientConfiguration
    {
        public string BaseUrl => Environment.GetEnvironmentVariable("TwoHire__Sandbox__BaseUrl");

        public string Version => Environment.GetEnvironmentVariable("TwoHire__Sandbox__ApiVersion");

        public string ServiceToken => Environment.GetEnvironmentVariable("TwoHire__Sandbox__ServiceToken");

        public string Username => Environment.GetEnvironmentVariable("TwoHire__Sandbox__Username");

        public string Password => Environment.GetEnvironmentVariable("TwoHire__Sandbox__Password");

        public string HttpClientName => "TwoHire";
    }
}
