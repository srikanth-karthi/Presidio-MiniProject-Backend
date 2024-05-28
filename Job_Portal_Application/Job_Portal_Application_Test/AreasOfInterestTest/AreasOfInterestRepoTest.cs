using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Job_Portal_Application_Test.AreasOfInterestTest
{
    [TestFixture]
    public class AreasOfInterestRepoTest
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private AreasOfInterestRepository _areasOfInterestRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _areasOfInterestRepo = new AreasOfInterestRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAreasOfInterest()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var areasOfInterest = new AreasOfInterest
            {
                UserId = Guid.NewGuid(),
                TitleId = title.TitleId,
                Lpa = 15.5f,
                Title = title
            };

            var result = await _areasOfInterestRepo.Add(areasOfInterest);

            Assert.IsNotNull(result);
            Assert.AreEqual(areasOfInterest.AreasOfInterestId, result.AreasOfInterestId);
            Assert.AreEqual(1, await _context.AreasOfInterests.CountAsync());
        }

        [Test]
        public async Task DeleteAreasOfInterest()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var areasOfInterest = new AreasOfInterest
            {
                UserId = Guid.NewGuid(),
                TitleId = title.TitleId,
                Lpa = 15.5f,
                Title = title
            };

            await _context.AreasOfInterests.AddAsync(areasOfInterest);
            await _context.SaveChangesAsync();

            var result = await _areasOfInterestRepo.Delete(areasOfInterest);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.AreasOfInterests.CountAsync());
        }

        [Test]
        public async Task GetAreasOfInterest()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var areasOfInterest = new AreasOfInterest
            {
                UserId = Guid.NewGuid(),
                TitleId = title.TitleId,
                Lpa = 15.5f,
                Title = title
            };

            await _context.AreasOfInterests.AddAsync(areasOfInterest);
            await _context.SaveChangesAsync();

            var result = await _areasOfInterestRepo.Get(areasOfInterest.AreasOfInterestId);

            Assert.IsNotNull(result);
            Assert.AreEqual(areasOfInterest.AreasOfInterestId, result.AreasOfInterestId);
        }

        [Test]
        public async Task Failed_GetAreasOfInterest()
        {
            var result = await _areasOfInterestRepo.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAreasOfInterest()
        {
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
            await _context.AreasOfInterests.AddRangeAsync(
                new AreasOfInterest
                {
                    UserId = Guid.NewGuid(),
                    TitleId = title1.TitleId,
                    Lpa = 15.5f,
                    Title = title1
                },
                new AreasOfInterest
                {
                    UserId = Guid.NewGuid(),
                    TitleId = title2.TitleId,
                    Lpa = 12.0f,
                    Title = title2
                }
            );
            await _context.SaveChangesAsync();

            var result = await _areasOfInterestRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetAllAreasOfInterestForUser()
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
            await _context.AreasOfInterests.AddRangeAsync(
                new AreasOfInterest
                {
                    UserId = userId,
                    TitleId = title1.TitleId,
                    Lpa = 15.5f,
                    Title = title1
                },
                new AreasOfInterest
                {
                    UserId = userId,
                    TitleId = title2.TitleId,
                    Lpa = 12.0f,
                    Title = title2
                }
            );
            await _context.SaveChangesAsync();

            var result = await _areasOfInterestRepo.GetAll(userId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task UpdateAreasOfInterest()
        {
            var title = new Title
            {
                TitleId = Guid.NewGuid(),
                TitleName = "Software Engineer"
            };
            var areasOfInterest = new AreasOfInterest
            {
                UserId = Guid.NewGuid(),
                TitleId = title.TitleId,
                Lpa = 15.5f,
                Title = title
            };

            await _context.AreasOfInterests.AddAsync(areasOfInterest);
            await _context.SaveChangesAsync();

            areasOfInterest.Lpa = 20.0f;
            var result = await _areasOfInterestRepo.Update(areasOfInterest);

            Assert.IsNotNull(result);
            Assert.AreEqual(20.0f, result.Lpa);
        }
    }
}
