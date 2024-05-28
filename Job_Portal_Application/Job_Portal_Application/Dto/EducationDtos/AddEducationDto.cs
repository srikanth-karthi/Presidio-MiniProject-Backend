using System;
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Validation;

namespace Job_Portal_Application.Dto.EducationDtos
{

    public class AddEducationDto
    {




        [Required(ErrorMessage = "Level is required")]
        [StringLength(50, ErrorMessage = "Level cannot exceed 50 characters")]
        public string Level { get; set; }

        [Required(ErrorMessage = "StartYear is required")]
        [DataType(DataType.Date)]
        public DateTime StartYear { get; set; }

        [DataType(DataType.Date)]
        public DateTime? EndYear { get; set; }

        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100")]
        public float Percentage { get; set; }

        [Required(ErrorMessage = "Institution Name is required")]
        [StringLength(255, ErrorMessage = "Institution Name cannot exceed 255 characters")]
        public string InstitutionName { get; set; }

        [EducationValidation]
        public bool IsCurrentlyStudying { get; set; }

    }
}
