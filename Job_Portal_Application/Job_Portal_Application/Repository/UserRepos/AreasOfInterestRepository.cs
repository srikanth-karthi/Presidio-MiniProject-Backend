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
    public class AreasOfInterestRepository : IAreasOfInterestRepository
    {
        private readonly JobportalContext _context;

        public AreasOfInterestRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<AreasOfInterest> Add(AreasOfInterest entity)
        {

            _context.AreasOfInterests.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<bool> Delete(AreasOfInterest areaOfInterest)
        {

            _context.AreasOfInterests.Remove(areaOfInterest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AreasOfInterest> Get(Guid id)
        {
            return await _context.AreasOfInterests.Include(a => a.Title)
                .FirstOrDefaultAsync(a => a.AreasOfInterestId == id);
        }

        public async Task<AreasOfInterest> Get(Guid id,Guid UserId)
        {
            return await _context.AreasOfInterests.Include(a => a.Title)
                .FirstOrDefaultAsync(a => a.AreasOfInterestId == id && a.UserId==UserId);
        }

        public async Task<IEnumerable<AreasOfInterest>> GetAll()
        {
            return await _context.AreasOfInterests
                .Include(a => a.Title)
                .ToListAsync();

;
        }

        public async Task<IEnumerable<AreasOfInterest>> GetAll(Guid UserId)
        {
           return await _context.AreasOfInterests
                .Where(e => e.UserId == UserId)
                 .Include(a => a.Title)
                 .ToListAsync();

        }

        public async Task<AreasOfInterest> Update(AreasOfInterest entity)
        {

            _context.AreasOfInterests.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

    }
}
