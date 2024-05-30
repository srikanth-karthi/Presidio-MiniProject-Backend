using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Dto.SkillDtos;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IJobService
    {
        Task<(JobDto job, List<Guid> notFoundSkills)> AddJob(PostJobDto newJob, Guid companyId);
        Task<JobDto> UpdateJob(UpdateJobDto jobUpdateDto, Guid companyId);
        Task<bool> DeleteJob(Guid jobId, Guid companyId);
        Task<JobDto> GetJob(Guid jobId, Guid companyId);
        Task<IEnumerable<JobDto>> GetAllJobs();
        Task<IEnumerable<JobDto>> GetAllJobsPosted(Guid companyId);
        Task<SkillsresponseDto> UpdateJobSkills(JobSkillsdto jobSkillsDto, Guid companyId);
        Task<IEnumerable<JobDto>> GetJobs(
            int pageNumber ,
            int pageSize ,
            Guid ? JobTitle=null,
            float? lpa = null,
            bool recentlyPosted = false,
            IEnumerable<Guid> skillIds = null,
            float? experienceRequired = null,
            string location = null,
            Guid? companyId = null);
    }
}
