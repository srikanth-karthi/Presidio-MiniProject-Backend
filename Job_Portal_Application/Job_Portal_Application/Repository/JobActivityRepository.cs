using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces;
using Job_Portal_Application.Models;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Application.Repository
{
    public class JobActivityRepository : IRepository<Guid, JobActivity>
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

        public async Task<bool> Delete(Guid id)
        {
            var jobActivity = await Get(id);
            _context.JobActivities.Remove(jobActivity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobActivity> Get(Guid id)
        {
            var jobActivity = await _context.JobActivities
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .FirstOrDefaultAsync(ja => ja.UserJobId == id);

            if (jobActivity != null)
                return jobActivity;

            throw new JobActivityNotFoundException();
        }

        public async Task<IEnumerable<JobActivity>> GetAll()
        {
            var jobActivities = await _context.JobActivities
                .Include(ja => ja.User)
                .Include(ja => ja.Job)
                .ToListAsync();

            if (jobActivities.Count == 0)
                throw new JobActivityNotFoundException();

            return jobActivities;
        }

        public async Task<JobActivity> Update(JobActivity entity)
        {
            var jobActivity = await Get(entity.UserJobId);
            _context.JobActivities.Update(entity);
            await _context.SaveChangesAsync();
            return jobActivity;
        }
    }
}
