using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.EducationDto;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
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
        private readonly IAuthorizeService _authorizeService;

        public JobService(IAuthorizeService authorizeService, IJobRepository jobRepository, IRepository<Guid, Title> titleRepository, ICompanyRepository companyRepository, IRepository<Guid, Skill> skillRepository, IJobSkillsRepository jobSkillsRepository)
        {
            _jobRepository = jobRepository;
            _companyRepository = companyRepository;
            _skillRepository = skillRepository;
            _jobSkillsRepository = jobSkillsRepository;
            _titleRepository = titleRepository;
            _authorizeService = authorizeService;
        }

        public async Task<(JobDto job, List<Guid> notFoundSkills)> AddJob(PostJobDto newJob)
        {
            _ = await _companyRepository.Get(_authorizeService.Gettoken()) ?? throw new CompanyNotFoundException("Company not found.");
            _ = await _titleRepository.Get(newJob.TitleId) ?? throw new TitleNotFoundException("Invalid TitleId. Title does not exist.");

            var job = new Job
            {
                CompanyId = _authorizeService.Gettoken(),
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

        public async Task<JobDto> UpdateJob(UpdateJobDto jobUpdateDto)
        {
            var job = await _jobRepository.Get(jobUpdateDto.JobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");
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

        public async Task<bool> DeleteJob(Guid jobId)
        {
            return await _jobRepository.Delete(await _jobRepository.Get(jobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist."));
        }

        public async Task<JobDto> GetJob(Guid jobId)
        {



            return MapToJobDto(await _jobRepository.Get(jobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist."));

        }



        public async Task<IEnumerable<JobDto>> GetAllJobs()
        {
            var jobs = await _jobRepository.GetAll();
            if (!jobs.Any()) throw new JobNotFoundException("No jobs found ");
            return jobs.Select(j => MapToJobDto(j)).ToList();
        }

        public async Task<IEnumerable<JobDto>> GetAllJobsPosted(Guid companyId)
        {
            var jobs = await _jobRepository.GetAllJobsPosted(companyId);
            if (!jobs.Any()) throw new JobNotFoundException("No jobs found for the company.");
            return jobs.Select(j => MapToJobDto(j)).ToList();
        }







        public async Task UpdateJobSkills(JobSkillsDto jobSkillsDto)
        {
            var job = await _jobRepository.Get(jobSkillsDto.JobId) ?? throw new JobNotFoundException("Invalid JobId. Job does not exist.");

            await _jobSkillsRepository.UpdateJobSkills(jobSkillsDto.JobId, jobSkillsDto.SkillsToAdd, jobSkillsDto.SkillsToRemove);
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
        public async Task<IEnumerable<JobDto>> GetJobs(
            int pageNumber,
            int pageSize ,
            string title = null,
            float? lpa = null,
            bool recentlyPosted = false,
            IEnumerable<Guid> skillIds = null,
            float? experienceRequired = null,
            string location = null,
            Guid? companyId = null)
        {
            var jobs = await _jobRepository.GetJobs(
                pageNumber,
                pageSize,
                title,
                lpa,
                recentlyPosted,
                skillIds,
                experienceRequired,
                location,
                companyId);

            if (!jobs.Any())
                throw new JobNotFoundException("No jobs found matching the specified criteria.");

            return jobs.Select(j => MapToJobDto(j)).ToList();
        }

    }
}
