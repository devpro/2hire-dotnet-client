using System.Collections.Generic;
using System.Threading.Tasks;

namespace Devpro.Twohire.Client.Domain.Repositories
{
    /// <summary>
    /// Personal vehicle repository.
    /// </summary>
    public interface IPersonalVehicleRepository
    {
        Task<List<object>> FindAllAsync();
    }
}
