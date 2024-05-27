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
    public class JobSkillsRepository : IJobSkillsRepository
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

        public async Task<bool> Delete(JobSkills jobSkills)
        {
            _context.JobSkills.Remove(jobSkills);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<JobSkills> Get(Guid id)
        {
            return await _context.JobSkills
                .Include(js => js.Job)
                .Include(js => js.Skill)
                .FirstOrDefaultAsync(js => js.JobSkillsId == id);
        }

        public async Task<IEnumerable<JobSkills>> GetAll()
        {
            return await _context.JobSkills
                .Include(js => js.Job)
                .Include(js => js.Skill)
                .ToListAsync();
        }

        public async Task<JobSkills> Update(JobSkills entity)
        {
            _context.JobSkills.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateJobSkills(Guid jobId, List<Guid> skillsToAdd, List<Guid> skillsToRemove)
        {
            foreach (var skillId in skillsToAdd)
            {
                var existingJobSkill = await _context.JobSkills
                    .FirstOrDefaultAsync(js => js.JobId == jobId && js.SkillId == skillId);

                if (existingJobSkill == null)
                {
                    var skill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == skillId);
                    if (skill != null)
                    {
                        await Add(new JobSkills { JobId = jobId, SkillId = skillId });
                    }
                }
            }

            foreach (var skillId in skillsToRemove)
            {
                var jobSkill = await _context.JobSkills.FirstOrDefaultAsync(js => js.JobId == jobId && js.SkillId == skillId);
                if (jobSkill != null)
                {
                    await Delete(jobSkill);
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
