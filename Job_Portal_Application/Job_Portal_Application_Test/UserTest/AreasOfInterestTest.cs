using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.AreasOfInterestDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class AreasOfInterestTest
    {
        private DbContextOptions _dbContextOptions;
        private JobportalContext _context;
        private AreasOfInterestService _areasOfInterestService;
        private Guid _testUserId;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            IAreasOfInterestRepository areasOfInterestRepo = new AreasOfInterestRepository(_context);

            TitleRepository titlerepository = new TitleRepository(_context);

            _testUserId = Guid.NewGuid();

            _areasOfInterestService = new AreasOfInterestService(titlerepository, areasOfInterestRepo);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddAreasOfInterest_Success()
        {
            var areasOfInterestDto = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId1,
                Lpa = 5
            };

            var result = await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto, _testUserId);

            Assert.NotNull(result);
            Assert.AreEqual(TestJobportalContext.TitleId1, result.TitleId);
        }

        [Test]
        public async Task UpdateAreasOfInterest_Success()
        {
            var areasOfInterestDto = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId1,
                Lpa = 5
            };

            var addedAreasOfInterest = await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto, _testUserId);

            var updateAreasOfInterestDto = new AreasOfInterestDto
            {
                AreasOfInterestId = addedAreasOfInterest.AreasOfInterestId,
                TitleId = TestJobportalContext.TitleId2,
                Lpa = 10
            };

            var updatedAreasOfInterest = await _areasOfInterestService.UpdateAreasOfInterest(updateAreasOfInterestDto, _testUserId);

            Assert.NotNull(updatedAreasOfInterest);
            Assert.AreEqual(TestJobportalContext.TitleId2, updatedAreasOfInterest.TitleId);
            Assert.AreEqual(10, updatedAreasOfInterest.Lpa);
        }

        [Test]
        public async Task DeleteAreasOfInterest_Success()
        {
            var areasOfInterestDto = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId1,
                Lpa = 5
            };

            var addedAreasOfInterest = await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto, _testUserId);

            var result = await _areasOfInterestService.DeleteAreasOfInterest(addedAreasOfInterest.AreasOfInterestId, _testUserId);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetAreasOfInterest_Success()
        {
            var areasOfInterestDto = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId1,
                Lpa = 5
            };

            var addedAreasOfInterest = await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto, _testUserId);

            var areasOfInterest = await _areasOfInterestService.GetAreasOfInterest(addedAreasOfInterest.AreasOfInterestId, _testUserId);

            Assert.NotNull(areasOfInterest);
            Assert.AreEqual(TestJobportalContext.TitleId1, areasOfInterest.TitleId);
        }

        [Test]
        public async Task GetAllAreasOfInterest_Success()
        {
            var areasOfInterestDto1 = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId1,
                Lpa = 5
            };

            var areasOfInterestDto2 = new AddAreasOfInterestDTO
            {
                TitleId = TestJobportalContext.TitleId2,
                Lpa = 7
            };

            await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto1, _testUserId);
            await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto2, _testUserId);

            var areasOfInterests = await _areasOfInterestService.GetAllAreasOfInterest(_testUserId);

            Assert.NotNull(areasOfInterests);
            Assert.AreEqual(2, areasOfInterests.Count());
        }

        [Test]
        public async Task AddAreasOfInterest_InvalidTitleId_ThrowsException()
        {
            var areasOfInterestDto = new AddAreasOfInterestDTO
            {
                TitleId = Guid.NewGuid(), 
                Lpa = 5
            };

            Assert.ThrowsAsync<TitleNotFoundException>(async () =>
            {
                await _areasOfInterestService.AddAreasOfInterest(areasOfInterestDto, _testUserId);
            });
        }

        [Test]
        public async Task UpdateAreasOfInterest_AreasOfInterestNotFound_ThrowsException()
        {
            var updateAreasOfInterestDto = new AreasOfInterestDto
            {
                AreasOfInterestId = Guid.NewGuid(),
                TitleId = TestJobportalContext.TitleId2,
                Lpa = 10
            };

            Assert.ThrowsAsync<AreasOfInterestNotFoundException>(async () =>
            {
                await _areasOfInterestService.UpdateAreasOfInterest(updateAreasOfInterestDto, _testUserId);
            });
        }

        [Test]
        public async Task DeleteAreasOfInterest_AreasOfInterestNotFound_ThrowsException()
        {
            var invalidAreasOfInterestId = Guid.NewGuid();

            Assert.ThrowsAsync<AreasOfInterestNotFoundException>(async () =>
            {
                await _areasOfInterestService.DeleteAreasOfInterest(invalidAreasOfInterestId, _testUserId);
            });
        }

        [Test]
        public async Task GetAreasOfInterest_AreasOfInterestNotFound_ThrowsException()
        {
            var invalidAreasOfInterestId = Guid.NewGuid(); 

            Assert.ThrowsAsync<AreasOfInterestNotFoundException>(async () =>
            {
                await _areasOfInterestService.GetAreasOfInterest(invalidAreasOfInterestId, _testUserId);
            });
        }

        [Test]
        public async Task GetAllAreasOfInterest_NoAreasOfInterestFound_ThrowsException()
        {
        
            Assert.ThrowsAsync<AreasOfInterestNotFoundException>(async () =>
            {
                await _areasOfInterestService.GetAllAreasOfInterest(_testUserId);
            });
        }

    }
}
