using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.CompanyRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Job_Portal_Application_Test.CompanyReposTest
{
    [TestFixture]
    public class CompanyRepositoryTest
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private CompanyRepository _companyRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _companyRepo = new CompanyRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddCompany()
        {
            var company = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            var result = await _companyRepo.Add(company);

            Assert.IsNotNull(result);
            Assert.AreEqual(company.CompanyId, result.CompanyId);
            Assert.AreEqual(1, await _context.Companies.CountAsync());
        }

        [Test]
        public async Task DeleteCompany()
        {
            var company = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            await _companyRepo.Add(company);

            var result = await _companyRepo.Delete(company);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.Companies.CountAsync());
        }

        [Test]
        public async Task GetCompanyById()
        {
            var company = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            await _companyRepo.Add(company);

            var result = await _companyRepo.Get(company.CompanyId);

            Assert.IsNotNull(result);
            Assert.AreEqual(company.CompanyId, result.CompanyId);
        }

        [Test]
        public async Task GetCompanyByEmail()
        {
            var company = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            await _companyRepo.Add(company);

            var result = await _companyRepo.GetByEmail(company.Email);

            Assert.IsNotNull(result);
            Assert.AreEqual(company.Email, result.Email);
        }

        [Test]
        public async Task GetAllCompanies()
        {
            var company1 = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            var company2 = new Company
            {
                CompanyName = "Innovate Inc",
                Email = "hello@innovateinc.com",
                Password = new byte[] { 7, 8, 9 },
                HasCode = new byte[] { 10, 11, 12 },
                CompanyAddress = "456 Innovation Drive",
                City = "Innovation City",
                CompanySize = 200,
                CompanyWebsite = "https://www.innovateinc.com"
            };

            await _companyRepo.Add(company1);
            await _companyRepo.Add(company2);

            var result = await _companyRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateCompany()
        {
            var company = new Company
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                Password = new byte[] { 1, 2, 3 },
                HasCode = new byte[] { 4, 5, 6 },
                CompanyAddress = "123 Tech Lane",
                City = "Tech City",
                CompanySize = 100,
                CompanyWebsite = "https://www.techcorp.com"
            };

            await _companyRepo.Add(company);

            company.CompanyName = "Tech Corporation";
            company.CompanySize = 150;
            var result = await _companyRepo.Update(company);

            Assert.IsNotNull(result);
            Assert.AreEqual("Tech Corporation", result.CompanyName);
            Assert.AreEqual(150, result.CompanySize);
        }
    }
}
