using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;

namespace Job_Portal_Application.Repository
{
    public class CredentialRepository : IRepository<Guid, Credential>
    {
        private readonly JobportalContext _context;

        public CredentialRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Credential> Add(Credential entity)
        {

            _context.Credential.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Credential credential)
        {
            _context.Credential.Remove(credential);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Credential> Get(Guid id)
        {
            return await _context.Credential
                .FirstOrDefaultAsync(c => c.CredentialId == id);
        }



        public async Task<IEnumerable<Credential>> GetAll()
        {
            return await _context.Credential.ToListAsync();
        }

        public async Task<Credential> Update(Credential entity)
        {
            _context.Credential.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
