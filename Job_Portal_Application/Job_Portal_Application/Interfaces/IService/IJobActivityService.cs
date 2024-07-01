using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IJobActivityService
    {
        Task<JobActivityDto> ApplyForJob(Guid jobId, Guid companyId);
        Task<IEnumerable<UserActivitydto>> GetFilteredUser(Guid companyId, Guid jobId, int pageNumber = 1, int pageSize = 25, bool firstApplied = false, bool perfectMatchSkills = false, bool perfectMatchExperience = false, bool hasExperienceInJobTitle = false);
        Task<IEnumerable<UserAppliedJobsDto>> GetJobsUserApplied(Guid companyId);
        Task<JobActivityDto> UpdateJobActivityStatus(UpdateJobactivityDto updateJobactivityDto);
        Task<JobActivityDto> GetJobActivityById(Guid jobActivityId);
        Task<IEnumerable<JobActivityDto>> GetJobActivitiesByJobId(Guid jobId);
        Task<JobActivityDto> Updateresumestatus(Guid JobactivityId);
    }
}
