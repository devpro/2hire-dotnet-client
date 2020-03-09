using System.Collections.Generic;
using System.Threading.Tasks;
using Devpro.Twohire.Abstractions.Models;

namespace Devpro.Twohire.Abstractions.Repositories
{
    /// <summary>
    /// Personal vehicle repository.
    /// </summary>
    public interface IPersonalVehicleRepository
    {
        Task<ResponseModel<List<object>>> FindAllAsync();
    }
}
