using System;
using System.Collections.Generic;

namespace Job_Portal_Application.Dto.profile
{
    public class EducationDto
    {
        public Guid EducationId { get; set; }
        public string Level { get; set; }
        public DateOnly StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public float Percentage { get; set; }
        public string InstitutionName { get; set; }
        public bool IsCurrentlyStudying { get; set; }
    }

    public class ExperienceDto
    {
        public Guid ExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string TitleName { get; set; }
        public Guid titleId { get; set; }
        public DateOnly StartYear { get; set; }
        public DateOnly EndYear { get; set; }
        public int ExperienceDuration { get; set; }
    }

    public class UserSkillDto
    {
        public Guid SkillId { get; set; }
        public string SkillName { get; set; }
    }

    public class AreaOfInterestDto
    {
        public Guid AreasOfInterestId { get; set; }
        public string TitleName { get; set; }

        public Guid titleId { get; set; }
        public float Lpa { get; set; }
    }

    public class UserProfileDto : Userdetails
    {
        public ICollection<EducationDto> Educations { get; set; }
        public ICollection<ExperienceDto> Experiences { get; set; }
        public ICollection<UserSkillDto> UserSkills { get; set; }
        public ICollection<AreaOfInterestDto> AreasOfInterests { get; set; }
    }


    public class Userdetails
    {
        public string? ProfilePictureUrl { get; set; }
        public Guid UserId { get; set; }
        public DateTime Dob { get; set; }

        public string AboutMe {  get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PortfolioLink { get; set; }
        public string PhoneNumber { get; set; }
        public string ResumeUrl { get; set; }
    }
}
