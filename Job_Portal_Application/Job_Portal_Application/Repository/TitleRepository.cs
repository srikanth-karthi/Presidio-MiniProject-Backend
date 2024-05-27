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
    public class TitleRepository : IRepository<Guid, Title>
    {
        private readonly JobportalContext _context;

        public TitleRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Title> Add(Title entity)
        {
            _context.Titles.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Title title)
        {

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Title> Get(Guid id)
        {
            return await _context.Titles.FirstOrDefaultAsync(t => t.TitleId == id);

        }

        public async Task<IEnumerable<Title>> GetAll()
        {
      return await _context.Titles.ToListAsync();

        }

        public async Task<Title> Update(Title entity)
        {

            _context.Titles.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
