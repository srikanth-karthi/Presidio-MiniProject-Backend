using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Job_Portal_Application.Models;
using Job_Portal_Application.Validation;

namespace Job_Portal_Application.Dto.ExperienceDtos
{

    public class AddExperienceDto
    {

 

        [Required(ErrorMessage = "CompanyName is required")]
        [StringLength(255, ErrorMessage = "CompanyName cannot exceed 255 characters")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "TitleId is required")]
        public Guid TitleId { get; set; }

        [Required(ErrorMessage = "StartYear is required")]
        [DataType(DataType.Date)]
        public DateTime StartYear { get; set; }

        [Required(ErrorMessage = "EndYear is required")]
        [DataType(DataType.Date)]
        [ExperienceValidation]
        public DateTime EndYear { get; set; }


    }

}
