using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;

namespace Job_Portal_Application.Repository.UserRepos
{
    public class ExperienceRepository : IExperienceRepository
    {
        private readonly JobportalContext _context;

        public ExperienceRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Experience> Add(Experience entity)
        {


            _context.Experiences.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Experience experience)
        {
        
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Experience> Get(Guid id)
        {
            return await _context.Experiences.Include(job => job.Title).FirstOrDefaultAsync(e => e.ExperienceId == id);

        }
        public async Task<Experience> Get(Guid id,Guid Userid)
        {
            return await _context.Experiences.Include(job => job.Title).FirstOrDefaultAsync(e => e.ExperienceId == id && e.UserId==Userid);

        }

        public async Task<IEnumerable<Experience>> GetAll()
        {
            return await _context.Experiences.Include(job => job.Title).ToListAsync();
 
        }

        public async Task<IEnumerable<Experience>> GetAll(Guid UserId)
        {
     return await _context.Experiences
                .Where(e => e.UserId == UserId)
                .Include(e => e.Title) 
                .ToListAsync();



   
        }

        public async Task<Experience> Update(Experience entity)
        {
  

            _context.Experiences.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
