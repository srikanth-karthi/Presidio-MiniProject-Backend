using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Job_Portal_Application_Test.ExperienceTest
{
    [TestFixture]
    public class ExperienceRepoTest
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private ExperienceRepository _experienceRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _experienceRepo = new ExperienceRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddExperience()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var experience = new Experience
            {
                UserId = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                TitleId = title.TitleId,
                StartYear = new DateOnly(2019, 1, 1),
                EndYear = new DateOnly(2021, 1, 1),
                Title = title
            };

            var result = await _experienceRepo.Add(experience);

            Assert.IsNotNull(result);
            Assert.AreEqual(experience.ExperienceId, result.ExperienceId);
            Assert.AreEqual(1, await _context.Experiences.CountAsync());
        }

        [Test]
        public async Task DeleteExperience()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var experience = new Experience
            {
                UserId = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                TitleId = title.TitleId,
                StartYear = new DateOnly(2019, 1, 1),
                EndYear = new DateOnly(2021, 1, 1),
                Title = title
            };

            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();

            var result = await _experienceRepo.Delete(experience);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.Experiences.CountAsync());
        }

        [Test]
        public async Task Getexperience()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var experience = new Experience
            {
                UserId = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                TitleId = title.TitleId,
                StartYear = new DateOnly(2019, 1, 1),
                EndYear = new DateOnly(2021, 1, 1),
                Title = title
            };

            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();

            var result = await _experienceRepo.Get(experience.ExperienceId);

            Assert.IsNotNull(result);
            Assert.AreEqual(experience.ExperienceId, result.ExperienceId);
        }

        [Test]
        public async Task Failed_GetExperience()
        {
            var result = await _experienceRepo.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllExperiences()
        {
            var title1 = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var title2 = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName= "Data Analyst"
            };
            await _context.Experiences.AddRangeAsync(
                new Experience
                {
                    UserId = Guid.NewGuid(),
                    CompanyName = "Tech Corp",
                    TitleId = title1.TitleId,
                    StartYear = new DateOnly(2019, 1, 1),
                    EndYear = new DateOnly(2021, 1, 1),
                    Title = title1
                },
                new Experience
                {
                    UserId = Guid.NewGuid(),
                    CompanyName = "Data Corp",
                    TitleId = title2.TitleId,
                    StartYear = new DateOnly(2020, 1, 1),
                    EndYear = new DateOnly(2022, 1, 1),
                    Title = title2
                }
            );
            await _context.SaveChangesAsync();

            var result = await _experienceRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllExperiencesForUser()
        {
            var userId = Guid.NewGuid();
            var title1 = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var title2 = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Data Analyst"
            };
            await _context.Experiences.AddRangeAsync(
                new Experience
                {
                    UserId = userId,
                    CompanyName = "Tech Corp",
                    TitleId = title1.TitleId,
                    StartYear = new DateOnly(2019, 1, 1),
                    EndYear = new DateOnly(2021, 1, 1),
                    Title = title1
                },
                new Experience
                {
                    UserId = userId,
                    CompanyName = "Data Corp",
                    TitleId = title2.TitleId,
                    StartYear = new DateOnly(2020, 1, 1),
                    EndYear = new DateOnly(2022, 1, 1),
                    Title = title2
                }
            );
            await _context.SaveChangesAsync();

            var result = await _experienceRepo.GetAll(userId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateExperience()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var experience = new Experience
            {
                UserId = Guid.NewGuid(),
                CompanyName = "Tech Corp",
                TitleId = title.TitleId,
                StartYear = new DateOnly(2019, 1, 1),
                EndYear = new DateOnly(2021, 1, 1),
                Title = title
            };

            await _context.Experiences.AddAsync(experience);
            await _context.SaveChangesAsync();

            experience.CompanyName = "Tech Solutions";
            var result = await _experienceRepo.Update(experience);

            Assert.IsNotNull(result);
            Assert.AreEqual("Tech Solutions", result.CompanyName);
        }
    }
}
