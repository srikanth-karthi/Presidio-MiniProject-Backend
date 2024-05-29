using Job_Portal_Application.Context;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Microsoft.EntityFrameworkCore;

using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class UserSkillsTest
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private UserSkillsService _userSkillsService;
        private Guid _testUserId;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
              .UseInMemoryDatabase("JobPortalTestDb")
              .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            IUserSkillsRepository userSkillsRepo = new UserSkillsRepository(_context);
            IRepository<Guid, Skill> skillRepository = new SkillRepository(_context);

            _userSkillsService = new UserSkillsService(userSkillsRepo, skillRepository);
            _testUserId = TestJobportalContext.UserId;
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddUserSkills_Success()
        {
            var request = new UserSkillsRequestDto
            {
                SkillIds = new List<Guid> { TestJobportalContext.SkillId3, TestJobportalContext.SkillId2 }
            };

            var response = await _userSkillsService.AddUserSkills(request, _testUserId);

            Assert.That(response, Is.Not.Null);
            Assert.That(response.Skills, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task AddUserSkills_InvalidSkill_ThrowsException()
        {
            var request = new UserSkillsRequestDto
            {
                SkillIds = new List<Guid> { Guid.NewGuid() }
            };
            var response = await _userSkillsService.AddUserSkills(request, _testUserId);
            Assert.That(response.InvalidSkills, Has.Count.EqualTo(1));

        }

        [Test]
        public async Task RemoveUserSkill_Success()
        {
            var request = new UserSkillsRequestDto
            {
                SkillIds = new List<Guid> { TestJobportalContext.SkillId1 }
            };

            var initialSkillsCount =( await _userSkillsService.GetAllUserSkills(_testUserId)).Count();

            await _userSkillsService.RemoveUserSkill(request, _testUserId);

            var finalSkillsCount = (await _userSkillsService.GetAllUserSkills(_testUserId))
                .Count();

            Assert.AreEqual(initialSkillsCount - 1, finalSkillsCount);
        }


        [Test]
        public async Task GetAllUserSkills_Success()
        {
            var skills = await _userSkillsService.GetAllUserSkills(_testUserId);

            Assert.NotNull(skills);
            Assert.IsNotEmpty(skills);
        }
        [Test]
        public async Task RemoveUserSkill_Fails_ThrowsException()
        {
            var request = new UserSkillsRequestDto
            {
                SkillIds = new List<Guid> { Guid.NewGuid() } 
            };


            var response=    await _userSkillsService.RemoveUserSkill(request, _testUserId);
 
            Assert.That(response.InvalidSkills, Has.Count.EqualTo(1));
        }
    }
}
