using System;

namespace Devpro.Twohire.Client.IntegrationTests.Sandbox
{
    public class Sandbox2hireRestApiConfiguration : ITwohireRestApiConfiguration
    {
        public string BaseUrl => Environment.GetEnvironmentVariable("TwoHire_Sandbox_BaseUrl");

        public string Version => Environment.GetEnvironmentVariable("TwoHire_Sandbox_ApiVersion");

        public string ServiceToken => Environment.GetEnvironmentVariable("TwoHire_Sandbox_ServiceToken");

        public string Username => Environment.GetEnvironmentVariable("TwoHire_Sandbox_Username");

        public string Password => Environment.GetEnvironmentVariable("TwoHire_Sandbox_Password");

        public string HttpClientName => "TwoHire";
    }
}
