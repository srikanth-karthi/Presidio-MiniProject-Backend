using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;
using System;
using System.ComponentModel.Design;
using Job_Portal_Application.Dto.Enums;
using System.Security.Cryptography;
using System.Text;
using Job_Portal_Application.Dto.ExperienceDtos;


namespace Job_Portal_Application.Context
{
    public class JobportalContext : DbContext
    {
        public JobportalContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Experience> Experiences { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Credential> Credential { get; set; }   
        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobSkills> JobSkills { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobActivity> JobActivities { get; set; }
        public DbSet<AreasOfInterest> AreasOfInterests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //users and Creadential
            modelBuilder.Entity<User>()
            .HasOne(u => u.Credential)
            .WithOne()
            .HasForeignKey<User>(u => u.CredentialId);


            //Company and Creadential
            modelBuilder.Entity<Company>()
           .HasOne(u => u.Credential)
           .WithOne()
           .HasForeignKey<Company>(u => u.CredentialId);



            modelBuilder.Entity<Credential>().HasKey(c => c.CredentialId);
             

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
                .HasForeignKey(js => js.SkillId)
                             .OnDelete(DeleteBehavior.Restrict);

            // User and UserSkills
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserSkills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            // UserSkills and Skill
            modelBuilder.Entity<UserSkills>()
                .HasOne(us => us.Skill)
                .WithMany()
                .HasForeignKey(us => us.SkillId)
                             .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<Skill>().HasData(
          new Skill { SkillId = Guid.NewGuid(), SkillName = "HTML" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "CSS" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "JavaScript" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "TypeScript" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "React" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Angular" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Vue" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Node.js" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Express" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Python" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Django" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Flask" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Java" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Spring" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Kotlin" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Swift" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Objective-C" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Ruby" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Rails" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "PHP" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "C#" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "ASP.NET" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Azure" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "AWS" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "GCP" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "SQL" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "NoSQL" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Docker" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Kubernetes" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "GraphQL" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "SASS" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "LESS" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Webpack" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Babel" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Redux" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "MobX" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Jest" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Mocha" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Chai" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Cucumber" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "C++" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "C" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Go" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Rust" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Perl" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Shell Scripting" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "PowerShell" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Terraform" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Ansible" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Puppet" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Chef" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Splunk" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "ELK Stack" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Prometheus" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Grafana" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Jenkins" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "CircleCI" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Travis CI" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Git" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Mercurial" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Subversion" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Project Management" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Agile Methodology" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Scrum" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Kanban" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Communication" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Teamwork" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Leadership" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Problem-Solving" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Time Management" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Critical Thinking" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Creativity" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Adaptability" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Attention to Detail" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Conflict Resolution" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Negotiation" },
          new Skill { SkillId = Guid.NewGuid(), SkillName = "Customer Service" }
      );

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
    new Title { TitleId = Guid.NewGuid(), TitleName = "VR/AR Developer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Site Reliability Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Embedded Systems Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Firmware Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IoT Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Robotics Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Computer Vision Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Natural Language Processing Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Bioinformatics Specialist" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Cryptographer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Penetration Tester" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Incident Response Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Cloud Security Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Ethical Hacker" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Digital Forensics Specialist" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Support Specialist" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Help Desk Technician" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Technical Writer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "UX Researcher" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Director" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Chief Technology Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Scrum Master" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Agile Coach" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Technical Lead" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Software Architect" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Engineering Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Chief Information Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Chief Data Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Consultant" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Enterprise Architect" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Security Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Data Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Data Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Business Intelligence Developer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Big Data Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Hadoop Developer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Solution Architect" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "DevSecOps Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Information Security Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Site Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Hardware Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Auditor" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Compliance Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Quality Assurance Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Operations Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Release Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Build Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Configuration Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Trainer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "ERP Consultant" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "CRM Developer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Business Systems Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Systems Analyst" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Network Architect" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Telecommunications Specialist" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Procurement Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Enterprise Systems Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Chief Information Security Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Information Technology Specialist" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Application Support Engineer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Project Coordinator" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Project Manager" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "IT Director" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Chief Information Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Data Privacy Officer" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Systems Integrator" },
    new Title { TitleId = Guid.NewGuid(), TitleName = "Software Development Manager" }
);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var enumProperties = entityType.ClrType.GetProperties()
                    .Where(p => p.PropertyType.IsEnum);

                foreach (var property in enumProperties)
                {
                    modelBuilder.Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion<string>();
                }
            }


            //Creating DefaultAdmin
            using var hmacSha = new HMACSHA512();
            modelBuilder.Entity<Credential>().HasData(
                new Credential
                {
                    CredentialId = Guid.Parse("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"),

                    Password = hmacSha.ComputeHash(Encoding.UTF8.GetBytes("Admin@jobportal")),
                    HasCode = hmacSha.Key,
                    Role = Roles.Admin
                }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    CredentialId = Guid.Parse("bf0e4d0f-f8d4-4bb5-839e-2f34d9f6c6a4"),
                    Name = "Admin",
                    Email = "Admin@jobportal.com",

                }




            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
