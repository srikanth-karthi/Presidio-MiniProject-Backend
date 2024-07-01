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
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services
{
    public class JobActivityService : IJobActivityService
    {
        private readonly IJobActivityRepository _jobActivityRepository;

        private readonly IJobRepository _jobRepository;

        public JobActivityService(IJobRepository jobRepository, IJobActivityRepository jobActivityRepository)
        {
            _jobActivityRepository = jobActivityRepository;
            _jobRepository = jobRepository;
        }

        public async Task<JobActivityDto> ApplyForJob(Guid jobId,Guid companyId)
        {
            var job = await _jobRepository.Get(jobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");

            if (!job.Status)
                throw new JobDisabledException("This Job is not in active state.");

           
            var existingJobActivity = await _jobRepository.GetByUserIdAndJobId(companyId, jobId);
            if (existingJobActivity != null)
                throw new JobAlreadyAppliedException("You have already applied for this job.");

            var jobActivity = await _jobActivityRepository.Add(new JobActivity { UserId = companyId, JobId = jobId });

            return MapToDto(jobActivity);
        }

        public async Task<IEnumerable<UserActivitydto>> GetFilteredUser(Guid companyId, Guid jobId, int pageNumber = 1, int pageSize = 25, bool firstApplied = false, bool perfectMatchSkills = false, bool perfectMatchExperience = false, bool hasExperienceInJobTitle = false)
        {
            var jobActivities = await _jobActivityRepository.GetFilteredUser(companyId, jobId, pageNumber, pageSize, firstApplied, perfectMatchSkills, perfectMatchExperience, hasExperienceInJobTitle);

            if (!jobActivities.Any())
            {
                throw new JobNotFoundException($"No User found for Job ID: {jobId} with the specified filters.");
            }

            return jobActivities.Select(j => MapToUserActivityDto(j));
        }

        private UserActivitydto MapToUserActivityDto(JobActivity jobActivity)
        {
            return new UserActivitydto
            {
                JobactivityId = jobActivity.JobApplicationId,
                UserId = jobActivity.User.UserId,
                Dob = jobActivity.User.Dob.ToDateTime(TimeOnly.MinValue),
                Email = jobActivity.User.Email,
                logourl = jobActivity.User.ProfilePictureUrl,
                Name = jobActivity.User.Name,
                AppliedDate = jobActivity.AppliedDate,
                Status = jobActivity.Status
            };
        }



        public async Task<IEnumerable<UserAppliedJobsDto>> GetJobsUserApplied(Guid UserId)
        {
            var jobActivities = await _jobActivityRepository.GetJobsUserApplied(UserId);
            if (!jobActivities.Any())
                throw new JobNotFoundException("JobActivity does not exist.");
            return jobActivities.Select(j => MapToUserAppliedJobsDto(j));
        }

        public async Task<JobActivityDto> UpdateJobActivityStatus(UpdateJobactivityDto jobactivity)
        {
            var jobActivity = await _jobActivityRepository.Get(jobactivity.JobactivityId) ?? throw new JobActivityNotFoundException("Job activity not found.");
            jobActivity.Status = jobactivity.status;
            jobActivity.UpdatedDate = DateTime.UtcNow;
            jobActivity.ResumeViewed = jobactivity.ResumeViewed;
            jobActivity.Comments = jobactivity.Comments;

            return MapToDto(await _jobActivityRepository.Update(jobActivity));
        }


        public async Task<JobActivityDto> Updateresumestatus(Guid JobactivityId)
        {
            var jobActivity = await _jobActivityRepository.Get(JobactivityId) ?? throw new JobActivityNotFoundException("Job activity not found.");
            jobActivity.ResumeViewed = true;
            jobActivity.UpdatedDate = DateTime.UtcNow;
    

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
                JobactivityId = jobActivity.JobApplicationId,
                UserId = jobActivity.UserId,
                name = jobActivity.User?.Name ?? string.Empty,  // Using string.Empty instead of ""
                logourl = jobActivity.User?.ProfilePictureUrl ?? string.Empty,
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
                logourl=user.ProfilePictureUrl,
                Email = user.Email,
                Dob = user.Dob.ToDateTime(TimeOnly.MinValue),
                Address = user.Address,
                City = user.City,
                PortfolioLink = user.PortfolioLink,
                PhoneNumber = user.Phonenumber,
                ResumeUrl = user.ResumeUrl
            };
        }

        private static UserAppliedJobsDto MapToUserAppliedJobsDto(JobActivity jobActivity)
        {
            return new UserAppliedJobsDto
            {
                JobactivityId = jobActivity.JobApplicationId,
                JobId = jobActivity.JobId,
                
                JobType = jobActivity.Job.JobType.ToString(),
                TitleName = jobActivity.Job.Title.TitleName,
                CompanyName = jobActivity.Job.Company.CompanyName,
                JobStatus = jobActivity.Status.ToString(),
                ResumeViewed = jobActivity.ResumeViewed,
                Applicationstatus = jobActivity.Status.ToString(),
                Comments = jobActivity.Comments,
                AppliedDate = jobActivity.AppliedDate,
                logourl=jobActivity.Job.Company.LogoUrl,
                UpdatedDate=jobActivity.UpdatedDate
            };
        }


    }
}
