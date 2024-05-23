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
    public class JobSkillsRepository : IRepository<Guid, JobSkills>
    {
        private readonly JobportalContext _context;

        public JobSkillsRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<JobSkills> Add(JobSkills entity)
        {
            _context.JobSkills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var jobSkills = await Get(id);
            _context.JobSkills.Remove(jobSkills);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobSkills> Get(Guid id)
        {
            var jobSkills = await _context.JobSkills
                .Include(js => js.Job)
                .Include(js => js.Skill)
                .FirstOrDefaultAsync(js => js.JobSkillsId == id);

            if (jobSkills != null)
                return jobSkills;

            throw new JobSkillsNotFoundException();
        }

        public async Task<IEnumerable<JobSkills>> GetAll()
        {
            var jobSkillsList = await _context.JobSkills
                .Include(js => js.Job)
                .Include(js => js.Skill)
                .ToListAsync();

            if (jobSkillsList.Count == 0)
                throw new JobSkillsNotFoundException();

            return jobSkillsList;
        }

        public async Task<JobSkills> Update(JobSkills entity)
        {
            var jobSkills = await Get(entity.JobSkillsId);
            _context.JobSkills.Update(entity);
            await _context.SaveChangesAsync();
            return jobSkills;
        }
    }
}
