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
    public class ExperienceRepository : IRepository<Guid, Experience>
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

        public async Task<bool> Delete(Guid id)
        {
            var experience = await Get(id);
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Experience> Get(Guid id)
        {
            var experience = await _context.Experiences.Include(job => job.Title).FirstOrDefaultAsync(e => e.ExperienceId == id);
            if (experience != null)
                return experience;

            throw new ExperienceNotFoundException();
        }

        public async Task<IEnumerable<Experience>> GetAll()
        {
            var experiences = await _context.Experiences.Include(job => job.Title).ToListAsync();
            if (experiences.Count == 0)
                throw new ExperienceNotFoundException();

            return experiences;
        }

        public async Task<Experience> Update(Experience entity)
        {
            var experience = await Get(entity.ExperienceId);
            _context.Experiences.Update(entity);
            await _context.SaveChangesAsync();
            return experience;
        }
    }
}
