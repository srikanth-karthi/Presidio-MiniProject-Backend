using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IJobRepository : IRepository<Guid, Job>
    {
        Task<Job> Add(Job entity);
        Task<bool> Delete(Job job);
        Task<Job> Get(Guid id);
        Task<IEnumerable<Job>> GetAll(Guid companyId);
        Task<Job> Update(Job entity);
        Task<IEnumerable<Job>> GetAllJobsPosted(Guid companyId);
        public Task<Job> Get(Guid id, Guid companyId);
        Task<JobActivity> GetByUserIdAndJobId(Guid userId, Guid jobId);

        Task<IEnumerable<Job>> GetJobs(
  int pageNumber = 1,
               int pageSize = 25,
               Guid? JobTitle = null,
               float? minLpa = null,
               float? maxLpa = null,
               bool recentlyPosted = true,
               IEnumerable<Guid> skillIds = null,
               float? minExperience = null,
               float? maxExperience = null,
               string location = null,
               Guid? companyId = null,
               JobType? jobType = null);

    }
}
