using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Job_Portal_Application_Test.UserSkillsTest
{
    [TestFixture]
    public class UserSkillsRepoTest
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private UserSkillsRepository _userSkillsRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _userSkillsRepo = new UserSkillsRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddUserSkills()
        {
            var skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            var userSkills = new UserSkills
            {
                UserId = user.UserId,
                SkillId = skill.SkillId,
                Skill = skill,
 
            };

            var result = await _userSkillsRepo.Add(userSkills);

            Assert.IsNotNull(result);
            Assert.AreEqual(userSkills.UserSkillsId, result.UserSkillsId);
            Assert.AreEqual(1, await _context.UserSkills.CountAsync());
        }

        [Test]
        public async Task DeleteUserSkills()
        {
            var skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            var userSkills = new UserSkills
            {
                UserId = user.UserId,
                SkillId = skill.SkillId,
                Skill = skill,

            };

            await _context.UserSkills.AddAsync(userSkills);
            await _context.SaveChangesAsync();

            var result = await _userSkillsRepo.Delete(userSkills);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.UserSkills.CountAsync());
        }

        [Test]
        public async Task GetUserSkills()
        {
            var skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            var userSkills = new UserSkills
            {
                UserId = user.UserId,
                SkillId = skill.SkillId,
                Skill = skill,
      
            };


            var addedUserskill= await _userSkillsRepo.Add(userSkills);

            var result = await _userSkillsRepo.Get(addedUserskill.UserSkillsId);

            Assert.IsNotNull(result);
            Assert.AreEqual(userSkills.UserSkillsId, result.UserSkillsId);
        }

        [Test]
        public async Task Failed_GetUserSkills()
        {
            var result = await _userSkillsRepo.Get(Guid.NewGuid());

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllUserSkills()
        {
            var skill1 = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var skill2 = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "Python"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            await _context.UserSkills.AddRangeAsync(
                new UserSkills
                {
                    UserId = user.UserId,
                    SkillId = skill1.SkillId,
                    Skill = skill1,
          
                },
                new UserSkills
                {
                    UserId = user.UserId,
                    SkillId = skill2.SkillId,
                    Skill = skill2,
                
                }
            );
            await _context.SaveChangesAsync();

            var result = await _userSkillsRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetUserSkillsByUserId()
        {
            var skill1 = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var skill2 = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "Python"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            await _context.UserSkills.AddRangeAsync(
                new UserSkills
                {
                    UserId = user.UserId,
                    SkillId = skill1.SkillId,
                    Skill = skill1,
                   
                },
                new UserSkills
                {
                    UserId = user.UserId,
                    SkillId = skill2.SkillId,
                    Skill = skill2,
              
                }
            );
            await _context.SaveChangesAsync();

            var result = await _userSkillsRepo.GetByUserId(user.UserId);

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetUserSkillsByUserIdAndSkillId()
        {
            var skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            var userSkills = new UserSkills
            {
                UserId = user.UserId,
                SkillId = skill.SkillId,
                Skill = skill,
         
            };

            await _context.UserSkills.AddAsync(userSkills);
            await _context.SaveChangesAsync();

            var result = await _userSkillsRepo.GetByUserIdAndSkillId(user.UserId, skill.SkillId);

            Assert.IsNotNull(result);
            Assert.AreEqual(userSkills.UserSkillsId, result.UserSkillsId);
        }

        [Test]
        public async Task UpdateUserSkills()
        {
            var skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "C#"
            };
            var user = new User
            {
                UserId = Guid.NewGuid(),
                Name = "John Doe"
            };
            var userSkills = new UserSkills
            {
                UserId = user.UserId,
                SkillId = skill.SkillId,
                Skill = skill,
               
            };

            await _context.UserSkills.AddAsync(userSkills);
            await _context.SaveChangesAsync();

            userSkills.Skill = new Skill
            {
                SkillId = Guid.NewGuid(),
                Skill_Name = "JavaScript"
            };
            var result = await _userSkillsRepo.Update(userSkills);

            Assert.IsNotNull(result);
            Assert.AreEqual("JavaScript", result.Skill.Skill_Name);
        }
    }
}
