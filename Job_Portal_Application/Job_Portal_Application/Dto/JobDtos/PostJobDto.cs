using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobDto
{
    public class PostJobDto
    {

        [Required(ErrorMessage = "Job type is required")]
        public JobType JobType { get; set; }

        [Required(ErrorMessage = "Title ID is required")]
        public Guid TitleId { get; set; }




        [Range(0, double.MaxValue, ErrorMessage = "Experience required must be a positive number")]
        public float? ExperienceRequired { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "LPA must be a positive number")]
        public float? Lpa { get; set; }



        [StringLength(1000, ErrorMessage = "Job description must be at most 1000 characters")]
        public string JobDescription { get; set; }



        public List<Guid>? SkillsRequired { get; set; }

    }
}
