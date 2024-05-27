using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IJobService
    {
        Task<(JobDto job, List<Guid> notFoundSkills)> AddJob(PostJobDto newJob);
        Task<JobDto> UpdateJob(UpdateJobDto jobUpdateDto);
        Task<bool> DeleteJob(Guid jobId);
        Task<JobDto> GetJob(Guid jobId);
        Task<IEnumerable<JobDto>> GetAllJobs();
        Task<IEnumerable<JobDto>> GetAllJobsPosted(Guid companyId);
        Task UpdateJobSkills(JobSkillsDto jobSkillsDto);
        Task<IEnumerable<JobDto>> GetJobs(
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
