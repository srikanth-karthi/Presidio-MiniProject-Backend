using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.CompanyDto;
using Job_Portal_Application.Dto.CompanyDtos;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Job_Portal_Application.Services.CompanyService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.CompanyTest
{
    [TestFixture]
    public class CompanyServiceTests
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private CompanyService _companyService;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
              .UseInMemoryDatabase("JobPortalTestDb")
              .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            ICompanyRepository companyRepo = new CompanyRepository(_context);
            IUserRepository userRepo = new UserRepository(_context);

            IConfiguration configuration = new ConfigurationBuilder()
               .AddInMemoryCollection(new Dictionary<string, string>
               {
                   { "TokenKey:JWT", "y_J82VYvg5Jh8-DT89E1kz_FzHNN3tB_Sy4b_yLhoZ8Y6q-jVOWU3-xPFlPS6cYYicWlb0XhREXAf3OWTbnN3w==" }
               })
                .Build();

            ITokenService tokenService = new TokenServices(configuration);

            IRepository<Guid, Credential> _credentialRepository = new CredentialRepository(_context);
            _companyService = new CompanyService(_credentialRepository,companyRepo, userRepo, tokenService);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task Register_Success()
        {
            var companyDto = new CompanyRegisterDto
            {
                CompanyName = "Tech Corp",
                Email = "contacts@techcorp.com",
                CompanyAddress = "123 Tech Street",
                City = "Tech City",
                CompanySize = 1000,
                CompanyWebsite = "https://www.techcorp.com",
                Password = "password",
                        CompanyDescription = ""
            };

            var result = await _companyService.Register(companyDto);

            Assert.NotNull(result);
            Assert.AreEqual("contacts@techcorp.com", result.Email);
        }
        [Test]
        public async Task Register_failed()
        {
            var companyDto = new CompanyRegisterDto
            {
                CompanyName = "Tech Corp",
                Email = "contact@techcorp.com",
                CompanyAddress = "123 Tech Street",
                City = "Tech City",
                CompanySize = 1000,
                CompanyWebsite = "https://www.techcorp.com",
                Password = "password"
            };

            Assert.ThrowsAsync<UserAlreadyExistsException>(() => _companyService.Register(companyDto));

        }

        [Test]
        public async Task Login_Failed()
        {
 
            var loginDto = new LoginDto
            {
                Email = "admin@test.com",
                Password = "AdminPassword"
            };

            Assert.ThrowsAsync<InvalidCredentialsException>(() =>  _companyService.Login(loginDto));
        }
        [Test]
        public async Task Login_Success()
        {
            var loginDto = new LoginDto
            {
                Email = "contact@innovate.com",
                Password = "123"
            };

            var result = await _companyService.Login(loginDto);

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }
        [Test]
        public async Task UpdateCompany_Success()
        {
            var updateDto = new CompanyUpdateDto
            { 
              
                CompanyName = "Updated Tech Corp",
                CompanyAddress = "456 Updated Tech Street",
                City = "Updated Tech City",
                CompanySize = 1200,
                CompanyWebsite = "https://www.updated.techcorp.com"
            };

            var result = await _companyService.UpdateCompany(updateDto, TestJobportalContext.CompanyId1);

    
            Assert.That(result.CompanyName, Is.EqualTo("Updated Tech Corp"));
        }
        [Test]
        public async Task DeleteCompany_Success()
        {
  
            var result = await _companyService.DeleteCompany( TestJobportalContext.CompanyId1);

            Assert.That(result, Is.True);
     
        }

        [Test]
        public async Task GetCompany_Success()
        {

            var result = await _companyService.GetCompany(TestJobportalContext.CompanyId1);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.CompanyId, TestJobportalContext.CompanyId1);

        }

        [Test]
        public async Task GetAllCompany_Success()
        {
            var result = await _companyService.GetAllCompanies();
            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result);
        }




    }
}
