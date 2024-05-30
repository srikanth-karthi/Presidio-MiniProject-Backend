using Job_Portal_Application.Context;
using Job_Portal_Application.Dto.Enums;
using Job_Portal_Application.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

public class TestJobportalContext : JobportalContext
{
    public TestJobportalContext(DbContextOptions options)
        : base(options)
    {
    }

    public static readonly Guid UserSkillId1 = Guid.Parse("9fa07a27-a0c3-4158-a6c2-36d56e9982a3");
    public static readonly Guid Credential1 = Guid.Parse("0cd2f7f3-0517-45b4-a6fa-2c9d76074ec1");
    public static readonly Guid Credential2 = Guid.Parse("77f4bba6-63cc-437b-9d0f-15130f857e6a");
    public static readonly Guid Credential3 = Guid.Parse("13d44e5e-fb16-4082-8ee6-0f14644ad351");
    public static readonly Guid UserSkillId2 = Guid.Parse("aef452ff-01ae-4c7d-8b91-4c27c72a7c1d");
    public static readonly Guid UserId = Guid.Parse("c20b7faa-890d-4908-a57a-a9ea75b82b5f");
    public static readonly Guid EducationId1 = Guid.Parse("5b5535e7-f592-4006-b257-35d015c67115");
    public static readonly Guid EducationId2 = Guid.Parse("0c6d73e0-1bc7-493d-b013-5786114b1d35");
    public static readonly Guid ExperienceId1 = Guid.Parse("699e7a3a-eb03-4191-ae61-26ab54959ee4");
    public static readonly Guid ExperienceId2 = Guid.Parse("b8b5e38a-277b-49f4-b361-7a9c3fba22e4");
    public static readonly Guid TitleId1 = Guid.Parse("23b60721-74b9-4209-9a57-386ffeebd57f");
    public static readonly Guid TitleId2 = Guid.Parse("2b24c3c2-9cc0-43d8-b186-bfe9f84a41c8");
    public static readonly Guid SkillId1 = Guid.Parse("1686b4cc-0d39-4952-a3cc-12e41a907d7b");
    public static readonly Guid SkillId2 = Guid.Parse("4e0e88c8-1244-493b-88f7-9577474f65ac");
    public static readonly Guid SkillId3 = Guid.Parse("cb0559ee-e156-4c4d-985b-13f3fc966725");
    public static readonly Guid AreaOfInterestId1 = Guid.Parse("db0f9cf9-78de-4a33-923b-14d642a5ef45");
    public static readonly Guid AreaOfInterestId2 = Guid.Parse("03c9b7b3-0c1a-4d6e-af00-f81d44003db1");
    public static readonly Guid CompanyId1 = Guid.Parse("5d04d46f-9279-4c7f-9c55-9dff63181af5");
    public static readonly Guid CompanyId2 = Guid.Parse("0d3445b2-6d2b-4293-91c1-2958df4d53b2");
    public static readonly Guid JobId1 = Guid.Parse("7764c3bb-84cd-4dc7-b7f7-1b1a5dc5a8ec");
    public static readonly Guid JobId2 = Guid.Parse("4f622e3c-05b7-431b-9e71-fd007a2b69a7");
    public static readonly Guid JobId3 = Guid.Parse("3767e4a7-4e42-4419-8343-6b1a1d4d38f4");
    public static readonly Guid JobActivityId1 = Guid.Parse("6ce122a6-91f9-4e8f-9050-0652ff78ff74");
    public static readonly Guid JobActivityId2 = Guid.Parse("699e7a3a-eb03-4191-ae61-26ab54959ee4");


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     


