using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IEducationRepository : IRepository<Guid, Education>
    {

        Task<IEnumerable<Education>> GetAll(Guid UserId);

         Task<Education> Get(Guid id, Guid UserId);
    }
}
