using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Job_Portal_Application_Test.EducationTest
{
    [TestFixture]
    public class EducationRepoTest
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private EducationRepository _educationRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _educationRepo = new EducationRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddEducation()
        {
            var education = new Education
            {
                UserId = Guid.NewGuid(),
                Level = "Bachelor",
                StartYear = new DateOnly(2018, 9, 1),
                EndYear = new DateOnly(2022, 6, 1),
                Percentage = 85,
                InstitutionName = "University",
                IsCurrentlyStudying = false
            };

            var result = await _educationRepo.Add(education);

            Assert.IsNotNull(result);
            Assert.AreEqual(education.EducationId, result.EducationId);
            Assert.AreEqual(1, await _context.Educations.CountAsync());
        }

        [Test]
        public async Task DeleteEducation()
        {
            var education = new Education
            {
                UserId = Guid.NewGuid(),
                Level = "Bachelor",
                StartYear = new DateOnly(2018, 9, 1),
                EndYear = new DateOnly(2022, 6, 1),
                Percentage = 85,
                InstitutionName = "University",
                IsCurrentlyStudying = false
            };

            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();

            var result = await _educationRepo.Delete(education);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.Educations.CountAsync());
        }

        [Test]
        public async Task GetEducation()
        {
            var education = new Education
            {
                UserId = Guid.NewGuid(),
                Level = "Bachelor",
                StartYear = new DateOnly(2018, 9, 1),
                EndYear = new DateOnly(2022, 6, 1),
                Percentage = 85,
                InstitutionName = "University",
                IsCurrentlyStudying = false
            };

            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();

            var result = await _educationRepo.Get(education.EducationId);

            Assert.IsNotNull(result);
            Assert.AreEqual(education.EducationId, result.EducationId);
        }

        [Test]
        public async Task Get_ShouldReturnNull_WhenEducationIdDoesNotExist()
        {
            var result = await _educationRepo.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllEducations()
        {
            await _context.Educations.AddRangeAsync(
                new Education
                {
                    UserId = Guid.NewGuid(),
                    Level = "Bachelor",
                    StartYear = new DateOnly(2018, 9, 1),
                    EndYear = new DateOnly(2022, 6, 1),
                    Percentage = 85,
                    InstitutionName = "University",
                    IsCurrentlyStudying = false
                },
                new Education
                {
                    UserId = Guid.NewGuid(),
                    Level = "Master",
                    StartYear = new DateOnly(2023, 9, 1),
                    Percentage = 90,
                    InstitutionName = "Graduate School",
                    IsCurrentlyStudying = true
                }
            );
            await _context.SaveChangesAsync();

            var result = await _educationRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAll()
        {
            var userId = Guid.NewGuid();
            await _context.Educations.AddRangeAsync(
                new Education
                {
                    UserId = userId,
                    Level = "Bachelor",
                    StartYear = new DateOnly(2018, 9, 1),
                    EndYear = new DateOnly(2022, 6, 1),
                    Percentage = 85,
                    InstitutionName = "University",
                    IsCurrentlyStudying = false
                },
                new Education
                {
                    UserId = userId,
                    Level = "Master",
                    StartYear = new DateOnly(2023, 9, 1),
                    Percentage = 90,
                    InstitutionName = "Graduate School",
                    IsCurrentlyStudying = true
                }
            );
            await _context.SaveChangesAsync();

            var result = await _educationRepo.GetAll(userId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateEducation()
        {
            var education = new Education
            {
                UserId = Guid.NewGuid(),
                Level = "Bachelor",
                StartYear = new DateOnly(2018, 9, 1),
                EndYear = new DateOnly(2022, 6, 1),
                Percentage = 85,
                InstitutionName = "University",
                IsCurrentlyStudying = false
            };

            await _context.Educations.AddAsync(education);
            await _context.SaveChangesAsync();

            education.Level = "Master";
            var result = await _educationRepo.Update(education);

            Assert.IsNotNull(result);
            Assert.AreEqual("Master", result.Level);
        }
    }
}
