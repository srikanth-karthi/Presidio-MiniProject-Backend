using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Job_Portal_Application.Validation;

namespace Job_Portal_Application.Dto
{
    [ExperienceValidation]
    public class AddExperienceDto
    {


        [Required(ErrorMessage = "CompanyName is required")]
        [StringLength(255, ErrorMessage = "CompanyName cannot exceed 255 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "TitleId is required")]
        public Guid TitleId { get; set; }

        [Required(ErrorMessage = "StartYear is required")]
        public DateOnly StartYear { get; set; }

        [Required(ErrorMessage = "EndYear is required")]
        public DateOnly EndYear { get; set; }

        [NotMapped]
        public int ExperienceDuration
        {
            get
            {
                return EndYear.Year - StartYear.Year;
            }
        }
    }
}
