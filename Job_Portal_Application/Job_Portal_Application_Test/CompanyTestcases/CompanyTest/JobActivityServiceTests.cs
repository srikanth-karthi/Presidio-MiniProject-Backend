using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobActivityDto;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository;

using Job_Portal_Application.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application_Test.JobActivityTest
{
    [TestFixture]
    public class JobActivityServiceTests
    {
        private DbContextOptions _dbContextOptions;
        private TestJobportalContext _context;
        private JobActivityService _jobActivityService;

        [SetUp]
        public void Setup()
        {
            _dbContextOptions = new DbContextOptionsBuilder()
               .UseInMemoryDatabase("JobPortalTestDb")
               .Options;

            _context = new TestJobportalContext(_dbContextOptions);
            _context.Database.EnsureCreated();

            IJobActivityRepository jobActivityRepo = new JobActivityRepository(_context);
            IJobRepository jobRepo = new JobRepository(_context);

            _jobActivityService = new JobActivityService(jobRepo, jobActivityRepo);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task ApplyForJob_Success()
        {


            var result = await _jobActivityService.ApplyForJob(TestJobportalContext.JobId1, TestJobportalContext.UserId);

            Assert.IsNotNull(result);
            Assert.AreEqual(JobStatus.Applied.ToString(), result.Status);
        }

        [Test]
        public async Task ApplyForJob_Failure_ExistingApplication()
        {
            var result = await _jobActivityService.ApplyForJob(TestJobportalContext.JobId1, TestJobportalContext.UserId);
            Assert.ThrowsAsync<JobAlreadyAppliedException>(async () =>
              await _jobActivityService.ApplyForJob(TestJobportalContext.JobId1, TestJobportalContext.UserId));
        }


        [Test]
        public async Task GetFilteredUser_Success()
        {




            var users = await _jobActivityService.GetFilteredUser(TestJobportalContext.JobId2, TestJobportalContext.CompanyId1);

            Assert.IsTrue(users.Any(u => u.UserId == TestJobportalContext.UserId));
        }


        [Test]
        public async Task UpdateJobActivityStatus_Success()
        {

            var updateResult = await _jobActivityService.UpdateJobActivityStatus(new UpdateJobactivityDto { JobactivityId = TestJobportalContext.JobActivityId1, status = JobStatus.Interviewed,ResumeViewed=true});

            Assert.IsTrue(updateResult.ResumeViewed);
            Assert.AreEqual(JobStatus.Interviewed.ToString(), updateResult.Status);
        }
        [Test]
        public async Task GetuserAppliedJobs_Success()
        {

            var Result = await _jobActivityService.GetJobsUserApplied(TestJobportalContext.UserId);
            Assert.AreEqual(2, Result.Count());
        }


        [Test]
        public async Task GetJobActivityById_Success()
        {

      
            var jobActivity = await _jobActivityService.GetJobActivityById(TestJobportalContext.JobActivityId1);

            Assert.IsNotNull(jobActivity);
            Assert.AreEqual(jobActivity.UserJobId, TestJobportalContext.JobActivityId1);
        }


        [Test]
        public async Task GetJobActivitiesByJobId_Success()
        {
            

            // Retrieve job activities by job ID
            var activities = await _jobActivityService.GetJobActivitiesByJobId(TestJobportalContext.JobId2);

            Assert.AreEqual(2, activities.Count());

        }

    }
}
