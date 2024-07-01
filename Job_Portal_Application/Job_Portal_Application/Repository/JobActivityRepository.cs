using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Interfaces.IRepository;

namespace Job_Portal_Application.Repository
{
    public class JobActivityRepository : IJobActivityRepository
    {
        private readonly JobportalContext _context;

        public JobActivityRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<JobActivity> Add(JobActivity entity)
        {
            _context.JobActivities.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(JobActivity entity)
        {
         
            _context.JobActivities.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task<JobActivity> Get(Guid id)
        {
           return  await _context.JobActivities
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .FirstOrDefaultAsync(ja => ja.JobApplicationId == id);


        }

        public async Task<IEnumerable<JobActivity>> GetAll()
        {
           return await _context.JobActivities
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .ToListAsync();


        }

        public async Task<JobActivity> Update(JobActivity entity)
        {
         
            _context.JobActivities.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<JobActivity>> GetAllAppliedUsers(Guid companyId, Guid jobId)
        {
            return await _context.JobActivities
                .Include(ja => ja.User)
                .Where(ja => ja.JobId == jobId && ja.Job.CompanyId == companyId)
                .OrderByDescending(ja => ja.AppliedDate)
                .ToListAsync();
        }


        public async Task<IEnumerable<JobActivity>> GetFilteredUser(
              Guid companyId,
            Guid jobId,
            int pageNumber = 1,
            int pageSize = 25,
            bool firstApplied = false,
            bool perfectMatchSkills = false,
            bool perfectMatchExperience = false,
            bool hasExperienceInJobTitle = false)
        {
            var query = _context.JobActivities
                .Include(ja => ja.User)
                    .ThenInclude(u => u.UserSkills)
                        .ThenInclude(us => us.Skill)
                .Include(ja => ja.User)
                    .ThenInclude(u => u.Experiences)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Title)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.JobSkills)
                        .ThenInclude(js => js.Skill)
                .Where(ja => ja.JobId == jobId && ja.Job.CompanyId == companyId)
                .AsQueryable();

            if (firstApplied)
            {
                query = query.OrderBy(ja => ja.AppliedDate);
            }

            if (perfectMatchSkills)
            {
                query = query.Where(ja =>
                    ja.Job.JobSkills.All(js => ja.User.UserSkills.Any(us => us.SkillId == js.SkillId)));
            }

            if (perfectMatchExperience)
            {
                query = query.Where(ja =>
                    _context.Experiences.Any(e =>
                        e.UserId == ja.UserId &&
                        e.TitleId == ja.Job.TitleId &&
                        (e.EndYear.Year - e.StartYear.Year) >= ja.Job.ExperienceRequired));
            }

            if (hasExperienceInJobTitle)
            {
                query = query.Where(ja =>
                    _context.Experiences.Any(e =>
                        e.UserId == ja.UserId &&
                        e.TitleId == ja.Job.TitleId));
            }

            var jobActivities = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return jobActivities;
        }


        public async Task<IEnumerable<JobActivity>> GetJobsUserApplied(Guid userId)
        {
            return await _context.JobActivities
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Company)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.Title)
                .Include(ja => ja.Job)
                    .ThenInclude(j => j.JobSkills)
                        .ThenInclude(js => js.Skill)
                .Where(ja => ja.UserId == userId)
                .OrderByDescending(ja => ja.AppliedDate) 
                .ToListAsync();
        }



        public async Task<IEnumerable<JobActivity>> GetJobActivitiesByJobId(Guid jobId)
        {
            return await _context.JobActivities
                .Include(ja => ja.User)
                .Where(ja => ja.JobId == jobId)
                .ToListAsync();
        }


    }
}
