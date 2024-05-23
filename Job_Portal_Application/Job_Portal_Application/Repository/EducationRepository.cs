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
    public class EducationRepository : IRepository<Guid, Education>
    {
        private readonly JobportalContext _context;

        public EducationRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Education> Add(Education entity)
        {
            _context.Educations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var education = await Get(id);
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Education> Get(Guid id)
        {
            var education = await _context.Educations.FirstOrDefaultAsync(e => e.EducationId == id);
            if (education != null)
                return education;

            throw new EducationNotFoundException();
        }

        public async Task<IEnumerable<Education>> GetAll()
        {
            var educations = await _context.Educations.ToListAsync();
            if (educations.Count == 0)
                throw new EducationNotFoundException();

            return educations;
        }

        public async Task<Education> Update(Education entity)
        {
            var education = await Get(entity.EducationId);
            _context.Educations.Update(entity);
            await _context.SaveChangesAsync();
            return education;
        }
    }
}
