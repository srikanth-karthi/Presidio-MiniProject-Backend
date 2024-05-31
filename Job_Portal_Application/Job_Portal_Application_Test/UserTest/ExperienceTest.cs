using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.ExperienceDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Services.UsersServices;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Interfaces.IService;
using Microsoft.Extensions.Configuration;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class ExperienceTest
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private ExperienceService _experienceService;
        private Guid _testUserId;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            _testUserId =Guid.NewGuid();

            var testUser = new User
            {
                UserId = _testUserId,
                Name = "Test User",
                Email = "testuser@example.com",
                Dob = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                Address = "123 Test St",
                City = "Test City",
                PortfolioLink = "http://testuser.com",
                Phonenumber = "123-456-7890",
                ResumeUrl = "http://resume.testuser.com"
            };
            _context.Users.Add(testUser);



            _context.SaveChanges();

            IExperienceRepository experienceRepo = new ExperienceRepository(_context);
            TitleRepository titlerepository = new TitleRepository(_context);
            IUserRepository userRepo = new UserRepository(_context);

            _experienceService = new ExperienceService(titlerepository, experienceRepo, userRepo);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddExperience_Success()
        {
            var experienceDto = new AddExperienceDto
            {
                CompanyName = "Test Company",
                TitleId = TestJobportalContext.TitleId1,
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1)
            };

            var result = await _experienceService.AddExperience(experienceDto, _testUserId);

            Assert.NotNull(result);
            Assert.AreEqual("Test Company", result.CompanyName);
        }

        [Test]
        public async Task UpdateExperience_Success()
        {
            var experienceDto = new AddExperienceDto
            {
                CompanyName = "Test Company",
                TitleId = TestJobportalContext.TitleId1,
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1)
            };

            var addedExperience = await _experienceService.AddExperience(experienceDto, _testUserId);

            var updateExperienceDto = new GetExperienceDto
            {
                ExperienceId = addedExperience.ExperienceId,
                CompanyName = "Updated Company",
                TitleId = TestJobportalContext.TitleId2,
                StartYear = new DateTime(2020, 1, 1),
                EndYear = new DateTime(2022, 1, 1)
            };

            var updatedExperience = await _experienceService.UpdateExperience(updateExperienceDto, _testUserId);

            Assert.NotNull(updatedExperience);
            Assert.AreEqual("Updated Company", updatedExperience.CompanyName);
        }

        [Test]
        public async Task DeleteExperience_Success()
        {
            var experienceDto = new AddExperienceDto
            {
                CompanyName = "Test Company",
                TitleId = TestJobportalContext.TitleId1,
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1)
            };

            var addedExperience = await _experienceService.AddExperience(experienceDto, _testUserId);

            var result = await _experienceService.DeleteExperience(addedExperience.ExperienceId, _testUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetExperience_Success()
        {
            var experienceDto = new AddExperienceDto
            {
                CompanyName = "Test Company",
                TitleId = TestJobportalContext.TitleId1,
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1)
            };

            var addedExperience = await _experienceService.AddExperience(experienceDto, _testUserId);

            var experience = await _experienceService.GetExperience(addedExperience.ExperienceId, _testUserId);

            Assert.NotNull(experience);
            Assert.AreEqual("Test Company", experience.CompanyName);
        }

        [Test]
        public async Task GetAllExperiences_Success()
        {
            var experienceDto1 = new AddExperienceDto
            {
                CompanyName = "Test Company 1",
                TitleId = TestJobportalContext.TitleId1,
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1)
            };

            var experienceDto2 = new AddExperienceDto
            {
                CompanyName = "Test Company 2",
                TitleId = TestJobportalContext.TitleId2,
                StartYear = new DateTime(2020, 1, 1),
                EndYear = new DateTime(2022, 1, 1)
            };

            await _experienceService.AddExperience(experienceDto1, _testUserId);
            await _experienceService.AddExperience(experienceDto2, _testUserId);

            var experiences = await _experienceService.GetAllExperiences(_testUserId);

            Assert.NotNull(experiences);
            Assert.AreEqual(2, experiences.Count());
        }

        [Test]
        public void GetAllExperiences_Failed()
        {
            Assert.ThrowsAsync<ExperienceNotFoundException>(() => _experienceService.GetAllExperiences(Guid.NewGuid()));
        }
    }
}
