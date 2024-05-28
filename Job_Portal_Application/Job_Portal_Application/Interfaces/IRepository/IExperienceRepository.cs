using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IExperienceRepository : IRepository<Guid, Experience>
    {

        public  Task<IEnumerable<Experience>> GetAll(Guid UserId);
        public Task<Experience> Get(Guid id, Guid Userid);


    }
}
