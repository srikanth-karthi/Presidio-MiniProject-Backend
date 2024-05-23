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
    public class AreasOfInterestRepository : IRepository<Guid, AreasOfInterest>
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

        public async Task<bool> Delete(Guid id)
        {
            var areaOfInterest = await Get(id);
            _context.AreasOfInterests.Remove(areaOfInterest);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AreasOfInterest> Get(Guid id)
        {
            var areaOfInterest = await _context.AreasOfInterests
                .Include(a => a.Title)
                .FirstOrDefaultAsync(a => a.AreasOfInterestId == id);

            if (areaOfInterest != null)
                return areaOfInterest;

            throw new AreasOfInterestNotFoundException();
        }

        public async Task<IEnumerable<AreasOfInterest>> GetAll()
        {
            var areasOfInterestList = await _context.AreasOfInterests
                .Include(a => a.Title)
                .ToListAsync();

            if (areasOfInterestList.Count == 0)
                throw new AreasOfInterestNotFoundException();

            return areasOfInterestList;
        }

        public async Task<AreasOfInterest> Update(AreasOfInterest entity)
        {
            var areaOfInterest = await Get(entity.AreasOfInterestId);
            _context.AreasOfInterests.Update(entity);
            await _context.SaveChangesAsync();
            return areaOfInterest;
        }
    }
}
