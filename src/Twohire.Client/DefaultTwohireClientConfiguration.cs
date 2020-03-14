namespace Devpro.Twohire.Client
{
    /// <summary>
    /// Default configuration.
    /// </summary>
    public class DefaultTwohireClientConfiguration : ITwohireClientConfiguration
    {
        public string BaseUrl { get; set; }

        public string Version { get; set; }

        public string ServiceToken { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string HttpClientName { get; set; } = "Twohire";
    }
}
