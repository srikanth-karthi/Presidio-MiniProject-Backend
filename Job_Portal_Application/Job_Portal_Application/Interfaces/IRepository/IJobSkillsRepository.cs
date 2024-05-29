using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IJobSkillsRepository: IRepository<Guid, JobSkills>
    {


        Task<JobSkills> GetbyjobIdandSkillId(Guid jobId, Guid skillId);
    }
}
