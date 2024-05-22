using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;

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

        public DbSet<Title> Titles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobActivity> JobActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Education>()
                .HasOne<User>()
                .WithMany(u => u.Educations)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<Experience>()
                .HasOne<User>()
                .WithMany(u => u.Experiences)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Experience>()
                .HasOne(e => e.Title)
                .WithMany()
                .HasForeignKey(e => e.TitleId);



   
            modelBuilder.Entity<Skill>()
                .HasOne<User>()
                .WithMany(u => u.UserSkills)
                .HasForeignKey("UserId"); 

            modelBuilder.Entity<Job>()
                .HasOne<Company>()
                .WithMany(c => c.Jobs)
                .HasForeignKey(j => j.CompanyId);

         
            modelBuilder.Entity<Skill>()
                .HasOne<Job>()
                .WithMany(j => j.SkillsRequired)
                .HasForeignKey("JobId");


            modelBuilder.Entity<Job>()
                .HasOne(e => e.Title)
                .WithMany()
                .HasForeignKey(e => e.TitleId);

            modelBuilder.Entity<JobActivity>()
                .HasOne(j=>j.Job)
                .WithMany(e=>e.JobActivities)
              .HasForeignKey(j => j.JobId);


            modelBuilder.Entity<JobActivity>()
    .HasOne(j => j.User)
    .WithMany(e => e.JobActivities)
  .HasForeignKey(j => j.UserId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
