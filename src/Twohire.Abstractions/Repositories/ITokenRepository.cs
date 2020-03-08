using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Models;

namespace Devpro.Twohire.Abstractions.Repositories
{
    /// <summary>
    /// Token repository.
    /// </summary>
    public interface ITokenRepository
    {
        Task<TokenModel> CreateAsync();
    }
}
