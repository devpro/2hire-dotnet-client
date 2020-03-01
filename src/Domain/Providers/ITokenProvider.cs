namespace Devpro.Twohire.Client.Domain.Providers
{
    /// <summary>
    /// Token provider.
    /// </summary>
    public interface ITokenProvider
    {
        string Token { get; }
    }
}
