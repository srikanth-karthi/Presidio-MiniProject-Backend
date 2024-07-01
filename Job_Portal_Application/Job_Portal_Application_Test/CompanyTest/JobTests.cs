using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Dto.JobDto;
using Job_Portal_Application.Dto.JobDtos;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository;
using Job_Portal_Application.Repository.CompanyRepos;
using Job_Portal_Application.Repository.SkillRepos;
using Job_Portal_Application.Repository.UserRepos;
using Job_Portal_Application.Services;
using Job_Portal_Application.Services.CompanyService;
using Job_Portal_Application.Services.UsersServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Configuration;
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
            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
              {
            { "TokenKey:JWT", "y_J82VYvg5Jh8-DT89E1kz_FzHNN3tB_Sy4b_yLhoZ8Y6q-jVOWU3-xPFlPS6cYYicWlb0XhREXAf3OWTbnN3w==" }
              })
            .Build();
            _context = new TestJobportalContext(options);
            _context.Database.EnsureCreated();
            IRepository<Guid, Skill> skillRepository = new SkillRepository(_context);
            IRepository<Guid, Title> titlerepository = new TitleRepository(_context);
            ICompanyRepository CompanyRepository = new CompanyRepository(_context);
            IJobRepository _jobRepository = new JobRepository(_context);
            IJobSkillsRepository _jobSkillsRepository = new JobSkillsRepository(_context);
            MinIOService _minioService = new MinIOService(configuration);
            IUserRepository userRepo = new UserRepository(_context);
            ITokenService tokenService = new TokenServices(configuration);
            ICompanyRepository companyRepo = new CompanyRepository(_context);
            IJobRepository jobRepository = new JobRepository(_context);
            IRepository<Guid, Skill> _skillRepository = new SkillRepository(_context);
            IRepository<Guid, Credential> _credentialRepository = new CredentialRepository(_context);
            IUserSkillsRepository _userSkillsRepository = new UserSkillsRepository(_context);

         IUserService   _userService = new UserService(
                      _skillRepository,
                       companyRepo,
                          _userSkillsRepository,
                            _credentialRepository,
                            jobRepository,
                            userRepo,
                            tokenService,
                            _minioService
            );

            _jobService = new JobService(_userService,_jobRepository, titlerepository, CompanyRepository, skillRepository, _jobSkillsRepository);

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
                SkillsRequired = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() }, 
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

            var jobs = await _jobService.GetAllJobs(TestJobportalContext.CompanyId1);

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
          
            var jobSkillsDto = new JobSkillsdto
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
            var result = await _jobService.GetJobs(new Guid(),1,12);
            Assert.AreEqual(3,result.Count());
        }

    }
}
