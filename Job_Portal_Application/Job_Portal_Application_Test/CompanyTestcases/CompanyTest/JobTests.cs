﻿using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services.CompanyService;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.CompanyTest
{
    [TestFixture]
    public class JobTests
    {
        private TestJobportalContext _context;
        IJobService _jobService;


       [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TestJobportalContext>()
           .UseInMemoryDatabase("JobPortalTestDb")
           .Options;

            _context = new TestJobportalContext(options);
            _context.Database.EnsureCreated();
            IRepository<Guid, Skill> skillRepository = new SkillRepository(_context);
            IRepository<Guid, Title> titlerepository = new TitleRepository(_context);
            ICompanyRepository CompanyRepository = new CompanyRepository(_context);
            IJobRepository _jobRepository = new JobRepository(_context);
            IJobSkillsRepository _jobSkillsRepository = new JobSkillsRepository(_context);


             _jobService = new JobService(_jobRepository, titlerepository, CompanyRepository, skillRepository, _jobSkillsRepository);

        }

            [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task AddJob_Success()
        {
            var jobDto = new PostJobDto
            {
                JobDescription = "Software Developer",
                Lpa = 100000,
                JobType = JobType.FullTime,
                ExperienceRequired = 2.0f,
                SkillsRequired = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                TitleId = TestJobportalContext.TitleId2

            };
            var a = TestJobportalContext.CompanyId1;
            var result = await _jobService.AddJob(jobDto, TestJobportalContext.CompanyId1);

            Assert.IsNotNull(result.job);
            Assert.AreEqual(2, result.notFoundSkills.Count);
        }

        [Test]
        public async Task AddJob_Fail_NotFoundSkill()
        {
            var jobDto = new PostJobDto
            {
                JobDescription = "Software Developer",
                Lpa = 100000,
                JobType = JobType.FullTime,
                ExperienceRequired = 2.0f,
                SkillsRequired = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, // Assuming one of these IDs is invalid
                TitleId = TestJobportalContext.TitleId2

            };
            var result = await _jobService.AddJob(jobDto, TestJobportalContext.CompanyId1);


            Assert.IsNotNull(result.notFoundSkills);
            Assert.AreEqual(2, result.notFoundSkills.Count); 
        }

        [Test]
        public async Task UpdateJob_Success()
        {


            var updatedJob = await _jobService.UpdateJob(new UpdateJobDto
            {
                JobId = TestJobportalContext.JobId1,
                JobDescription = "Updated Description",
                Lpa = 10f,
                JobType = JobType.FullTime,
                ExperienceRequired =2,
                TitleId= TestJobportalContext.TitleId2
            }, TestJobportalContext.CompanyId1);

            Assert.IsNotNull(updatedJob);
            Assert.AreEqual("Updated Description", updatedJob.JobDescription);
            Assert.AreEqual(10f, updatedJob.Lpa);
        }

        [Test]
        public async Task DeleteJob_Success()
        {

 
            var result = await _jobService.DeleteJob(TestJobportalContext.JobId1, TestJobportalContext.CompanyId1);

            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetJob_Success()
        {



            var job = await _jobService.GetJob(TestJobportalContext.JobId1, TestJobportalContext.CompanyId1);

            Assert.IsNotNull(job);
            Assert.AreEqual(TestJobportalContext.JobId1, job.JobId);
        }

        [Test]
        public async Task GetAllJobs_Success()
        {
 
            var jobs = await _jobService.GetAllJobs();

            Assert.That(jobs, Is.Not.Empty);
        }
        [Test]
        public async Task GetAllJobsPostedbycompany_Success()
        {
            Assert.ThrowsAsync<JobNotFoundException>(async () => await _jobService.GetAllJobsPosted(Guid.NewGuid())); 
                

        }
        [Test]
        public async Task UpdateJobSkills_Success()
        {
            // Arrange
            var jobSkillsDto = new JobSkillsDto
            {
                JobId = TestJobportalContext.JobId1,
                SkillsToAdd = new List<Guid> { TestJobportalContext .SkillId3 },
                SkillsToRemove = new List<Guid> { Guid.NewGuid(), TestJobportalContext.SkillId3 ,TestJobportalContext.SkillId1}
            };


            var result = await _jobService.UpdateJobSkills(jobSkillsDto, TestJobportalContext.CompanyId1);

            Assert.AreEqual(jobSkillsDto.SkillsToAdd.Count(), result.AddedSkills.Count());
        }

        [Test]
        public async Task Getjobs_Success()
        {

            var result = await _jobService.GetJobs(1,12);

            Assert.AreEqual(3,result.Count());
        }

    }
}
