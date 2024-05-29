using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.EducationDtos;
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
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Interfaces.IService;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class EducationTest
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private EducationService _educationService;
        private Guid _testUserId;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();


            _context.Database.EnsureCreated();

            IEducationRepository educationRepo = new EducationRepository(_context);

            _testUserId = Guid.NewGuid();

            _educationService = new EducationService(educationRepo);

        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddEducation_Success()
        {
            var educationDto = new AddEducationDto
            {
                Level = "Bachelors",
                InstitutionName = "Test University",
                StartYear = new DateTime(2015, 1, 1),
                EndYear = new DateTime(2019, 1, 1),
                Percentage = 85.5f,
                IsCurrentlyStudying = false
            };

            var result = await _educationService.AddEducation(educationDto, _testUserId);

            Assert.NotNull(result);
            Assert.AreEqual("Test University", result.InstitutionName);
        }

        [Test]
        public async Task UpdateEducation_Success()
        {
            var educationDto = new AddEducationDto
            {
                Level = "Bachelors",
                InstitutionName = "Test University",
                StartYear = new DateTime(2015, 1, 1),
                EndYear = new DateTime(2019, 1, 1),
                Percentage = 85.5f,
                IsCurrentlyStudying = false
            };

            var addedEducation = await _educationService.AddEducation(educationDto, _testUserId);

            var updateEducationDto = new EducationDto
            {
                EducationId = addedEducation.EducationId,
                Level = "Masters",
                InstitutionName = "Updated University",
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1),
                Percentage = 90.0f,
                IsCurrentlyStudying = false
            };

            var updatedEducation = await _educationService.UpdateEducation(updateEducationDto, _testUserId);

            Assert.NotNull(updatedEducation);
            Assert.AreEqual("Updated University", updatedEducation.InstitutionName);
        }

        [Test]
        public async Task DeleteEducation_Success()
        {
            var educationDto = new AddEducationDto
            {
                Level = "Bachelors",
                InstitutionName = "Test University",
                StartYear = new DateTime(2015, 1, 1),
                EndYear = new DateTime(2019, 1, 1),
                Percentage = 85.5f,
                IsCurrentlyStudying = false
            };

            var addedEducation = await _educationService.AddEducation(educationDto, _testUserId);

            var result = await _educationService.DeleteEducation(addedEducation.EducationId, _testUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetEducation_Success()
        {
            var educationDto = new AddEducationDto
            {
                Level = "Bachelors",
                InstitutionName = "Test University",
                StartYear = new DateTime(2015, 1, 1),
                EndYear = new DateTime(2019, 1, 1),
                Percentage = 85.5f,
                IsCurrentlyStudying = false
            };

            var addedEducation = await _educationService.AddEducation(educationDto, _testUserId);

            var education = await _educationService.GetEducation(addedEducation.EducationId, _testUserId);

            Assert.NotNull(education);
            Assert.AreEqual("Test University", education.InstitutionName);
        }

        [Test]
        public async Task GetAllEducations_Success()
        {
            var educationDto1 = new AddEducationDto
            {
                Level = "Bachelors",
                InstitutionName = "Test University 1",
                StartYear = new DateTime(2015, 1, 1),
                EndYear = new DateTime(2019, 1, 1),
                Percentage = 85.5f,
                IsCurrentlyStudying = false
            };

            var educationDto2 = new AddEducationDto
            {
                Level = "Masters",
                InstitutionName = "Test University 2",
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1),
                Percentage = 90.0f,
                IsCurrentlyStudying = false
            };

            await _educationService.AddEducation(educationDto1, _testUserId);
            await _educationService.AddEducation(educationDto2, _testUserId);

            var educations = await _educationService.GetAllEducations(_testUserId);

            Assert.NotNull(educations);
            Assert.AreEqual(2, educations.Count());
        }




        [Test]
        public async Task UpdateEducation_InvalidUserId_ThrowsException()
        {
            var updateEducationDto = new EducationDto
            {
                EducationId = Guid.NewGuid(),
                Level = "Masters",
                InstitutionName = "Updated University",
                StartYear = new DateTime(2019, 1, 1),
                EndYear = new DateTime(2021, 1, 1),
                Percentage = 90.0f,
                IsCurrentlyStudying = false
            };

            var invalidUserId = Guid.NewGuid();

            Assert.ThrowsAsync<EducationNotFoundException>(async () =>
            {
                await _educationService.UpdateEducation(updateEducationDto, invalidUserId);
            });
        }

        [Test]
        public async Task DeleteEducation_InvalidEducationId_ThrowsException()
        {
            var invalidEducationId = Guid.NewGuid(); 

            Assert.ThrowsAsync<EducationNotFoundException>(async () =>
            {
                await _educationService.DeleteEducation(invalidEducationId, _testUserId);
            });
        }

        [Test]
        public async Task GetEducation_InvalidEducationId_ThrowsException()
        {
            var invalidEducationId = Guid.NewGuid(); 

            Assert.ThrowsAsync<EducationNotFoundException>(async () =>
            {
                await _educationService.GetEducation(invalidEducationId, _testUserId);
            });
        }

        [Test]
        public async Task GetAllEducations_NoEducationsFound_ThrowsException()
        {
            

            Assert.ThrowsAsync<EducationNotFoundException>(async () =>
            {
                await _educationService.GetAllEducations(_testUserId);
            });
        }

    }
}
