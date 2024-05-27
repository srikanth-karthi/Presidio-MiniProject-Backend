using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IJobSkillsRepository: IRepository<Guid, JobSkills>
    {

        public Task UpdateJobSkills(Guid jobId, List<Guid> skillsToAdd, List<Guid> skillsToRemove);
    }
}
