using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class AdminServiceTests
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private AdminService _adminService;
        private IRepository<Guid, Skill> _skillRepository;
        private IRepository<Guid, Title> _titleRepository;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            _skillRepository = new SkillRepository(_context);
            _titleRepository = new TitleRepository(_context);

           ISkillService skillservice = new SkillService(_skillRepository);
            ITitleService titleservice = new TitleService(_titleRepository);

            _adminService = new AdminService(skillservice, titleservice);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task CreateSkill_Success()
        {
            var skill = new Skill { SkillName = "Test Skill" };

            var result = await _adminService.CreateSkill(skill);

            Assert.NotNull(result);
            Assert.AreEqual("Test Skill", result.SkillName);
        }

        [Test]
        public void CreateSkill_SkillAlreadyExists_ThrowsException()
        {
            var skill = new Skill { SkillName = "Test Skill" };
            _context.Skills.Add(skill);
            _context.SaveChanges();

            var newSkill = new Skill { SkillName = "Test Skill" };

            Assert.ThrowsAsync<SkillAlreadyExisitException>(async () =>
            {
                await _adminService.CreateSkill(newSkill);
            });
        }

        [Test]
        public async Task DeleteSkill_Success()
        {
            var skill = new Skill { SkillName = "Test Skill" };
            _context.Skills.Add(skill);
            _context.SaveChanges();

            var result = await _adminService.DeleteSkill(skill.SkillId);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteSkill_SkillNotFound_ThrowsException()
        {
            Assert.ThrowsAsync<SkillNotFoundException>(async () =>
            {
                await _adminService.DeleteSkill(Guid.NewGuid());
            });
        }

        [Test]
        public async Task CreateTitle_Success()
        {
            var title = new Title { TitleName = "Test Title" };

            var result = await _adminService.CreateTitle(title);

            Assert.NotNull(result);
            Assert.AreEqual("Test Title", result.TitleName);
        }

        [Test]
        public void CreateTitle_TitleAlreadyExists_ThrowsException()
        {
            var title = new Title { TitleName = "Test Title" };
            _context.Titles.Add(title);
            _context.SaveChanges();

            var newTitle = new Title { TitleName = "Test Title" };

            Assert.ThrowsAsync<TitleAlreadyExisitException>(async () =>
            {
                await _adminService.CreateTitle(newTitle);
            });
        }

        [Test]
        public async Task DeleteTitle_Success()
        {
            var title = new Title { TitleName = "Test Title" };
            _context.Titles.Add(title);
            _context.SaveChanges();

            var result = await _adminService.DeleteTitle(title.TitleId);

            Assert.IsTrue(result);
        }

        [Test]
        public void DeleteTitle_TitleNotFound_ThrowsException()
        {
            Assert.ThrowsAsync<TitleNotFoundException>(async () =>
            {
                await _adminService.DeleteTitle(Guid.NewGuid());
            });
        }
    }
}
