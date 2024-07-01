using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class UserServiceTests
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private IUserService _userService;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
              .UseInMemoryDatabase("JobPortalTestDb")
              .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            IConfiguration configuration = new ConfigurationBuilder()
              .AddInMemoryCollection(new Dictionary<string, string>
                {
            { "TokenKey:JWT", "y_J82VYvg5Jh8-DT89E1kz_FzHNN3tB_Sy4b_yLhoZ8Y6q-jVOWU3-xPFlPS6cYYicWlb0XhREXAf3OWTbnN3w==" }
                })
              .Build();
            MinIOService _minioService = new MinIOService(configuration);
            IUserRepository userRepo = new UserRepository(_context);
            ITokenService tokenService = new TokenServices(configuration);
            ICompanyRepository companyRepo = new CompanyRepository(_context);
            IJobRepository jobRepository = new JobRepository(_context);
            IRepository<Guid, Skill> _skillRepository = new SkillRepository(_context);
            IRepository<Guid, Credential> _credentialRepository = new CredentialRepository(_context);
            IUserSkillsRepository _userSkillsRepository = new UserSkillsRepository(_context);

            _userService = new UserService(
                      _skillRepository,
                       companyRepo,
                          _userSkillsRepository,
                            _credentialRepository,
                            jobRepository,
                            userRepo,
                            tokenService,
                            _minioService
            );
        }


        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public void SeedData_ShouldBeLoaded()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == "test.user@example.com");
            Assert.IsNotNull(user);
            Assert.AreEqual("Test", user.Name);
        }

        [Test]
        public async Task Register_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),
           
                Password = "password"
            };

            var result = await _userService.Register(userDto);

            Assert.NotNull(result);

        }

        [Test]
        public async Task Register_ShouldThrowUserAlreadyExistsException_WhenUserExists()
        {
            var userDto = new UserRegisterDto
            {
                Name = "Jane Doe",
                Email = "jane.doe@example.com",
                Dob = new DateTime(1995, 1, 1),
      
                Password = "password"
            };


            await _userService.Register(userDto);

            Assert.ThrowsAsync<UserAlreadyExistsException>(() => _userService.Register(userDto));
        }

        [Test]
        public async Task Register_ShouldThrowUserAlreadyExistsException_WhenCompanyUserExists()
        {
            var userDto = new UserRegisterDto
            {
                Name = "Jane Doe",
                Email = "contact@techcorp.com",
                Dob = new DateTime(1995, 1, 1),
            
                Password = "password"
            };

        
      
            Assert.ThrowsAsync<UserAlreadyExistsException>(() => _userService.Register(userDto));
        }


        [Test]
        public async Task Login_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),
  
                Password = "password"
            };

            await _userService.Register(userDto);

            var loginDto = new LoginDto
            {
                Email = "john.doe@example.com",
                Password = "password"
            };

            var result = await _userService.Login(loginDto);

            Assert.NotNull(result);
        }
        [Test]
        public async Task GetUserProfile_ReturnsCorrectUserProfileDto()
        {

            var userProfileDto = await _userService.GetUserProfile(TestJobportalContext.UserId);

            Assert.IsNotNull(userProfileDto);
            Assert.AreEqual(TestJobportalContext.UserId, userProfileDto.UserId);
            Assert.AreEqual("Test", userProfileDto.Name);
            Assert.AreEqual("test.user@example.com", userProfileDto.Email);

    

        }
        [Test]
        public async Task Login_ShouldThrowInvalidCredentialsException_WhenEmailNotFound()
        {
            var loginDto = new LoginDto
            {
                Email = "nonexistent@example.com",
                Password = "password"
            };

            Assert.ThrowsAsync<InvalidCredentialsException>(() => _userService.Login(loginDto));
        }

        [Test]
        public async Task Login_ShouldThrowInvalidCredentialsException_WhenInvalidPassword()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),
  
                Password = "password"
            };

            await _userService.Register(userDto);

            var loginDto = new LoginDto
            {
                Email = "john.doe@example.com",
                Password = "invalidpassword"
            };

            Assert.ThrowsAsync<InvalidCredentialsException>(() => _userService.Login(loginDto));
        }

        [Test]
        public async Task GetRecommendedJobs_Success()
        {

            var jobs = await _userService.GetRecommendedJobs(1, 10, Guid.Parse("c20b7faa-890d-4908-a57a-a9ea75b82b5f"));
            Assert.NotNull(jobs);
            Assert.IsNotEmpty(jobs);
        }

        [Test]
        public async Task UpdateUser_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),
 
                Password = "password"
            };

            var registeredUser = await _userService.Register(userDto);

            var updateUserDto = new UpdateUserDto
            {
                Name = "John Updated",
                Address = "Updated Address",
                City = "Updated City",
                PortfolioLink = "https://updatedportfolio.com",
                PhoneNumber = "0987654321",
                ResumeUrl = "https://updatedresume.com"
            };

            var updatedUser = await _userService.UpdateUser(updateUserDto, registeredUser.UserId);

            Assert.NotNull(updatedUser);
            Assert.AreEqual("John Updated", updatedUser.Name);
        }

        [Test]
        public async Task DeleteUser_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),

                Password = "password"
            };

            var registeredUser = await _userService.Register(userDto);

            var result = await _userService.DeleteUser(registeredUser.UserId);

            Assert.IsTrue(result);
        }
        [Test]
        public async Task CalculateJobMatchPercentage_ShouldReturnCorrectPercentage()
        {
 
            var result = await _userService.CalculateJobMatchPercentage(TestJobportalContext.JobId1, TestJobportalContext.UserId);

            Assert.GreaterOrEqual(result, 0);
            Assert.LessOrEqual(result, 100);
        }


        [Test]
        public async Task GetUserProfile_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),
    
                Password = "password"
            };

            var registeredUser = await _userService.Register(userDto);

            var userProfile = await _userService.GetUserProfile(registeredUser.UserId);

            Assert.NotNull(userProfile);
            Assert.AreEqual("John Doe", userProfile.Name);
        }

        [Test]
        public async Task UpdateResumeUrl_Success()
        {
            var userDto = new UserRegisterDto
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Dob = new DateTime(1990, 1, 1),

                Password = "password"
            };

            var registeredUser = await _userService.Register(userDto);

            var updatedUser = await _userService.UpdateResumeUrl(registeredUser.UserId, "https://newresume.com");

            Assert.NotNull(updatedUser);
            Assert.AreEqual("https://newresume.com", updatedUser.ResumeUrl);
        }

        public async Task GetAllUserSkills_Success()
        {
            var skills = await _userService.GetSkills(TestJobportalContext.UserId);

            Assert.NotNull(skills);
            Assert.IsNotEmpty(skills);
        }
      
    }
}
