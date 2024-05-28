using System.Text;
using Job_Portal_Application.Context;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.UserRepos;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Job_Portal_Application_Test.UserTest
{
    [TestFixture]
    public class UserRepoTests
    {
        private DbContextOptions<JobportalContext> _dbContextOptions;
        private JobportalContext _context;
        private UserRepository _userRepo;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder<JobportalContext>()
                .UseInMemoryDatabase("JobPortalTestDb")
                .Options;

            _context = new JobportalContext(_dbContextOptions);
            _userRepo = new UserRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task GetById_ShouldReturnEntity_WhenEntityExists()
        {
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Phonenumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HasCode = Encoding.UTF8.GetBytes("key")
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.Get(user.UserId);

            Assert.NotNull(result);
            Assert.AreEqual("user@example.com", result.Email);
            Assert.AreEqual("John Doe", result.Name);
        }

        [Test]
        public async Task Failed_GetById()
        {
            var result = await _userRepo.Get(Guid.NewGuid());
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAll()
        {
            await _context.Users.AddRangeAsync(
                new User
                {
                    Email = "user1@example.com",
                    Name = "John Doe",
                    Phonenumber = "1234567890",
                    Password = Encoding.UTF8.GetBytes("password"),
                    HasCode = Encoding.UTF8.GetBytes("key")
                },
                new User
                {
                    Email = "user2@example.com",
                    Name = "Jane Doe",
                    Phonenumber = "9876543210",
                    Password = Encoding.UTF8.GetBytes("password"),
                    HasCode = Encoding.UTF8.GetBytes("key")
                }
            );
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetAll();

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task Add()
        {
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Phonenumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HasCode = Encoding.UTF8.GetBytes("key")
            };

            var result = await _userRepo.Add(user);

            Assert.IsNotNull(result);
            Assert.AreEqual("user@example.com", result.Email);
            Assert.AreEqual(1, await _context.Users.CountAsync());
        }

        [Test]
        public async Task Update()
        {
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Phonenumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HasCode = Encoding.UTF8.GetBytes("key")
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            user.Name = "Updated John Doe";
            var result = await _userRepo.Update(user);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated John Doe", result.Name);
        }

        [Test]
        public async Task Delete()
        {
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Phonenumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HasCode = Encoding.UTF8.GetBytes("key")
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.Delete(user);

            Assert.IsTrue(result);
            Assert.AreEqual(0, await _context.Users.CountAsync());
        }

        [Test]
        public async Task GetByEmail_ShouldReturnEntity_WhenEmailExists()
        {
            var user = new User
            {
                Email = "user@example.com",
                Name = "John Doe",
                Phonenumber = "1234567890",
                Password = Encoding.UTF8.GetBytes("password"),
                HasCode = Encoding.UTF8.GetBytes("key")
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var result = await _userRepo.GetByEmail("user@example.com");

            Assert.NotNull(result);
            Assert.AreEqual("user@example.com", result.Email);
        }




    }
}
