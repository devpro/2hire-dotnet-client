namespace Devpro.Twohire.Client.Infrastructure.RestApi.UnitTests.Fakes
{
    public class FakeConfiguration : ITwohireRestApiConfiguration
    {
        public string BaseUrl => "http://does.not.exist";

        public string Version => "v42";

        public string ServiceToken => "Greetings";

        public string Username => "Hello";

        public string Password => "There";

        public string HttpClientName => "Fake";
    }
}
