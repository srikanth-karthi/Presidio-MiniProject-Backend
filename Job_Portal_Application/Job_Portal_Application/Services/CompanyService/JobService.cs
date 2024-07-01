using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Dto.SkillDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Application.Services.CompanyService
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IRepository<Guid, Skill> _skillRepository;
        private readonly IJobSkillsRepository _jobSkillsRepository;
        private readonly IRepository<Guid, Title> _titleRepository;
        private readonly IUserService _IUserService;

        public JobService(IUserService  IUserService,IJobRepository jobRepository, IRepository<Guid, Title> titleRepository, ICompanyRepository companyRepository, IRepository<Guid, Skill> skillRepository, IJobSkillsRepository jobSkillsRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _skillRepository = skillRepository;
            _jobSkillsRepository = jobSkillsRepository;
            _titleRepository = titleRepository;
            _IUserService = IUserService;

        }

        public async Task<(JobDto job, List<Guid> notFoundSkills)> AddJob(PostJobDto newJob, Guid companyId)
        {
            _ = await _companyRepository.Get(companyId) ?? throw new CompanyNotFoundException("Company not found.");
            _ = await _titleRepository.Get(newJob.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

            var job = new Job
            {
                CompanyId = companyId,
                TitleId = newJob.TitleId,
                JobDescription = newJob.JobDescription,
                Lpa = newJob.Lpa,
                DatePosted = DateOnly.FromDateTime(DateTime.UtcNow),
                JobType = newJob.JobType,
                ExperienceRequired = newJob.ExperienceRequired,
            };

            var addedJob = await _jobRepository.Add(job);
            var notFoundSkills = new List<Guid>();

            if (newJob.SkillsRequired != null)
            {
                foreach (var skillId in newJob.SkillsRequired)
                {
                    var skill = await _skillRepository.Get(skillId);
                    if (skill == null)
                    {
                        notFoundSkills.Add(skillId);
                    }
                    else
                    {
                        await _jobSkillsRepository.Add(new JobSkills { JobId = addedJob.JobId, SkillId = skillId });
                    }
                }
            }

            var jobDto = MapToJobDto(addedJob);
            return (jobDto, notFoundSkills);
        }

        public async Task<JobDto> UpdateJob(UpdateJobDto jobUpdateDto,Guid companyId)
        {
            var job = await _jobRepository.Get(jobUpdateDto.JobId, companyId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");
            _ = await _titleRepository.Get(jobUpdateDto.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

            job.TitleId = jobUpdateDto.TitleId;
            job.JobDescription = jobUpdateDto.JobDescription;
            job.Lpa = jobUpdateDto.Lpa;
            job.ExperienceRequired = jobUpdateDto.ExperienceRequired;
            job.JobType =  jobUpdateDto.JobType;
            job.Status = jobUpdateDto.Status;

            var updatedJob = await _jobRepository.Update(job);
            return MapToJobDto(updatedJob);
        }

        public async Task<bool> DeleteJob(Guid jobId,Guid companyId)
        {
            return await _jobRepository.Delete(await _jobRepository.Get(jobId, companyId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist."));
        }

        public async Task<JobDto> GetJob(Guid jobId,Guid companyId)
        {

            return MapToJobDto(await _jobRepository.Get(jobId, companyId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist."));

        }



        public async Task<IEnumerable<JobDto>> GetAllJobs(Guid companyId)
        {
            var jobs = await _jobRepository.GetAll( companyId);
            if (!jobs.Any()) throw new JobNotFoundException("No jobs found ");
            return jobs.Select(j => MapToJobDto(j)).ToList();
        }

        public async Task<IEnumerable<JobDto>> GetAllJobsPosted(Guid companyId)
        {
            var jobs = await _jobRepository.GetAllJobsPosted(companyId);
            if (!jobs.Any()) throw new JobNotFoundException("No jobs found for the company.");
            return jobs.Select(j => MapToJobDto(j)).ToList();
        }







        public async Task<SkillsresponseDto> UpdateJobSkills(JobSkillsdto jobSkillsDto,Guid companyId)
        {
            SkillsresponseDto response = new();

            var job = await _jobRepository.Get(jobSkillsDto.JobId, companyId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");
      
            foreach (var skillId in jobSkillsDto.SkillsToAdd)
            {
                var existingJobSkill = await _jobSkillsRepository.GetbyjobIdandSkillId(job.JobId, skillId);


                if (existingJobSkill == null)
                {
                    var skill = await _skillRepository.Get(skillId);
                    if (skill != null)
                    {
                        
                        await _jobSkillsRepository.Add(new JobSkills { JobId = job.JobId, SkillId = skillId });
                        response.AddedSkills.Add(skillId);
                    }
                    else
                    {
                        response.InvalidSkills.Add(skillId);
                    }
                }
          
            }


            foreach (var skillId in jobSkillsDto.SkillsToRemove)
            {
                var existingJobSkill = await _jobSkillsRepository.GetbyjobIdandSkillId(job.JobId,skillId );


              
                    var skill = await _skillRepository.Get(skillId);
                    if (skill != null && existingJobSkill != null)
                    {
                        
                        await _jobSkillsRepository.Delete(existingJobSkill);
                        response.RemovedSkills.Add(skillId);
                    }
                    else
                    {
                        response.InvalidSkills.Add(skillId);
                    }
                

            }
            return response;

        }

        private JobDto MapToJobDto(Job job)
        {
        
            var jobDto = new JobDto
            {
                JobId = job.JobId,
                JobType = job.JobType.ToString(),
                TitleId = job.TitleId,
                CompanyName = job.Company.CompanyName,
                DatePosted = job.DatePosted.ToDateTime(TimeOnly.MinValue),
                TitleName = job.Title?.TitleName,
                Status = job.Status,
                ExperienceRequired = job.ExperienceRequired,
                Lpa = job.Lpa,
                JobDescription = job.JobDescription,
                companylogo = job.Company.LogoUrl
            };

            // Map skills to SkillDto
            jobDto.Skills = job.JobSkills.Select(js => new Skill
            {
                SkillId = js.SkillId,
                SkillName = js.Skill.SkillName 
            }).ToList();

            return jobDto;
        }

        public async Task<List<JobDto>> GetJobs(
    Guid userid,
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
    JobType? jobType = null
)
        {
            var jobs = await _jobRepository.GetJobs(
                pageNumber,
                pageSize,
                JobTitle,
                minLpa,
                maxLpa,
                recentlyPosted,
                skillIds,
                minExperience,
                maxExperience,
                location,
                companyId,
                jobType);

            if (!jobs.Any())
                throw new JobNotFoundException("No jobs found matching the specified criteria.");

            var jobDtos = new List<JobDto>();
            foreach (var job in jobs)
            {
                var jobDto = await MapToJob(job, userid);
                jobDtos.Add(jobDto);
            }

            return jobDtos; // Return the list of job DTOs
        }

        private async Task<JobDto> MapToJob(Job job, Guid UserId)
        {
            var jobDto = new JobDto
            {
                JobId = job.JobId,
                JobType = job.JobType.ToString(),
                TitleId = job.TitleId,
                CompanyName = job.Company.CompanyName,
                DatePosted = job.DatePosted.ToDateTime(TimeOnly.MinValue),
                TitleName = job.Title?.TitleName,
                Status = job.Status,
                jobscrore = await _IUserService.CalculateJobMatchPercentage(job.JobId, UserId),
                ExperienceRequired = job.ExperienceRequired,
                Lpa = job.Lpa,
                JobDescription = job.JobDescription,
                companylogo = job.Company.LogoUrl
            };

            // Map skills to SkillDto
            jobDto.Skills = job.JobSkills.Select(js => new Skill
            {
                SkillId = js.SkillId,
                SkillName = js.Skill.SkillName
            }).ToList();

            return jobDto;
        }


    }
}
