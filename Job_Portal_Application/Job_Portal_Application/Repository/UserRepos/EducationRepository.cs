using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Dto.EducationDtos;

namespace Job_Portal_Application.Repository.UserRepos
{
    public class EducationRepository : IEducationRepository
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

        public async Task<bool> Delete(Education entity)
        {
   
            _context.Educations.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Education> Get(Guid id)
        {
           return await _context.Educations.FirstOrDefaultAsync(e => e.EducationId == id);

        }
        public async Task<Education> Get(Guid id, Guid UserId)
        {
            return await _context.Educations.FirstOrDefaultAsync(e => e.EducationId == id && e.UserId == UserId);
        }


        public async Task<IEnumerable<Education>> GetAll()
        {
            var educations = await _context.Educations.ToListAsync();

            return educations;
        }

        public async Task<IEnumerable<Education>> GetAll(Guid UserId)
        {
            var educations = await _context.Educations.Where(e => e.UserId == UserId).ToListAsync();

            return educations;
        }


        public async Task<Education> Update(Education entity)
        {

            _context.Educations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
