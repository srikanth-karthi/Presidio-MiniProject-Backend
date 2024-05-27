using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IUserSkillsRepository : IRepository<Guid, UserSkills>
    {

        Task<IEnumerable<UserSkills>> GetByUserId(Guid userId);
        Task<UserSkills> GetByUserIdAndSkillId(Guid userId, Guid skillId);
    }
}