        using (var hmacSha = new HMACSHA512())
        {

                        modelBuilder.Entity<Credential>().HasData(
                new Credential()
                {
                    CredentialId= Credential1,
                    Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes("123")),
                    HasCode = hmacSha.Key,
                    Role = Roles.User,
                });
            modelBuilder.Entity<Credential>().HasData(
                new Credential()
                {
                CredentialId = Credential2,
                Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes("123")),
                HasCode = hmacSha.Key,
                Role = Roles.User,
                });
                            modelBuilder.Entity<Credential>().HasData(
                new Credential()
                {
                CredentialId = Credential3,
                Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes("123")),
                HasCode = hmacSha.Key,
                Role = Roles.User,
                });
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = UserId,
                    Name = "Test",
                    Email = "test.user@example.com",
                    Dob = new DateOnly(2021, 1, 1),
                    CredentialId = Credential1,
                  
                }
            );
            modelBuilder.Entity<UserSkills>().HasData(
                new UserSkills
                {
                    UserSkillsId = UserSkillId1,
                    UserId = UserId,
                    SkillId = SkillId1
                },
                new UserSkills
                {
                    UserSkillsId = UserSkillId2,
                    UserId = UserId,
                    SkillId = SkillId2
                }
);
            modelBuilder.Entity<Education>().HasData(
                new Education
                {
                    EducationId = EducationId1,
                    UserId = UserId,
                    InstitutionName = "Test University 1",
                    Level = "Bachelors",
                    Percentage = 9.8f,
                    IsCurrentlyStudying = false,
                    StartYear = new DateOnly(2019, 2, 1),
                    EndYear = new DateOnly(2021, 1, 1)
                },
                new Education
                {
                    EducationId = EducationId2,
                    UserId = UserId,
                    InstitutionName = "Test University 2",
                    Level = "Masters",
                    Percentage = 9.5f,
                    IsCurrentlyStudying = true,
                    StartYear = new DateOnly(2021, 2, 1),
                    EndYear = null
                }
            );

            modelBuilder.Entity<Experience>().HasData(
                new Experience
                {
                    ExperienceId = ExperienceId1,
                    UserId = UserId,
                    TitleId = TitleId1,
                    CompanyName = "Test Company 1",
                    StartYear = new DateOnly(2019, 2, 1),
                    EndYear = new DateOnly(2021, 1, 1)
                    
                },
                new Experience
                {
                    ExperienceId = ExperienceId2,
                    UserId = UserId,
                    TitleId = TitleId2,
                    CompanyName = "Test Company 2",
                    StartYear = new DateOnly(2021, 2, 1),
                    EndYear = new DateOnly(2021, 1, 1)
                }
            );


            modelBuilder.Entity<Title>().HasData(
                new Title
                {
                    TitleId = TitleId1,
                    TitleName = "Software Developer"
                },
                new Title
                {
                    TitleId = TitleId2,
                    TitleName = "Software Engineer"
                }
            );

            modelBuilder.Entity<Skill>().HasData(
                new Skill
                {
                    SkillId = SkillId1,
                    Skill_Name = "C#"
                },
                new Skill
                {
                    SkillId = SkillId2,
                    Skill_Name = "ASP.NET Core"
                },
                  new Skill
                  {
                      SkillId = SkillId3,
                      Skill_Name = "ASP.NET"
                  }
            );

            modelBuilder.Entity<AreasOfInterest>().HasData(
                new AreasOfInterest
                {
                    AreasOfInterestId = AreaOfInterestId1,
                    UserId = UserId,
                    TitleId = TitleId1,
                },
                new AreasOfInterest
                {
                    AreasOfInterestId = AreaOfInterestId2,
                    UserId = UserId,
                    TitleId = TitleId2,
                    Lpa = 3
                }
            );

            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    CompanyId = CompanyId1,
                    CompanyName = "Tech Corp",
                    Email = "contact@techcorp.com",
                    CredentialId=Credential2,
                    CompanyAddress = "123 Tech Street",
                    City = "Tech City",
                    CompanySize = 1000,
                    CompanyWebsite = "https://www.techcorp.com",
                    CompanyDescription=""
                },
                new Company
                {
                    CompanyId = CompanyId2,
                    CompanyName = "Innovate LLC",
                    Email = "contact@innovate.com",
                    CredentialId = Credential3,
                    CompanyAddress = "456 Innovation Way",
                    City = "Innovation City",
                    CompanySize = 500,
                    CompanyWebsite = "https://www.innovate.com",
                          CompanyDescription = ""
                }
            );

            modelBuilder.Entity<Job>().HasData(
                new Job
                {
                    JobId = JobId1,
                    CompanyId = CompanyId1,
                    JobType = JobType.FullTime,
                    TitleId = TitleId1,
                    ExperienceRequired = 2,
                    Lpa = 5,
                    JobDescription = "Develop and maintain web applications.",
                    Status = true
                },
                new Job
                {
                    JobId = JobId2,
                    CompanyId = CompanyId1,
                    JobType = JobType.PartTime,
                    TitleId = TitleId2,
                    ExperienceRequired = 1,
                    Lpa = 3,
                    JobDescription = "Assist in developing software projects.",
                    Status = true
                },
                new Job
                {
                    JobId = JobId3,
                    CompanyId = CompanyId2,
                    JobType = JobType.Internship,
                    TitleId = TitleId1,
                    ExperienceRequired = 0,
                    Lpa = 1,
                    JobDescription = "Support the development team with various tasks.",
                    Status = true
                }
            );

            modelBuilder.Entity<JobSkills>().HasData(
                new JobSkills
                {
                    JobSkillsId = Guid.NewGuid(),
                    JobId = JobId1,
                    SkillId = SkillId1
                },
                new JobSkills
                {
                    JobSkillsId = Guid.NewGuid(),
                    JobId = JobId1,
                    SkillId = SkillId2
                },
                new JobSkills
                {
                    JobSkillsId = Guid.NewGuid(),
                    JobId = JobId2,
                    SkillId = SkillId1
                },
                new JobSkills
                {
                    JobSkillsId = Guid.NewGuid(),
                    JobId = JobId3,
                    SkillId = SkillId2
                }
            );

            modelBuilder.Entity<JobActivity>().HasData(

             new JobActivity
             {
                 JobApplicationId = JobActivityId2,
                 UserId = UserId,
                 JobId = JobId2,
                 Status = JobStatus.Applied,
                 ResumeViewed = false,
                 Comments = "Interested in part-time work.",
                 AppliedDate = DateOnly.FromDateTime(DateTime.Now)
             },
             new JobActivity
             {
                 JobApplicationId = JobActivityId1,
                 UserId = UserId,
                 JobId = JobId2,
                 Status = JobStatus.Applied,
                 ResumeViewed = false,
                 Comments = "Seeking an internship to gain experience.",
                 AppliedDate = DateOnly.FromDateTime(DateTime.Now)
             }
     );
            base.OnModelCreating(modelBuilder);
        }
    }
}

