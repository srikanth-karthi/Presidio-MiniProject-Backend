using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;

namespace Job_Portal_Application.Repository.CompanyRepos
{
    public class JobRepository :IJobRepository
    {
        private readonly JobportalContext _context;

        public JobRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Job> Add(Job entity)
        {
            _context.Jobs.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Job job)
        {
        
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Job> Get(Guid id)
        {
           return  await _context.Jobs
                .Include(j => j.Company)
                .Include(job => job.Title)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.JobId == id);

        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            return  await _context.Jobs
                .Include(j => j.Company)
                     .Include(job => job.Title)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .ToListAsync();


        }

        public async Task<Job> Update(Job entity)
        {
        
            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<IEnumerable<Job>> GetAllJobsPosted(Guid companyId)
        {
           return  await _context.Jobs
                .Include(job => job.Title)
                .Include(job => job.JobSkills)
                    .ThenInclude(js => js.Skill)
                .Where(job => job.CompanyId == companyId)
                .ToListAsync();


        }
        public async Task<JobActivity> GetByUserIdAndJobId(Guid userId, Guid jobId)
        {
            return await _context.JobActivities
                .FirstOrDefaultAsync(ja => ja.UserId == userId && ja.JobId == jobId);
        }
        public async Task<IEnumerable<Job>> GetJobs(
    int pageNumber = 1,
    int pageSize = 25,
    string title = null,
    float? lpa = null,
    bool recentlyPosted = false,
    IEnumerable<Guid> skillIds = null,
    float? experienceRequired = null,
    string location = null,
    Guid? companyId = null)
        {
            var query = _context.Jobs
             .Include(j => j.Company)
        .Include(job => job.Title)
        .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
        .Where(job => job.Status== true) 
        .AsQueryable();


            if (companyId.HasValue)
            {
                query = query.Where(j => j.CompanyId == companyId.Value);
            }

            if (!string.IsNullOrEmpty(title))
            {
                query = query.Where(job => job.Title.TitleName.Contains(title));
            }

            if (lpa.HasValue)
            {
                query = query.Where(job => job.Lpa == lpa.Value);
            }

            if (recentlyPosted)
            {
                query = query.OrderByDescending(job => job.DatePosted);
            }

            if (skillIds != null && skillIds.Any())
            {
                query = query.Where(job => job.JobSkills.Any(js => skillIds.Contains(js.SkillId)));
            }

            if (experienceRequired.HasValue)
            {
                query = query.Where(job => job.ExperienceRequired <= experienceRequired.Value);
            }

            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(job => job.Company.City.Contains(location));
            }

            var jobs = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        

            return jobs;
        }

    }
}
