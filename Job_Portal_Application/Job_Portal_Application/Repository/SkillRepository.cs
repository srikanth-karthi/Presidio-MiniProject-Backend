using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;

namespace Job_Portal_Application.Repository.SkillRepos
{
    public class SkillRepository : IRepository<Guid, Skill>
    {
        private readonly JobportalContext _context;

        public SkillRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Skill> Add(Skill entity)
        {
            _context.Skills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Skill skill)
        {
          
 
                _context.Skills.Remove(skill);
                await _context.SaveChangesAsync();
            return true;

        }

        public async Task<Skill> Get(Guid id)
        {
           return  await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == id);
         
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            return  await _context.Skills.ToListAsync();

        }

        public async Task<Skill> Update(Skill entity)
        {

            _context.Skills.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
