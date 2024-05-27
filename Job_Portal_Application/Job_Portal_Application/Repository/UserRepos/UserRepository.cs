using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Repository.UserRepos
{
    public class UserRepository : IUserRepository
    {
        private readonly JobportalContext _context;

        public UserRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<User> Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(User user)
        {
  
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;

        }

        public async Task<User> Get(Guid id)
        {
       return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();

        }

        public async Task<User> Update(User entity)
        {
       
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        }

        public async Task<User> GetUserProfile(Guid id)
        {
           return await _context.Users
                .Include(u => u.Educations)
                .Include(u => u.Experiences)
                .Include(u => u.UserSkills).ThenInclude(js => js.Skill)
                .Include(u => u.AreasOfInterests)
                .FirstOrDefaultAsync(u => u.UserId == id);

        }

        public async Task<JobActivity> GetJob(Guid id)
        {
            return await _context.JobActivities
               .Include(ja => ja.Job)
                    .ThenInclude(job => job.Title)
                .Include(ja => ja.Job)
                    .ThenInclude(job => job.Company)
                .Include(ja => ja.Job)
                    .ThenInclude(job => job.JobSkills)
                        .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(ja => ja.UserJobId == id);

        }
        public async Task<IEnumerable<JobActivity>> GetAllJobs(Guid UserId)
        {
            return  await _context.JobActivities
                .Include(ja => ja.Job)
                    .ThenInclude(job => job.Title)
                .Include(ja => ja.Job)
                    .ThenInclude(job => job.Company)
                .Include(ja => ja.Job)
                    .ThenInclude(job => job.JobSkills)
                        .ThenInclude(js => js.Skill)
                .Where(ja => ja.UserId == UserId)
                .ToListAsync();

        }


public async Task<IEnumerable<Job>> GetRecommendedJobsForUser(Guid userId, int pageNumber, int pageSize)
    {
        var userAreasOfInterest = await _context.AreasOfInterests
            .Where(a => a.UserId == userId)
            .Include(a => a.Title)
            .ToListAsync();

        var titleIds = userAreasOfInterest.Select(a => a.TitleId).ToList();
        var lpas = userAreasOfInterest.Select(a => a.Lpa).ToList();

        var jobs = await _context.Jobs
            .Include(j => j.Company)
            .Include(j => j.Title)
            .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
               .Where(job => job.Status == true)
            .Where(j => titleIds.Contains(j.TitleId) && lpas.Contains(j.Lpa ?? -1))
            .ToListAsync();

        var recommendedJobs = jobs
            .OrderBy(j => j.DatePosted.ToDateTime(TimeOnly.MinValue))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        if (recommendedJobs.Count == 0)
        {
            var fallbackJobs = await _context.Jobs
                .Include(j => j.Company)
                .Include(j => j.Title)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                     .Where(job => job.Status == true)
                .ToListAsync();

          return  fallbackJobs
                .OrderBy(j => j.DatePosted.ToDateTime(TimeOnly.MinValue))
           .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                     .ToList();


                
       
        }

            return recommendedJobs;
    }




}
}

