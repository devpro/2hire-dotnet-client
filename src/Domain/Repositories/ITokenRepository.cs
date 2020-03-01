using System.Threading.Tasks;
using Devpro.Twohire.Client.Domain.Models;

namespace Devpro.Twohire.Client.Domain.Repositories
{
    /// <summary>
    /// Token repository.
    /// </summary>
    public interface ITokenRepository
    {
        Task<TokenModel> CreateAsync();
    }
}
