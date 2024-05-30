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
    public class CompanyRepository : ICompanyRepository
    {
      

    private readonly JobportalContext _context;

        public CompanyRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<Company> Add(Company entity)
        {
            _context.Companies.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Company company)
        {
       
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Company> GetByEmail(string email)
        {
         return  await _context.Companies.FirstOrDefaultAsync(u => u.Email == email);

        }
        public async Task<Company> Get(Guid id)
        {
       return await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == id);

        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return  await _context.Companies.ToListAsync();

        }

        public async Task<Company> Update(Company entity)
        {
     
            _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


       
    }
}
