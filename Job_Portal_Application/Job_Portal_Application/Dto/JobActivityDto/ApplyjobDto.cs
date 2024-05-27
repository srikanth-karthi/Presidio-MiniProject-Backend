using System;
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto.Enums;

namespace Job_Portal_Application.Dto.JobActivityDto
{
    public class ApplyjobDto
    {

        [Required(ErrorMessage = "Job ID is required")]
        public Guid JobId { get; set; }


    }
}
