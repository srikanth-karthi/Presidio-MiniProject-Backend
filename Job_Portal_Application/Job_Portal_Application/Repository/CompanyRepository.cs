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
    public class CompanyRepository : IRepository<Guid, Company>
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

        public async Task<bool> Delete(Guid id)
        {
            var company = await Get(id);
            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Company> Get(Guid id)
        {
            var company = await _context.Companies
                .FirstOrDefaultAsync(c => c.CompanyId == id);

            if (company != null)
                return company;

            throw new CompanyNotFoundException();
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            var companies = await _context.Companies.ToListAsync();
            if (companies.Count == 0)
                throw new CompanyNotFoundException();

            return companies;
        }

        public async Task<Company> Update(Company entity)
        {
            var company = await Get(entity.CompanyId);
            _context.Companies.Update(entity);
            await _context.SaveChangesAsync();
            return company;
        }
    }
}
