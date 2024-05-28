using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using System;
using System.ComponentModel.Design;
using Job_Portal_Application.Dto.Enums;


namespace Job_Portal_Application.Context
{
    public class JobportalContext : DbContext
    {
        public JobportalContext(DbContextOptions<JobportalContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<Title> Titles { get; set; }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobSkills> JobSkills { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobActivity> JobActivities { get; set; }
        public DbSet<AreasOfInterest> AreasOfInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // AreasOfInterest and Title
            modelBuilder.Entity<AreasOfInterest>()
                .HasOne(a => a.Title)
                .WithMany()
                .HasForeignKey(a => a.TitleId);

            // User and AreasOfInterest
            modelBuilder.Entity<User>()
                .HasMany(u => u.AreasOfInterests)
                .WithOne()
                .HasForeignKey(a => a.UserId);

            // User and Education
            modelBuilder.Entity<User>()
                .HasMany(u => u.Educations)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            // User and Experience
            modelBuilder.Entity<User>()
                .HasMany(u => u.Experiences)
                .WithOne()
                .HasForeignKey(e => e.UserId);

            // Experience and Title
            modelBuilder.Entity<Experience>()
                .HasOne(e => e.Title)
                .WithMany()
                .HasForeignKey(e => e.TitleId);

            // Job and JobSkills
            modelBuilder.Entity<Job>()
                .HasMany(j => j.JobSkills)
                .WithOne(js => js.Job)
                .HasForeignKey(js => js.JobId);

            // JobSkills and Skill
            modelBuilder.Entity<JobSkills>()
                .HasOne(js => js.Skill)
                .WithMany()
                .HasForeignKey(js => js.SkillId);

            // User and UserSkills
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSkills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            // UserSkills and Skill
            modelBuilder.Entity<UserSkills>()
                .HasOne(us => us.Skill)
                .WithMany()
                .HasForeignKey(us => us.SkillId);

            // Job and Company
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Company)
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CompanyId);

            // Job and Title
            modelBuilder.Entity<Job>()
                .HasOne(j => j.Title)
                .WithMany()
                .HasForeignKey(j => j.TitleId);

            // JobActivity and Job
            modelBuilder.Entity<JobActivity>()
                .HasOne(ja => ja.Job)
                .WithMany(j => j.JobActivities)
                .HasForeignKey(ja => ja.JobId)
                 .OnDelete(DeleteBehavior.Restrict);

            // JobActivity and User
            modelBuilder.Entity<JobActivity>()
                .HasOne(ja => ja.User)
                .WithMany(u => u.JobActivities)
                .HasForeignKey(ja => ja.UserId);

            // Seed data for Skills
            modelBuilder.Entity<Skill>().HasData(
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "HTML" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "CSS" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "JavaScript" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "TypeScript" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "React" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Angular" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Vue" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Node.js" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Express" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Python" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Django" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Flask" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Java" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Spring" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Kotlin" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Swift" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Objective-C" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Ruby" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Rails" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "PHP" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "C#" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "ASP.NET" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Azure" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "AWS" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "GCP" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "SQL" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "NoSQL" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Docker" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "Kubernetes" },
                new Skill { SkillId = Guid.NewGuid(), Skill_Name = "GraphQL" }
            );

            // Seed data for Titles
            modelBuilder.Entity<Title>().HasData(
                new Title { TitleId = Guid.NewGuid(), TitleName = "Full Stack Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Front End Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Back End Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Software Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Data Scientist" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "DevOps Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Product Manager" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Project Manager" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Business Analyst" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "QA Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "UI/UX Designer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Mobile Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Security Analyst" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Network Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Systems Administrator" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Database Administrator" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Cloud Architect" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Machine Learning Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Artificial Intelligence Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Technical Support Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Cloud Engineer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Database Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Blockchain Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "Game Developer" },
                new Title { TitleId = Guid.NewGuid(), TitleName = "VR/AR Developer" }
           
         
            );




            base.OnModelCreating(modelBuilder);
        }
    }
}
