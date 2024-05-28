using Job_Portal_Application.Models;
using Job_Portal_Application.Services.UsersServices;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IAreasOfInterestRepository:IRepository<Guid,AreasOfInterest>
    {

        Task<IEnumerable<AreasOfInterest>> GetAll(Guid UserId);
        Task<AreasOfInterest> Get(Guid id, Guid UserId);
    }
}
