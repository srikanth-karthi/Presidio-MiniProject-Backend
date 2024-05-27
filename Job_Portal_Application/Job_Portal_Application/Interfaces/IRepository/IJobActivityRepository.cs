using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IJobActivityRepository : IRepository<Guid, JobActivity>
    {
        Task<IEnumerable<JobActivity>> GetAllAppliedUsers(Guid companyId, Guid jobId);

        Task<IEnumerable<JobActivity>> GetFilteredUser(
            Guid companyId,
            Guid jobId,
            int pageNumber = 1,
            int pageSize = 25,
            bool firstApplied = false,
            bool perfectMatchSkills = false,
            bool perfectMatchExperience = false,
            bool hasExperienceInJobTitle = false);

        Task<IEnumerable<JobActivity>> GetJobsUserApplied(Guid userid);
        Task<IEnumerable<JobActivity>> GetJobActivitiesByJobId(Guid jobId);
    }
}
