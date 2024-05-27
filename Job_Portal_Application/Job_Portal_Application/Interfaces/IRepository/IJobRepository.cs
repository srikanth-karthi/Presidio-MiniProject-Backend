using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IRepository
{
    public interface IJobRepository : IRepository<Guid, Job>
    {
        Task<Job> Add(Job entity);
        Task<bool> Delete(Job job);
        Task<Job> Get(Guid id);
        Task<IEnumerable<Job>> GetAll();
        Task<Job> Update(Job entity);
        Task<IEnumerable<Job>> GetAllJobsPosted(Guid companyId);
        Task<JobActivity> GetByUserIdAndJobId(Guid userId, Guid jobId);

        Task<IEnumerable<Job>> GetJobs(
  int pageNumber ,
  int pageSize ,
  string title = null,
  float? lpa = null,
  bool recentlyPosted = false,
  IEnumerable<Guid> skillIds = null,
  float? experienceRequired = null,
  string location = null,
  Guid? companyId = null);

    }
}
