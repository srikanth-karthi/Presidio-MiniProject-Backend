using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services
{
    public class JobActivityService : IJobActivityService
    {
        private readonly IJobActivityRepository _jobActivityRepository;
        private readonly IAuthorizeService _authorizeService;
        private readonly IJobRepository _jobRepository;

        public JobActivityService(IJobRepository jobRepository, IJobActivityRepository jobActivityRepository, IAuthorizeService authorizeService)
        {
            _jobActivityRepository = jobActivityRepository;
            _authorizeService = authorizeService;
            _jobRepository = jobRepository;
        }

        public async Task<JobActivityDto> ApplyForJob(Guid jobId)
        {
            var job = await _jobRepository.Get(jobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");

            if (!job.Status)
                throw new JobDisabledException("This Job is not in active state.");

            var userId = _authorizeService.Gettoken();
            var existingJobActivity = await _jobRepository.GetByUserIdAndJobId(userId, jobId);
            if (existingJobActivity != null)
                throw new JobAlreadyAppliedException("You have already applied for this job.");

            var jobActivity = await _jobActivityRepository.Add(new JobActivity { UserId = userId, JobId = jobId });

            return MapToDto(jobActivity);
        }

        public async Task<IEnumerable<UserDto>> GetFilteredUser(Guid jobId, int pageNumber = 1, int pageSize = 25, bool firstApplied = false, bool perfectMatchSkills = false, bool perfectMatchExperience = false, bool hasExperienceInJobTitle = false)
        {
            var jobActivities = await _jobActivityRepository.GetFilteredUser(_authorizeService.Gettoken(), jobId, pageNumber, pageSize, firstApplied, perfectMatchSkills, perfectMatchExperience, hasExperienceInJobTitle);
            if (!jobActivities.Any())
                throw new JobNotFoundException("JobActivity does not exist.");
            return jobActivities.Select(j => MapToUserDto(j.User));
        }

        public async Task<IEnumerable<JobDto>> GetJobsUserApplied()
        {
            var jobActivities = await _jobActivityRepository.GetJobsUserApplied(_authorizeService.Gettoken());
            if (!jobActivities.Any())
                throw new JobNotFoundException("JobActivity does not exist.");
            return jobActivities.Select(j => MapToJobDto(j.Job));
        }

        public async Task<JobActivityDto> UpdateJobActivityStatus(UpdateJobactivityDto jobactivity)
        {
            var jobActivity = await _jobActivityRepository.Get(jobactivity.JobactivityId) ?? throw new JobActivityNotFoundException("Job activity not found.");
            jobActivity.Status = jobactivity.status;
            jobActivity.UpdatedDate = DateTime.UtcNow;
            jobActivity.ResumeViewed = true;

            return MapToDto(await _jobActivityRepository.Update(jobActivity));
        }

        public async Task<JobActivityDto> GetJobActivityById(Guid jobActivityId)
        {
            var jobActivity = await _jobActivityRepository.Get(jobActivityId) ?? throw new JobActivityNotFoundException("Job activity not found.");
            return MapToDto(jobActivity);
        }

        public async Task<IEnumerable<JobActivityDto>> GetJobActivitiesByJobId(Guid jobId)
        {
            var jobActivities = await _jobActivityRepository.GetJobActivitiesByJobId(jobId);
            if (!jobActivities.Any())
                throw new JobNotFoundException("No job activities found for the specified job.");

            return jobActivities.Select(j => MapToDto(j));
        }


        private JobActivityDto MapToDto(JobActivity jobActivity)
        {
            return new JobActivityDto
            {
                UserJobId = jobActivity.UserJobId,
                UserId = jobActivity.UserId,
                JobId = jobActivity.JobId,
                Status = Enum.GetName(typeof(JobStatus), jobActivity.Status),  
                ResumeViewed = jobActivity.ResumeViewed,
                Comments = jobActivity.Comments,
                AppliedDate = jobActivity.AppliedDate,
                UpdatedDate = jobActivity.UpdatedDate
            };
        }


        private static UserDto MapToUserDto(User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Dob = user.Dob.ToDateTime(TimeOnly.MinValue),
                Address = user.Address,
                City = user.City,
                PortfolioLink = user.PortfolioLink,
                PhoneNumber = user.Phonenumber,
                ResumeUrl = user.ResumeUrl
            };
        }


        private JobDto MapToJobDto(Job job)
        {
            return new JobDto
            {
                JobId = job.JobId,
                JobType = Enum.GetName(typeof(JobType), job.JobType),
    
                TitleId = job.TitleId,
                CompanyName = job.Company.CompanyName,
                DatePosted = job.DatePosted.ToDateTime(TimeOnly.MinValue),
                TitleName = job.Title?.TitleName,
                Status = job.Status,
                ExperienceRequired = job.ExperienceRequired,
                Lpa = job.Lpa,
                JobDescription = job.JobDescription,
                Skills = job.JobSkills.Select(js => js.Skill.Skill_Name).ToList()
            };
        }
    }
}
