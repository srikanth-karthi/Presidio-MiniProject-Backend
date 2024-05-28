using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Job_Portal_Application.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] HasCode { get; set; }


        public DateOnly Dob { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public string? City { get; set; }

        [MaxLength(200)]
        [Url]
        public string? PortfolioLink { get; set; }

        [Phone]
        public string? Phonenumber { get; set; }

        [MaxLength(200)]
        [Url]
        public string? ResumeUrl { get; set; }




        public ICollection<Education> Educations { get; set; } = [];
        public ICollection<Experience> Experiences { get; set; } = [];
        public ICollection<UserSkills> UserSkills { get; set; } = [];
        public ICollection<JobActivity> JobActivities { get; set; } = [];
        public ICollection<AreasOfInterest> AreasOfInterests { get; set; } = [];
    }
}
