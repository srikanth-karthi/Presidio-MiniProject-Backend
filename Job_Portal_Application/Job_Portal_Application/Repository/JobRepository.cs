using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Repository
{
    public class JobRepository : IRepository<Guid, Job>
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

        public async Task<bool> Delete(Guid id)
        {
            var job = await Get(id);
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Job> Get(Guid id)
        {
            var job = await _context.Jobs
                .Include(j => j.Company)
                .Include(job => job.Title)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(j => j.JobId == id);

            if (job != null)
                return job;

            throw new JobNotFoundException();
        }

        public async Task<IEnumerable<Job>> GetAll()
        {
            var jobs = await _context.Jobs
                .Include(j => j.Company)
                     .Include(job => job.Title)
                .Include(j => j.JobSkills).ThenInclude(js => js.Skill)
                .ToListAsync();

            if (jobs.Count == 0)
                throw new JobNotFoundException();

            return jobs;
        }

        public async Task<Job> Update(Job entity)
        {
            var job = await Get(entity.JobId);
            _context.Jobs.Update(entity);
            await _context.SaveChangesAsync();
            return job;
        }
    }
}
