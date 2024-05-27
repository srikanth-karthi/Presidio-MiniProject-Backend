using Job_Portal_Application.Dto.ExperienceDto;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.EducationDto
{
    public class ExperienceDto : AddExperienceDto
    {


        [Required(ErrorMessage = "ExperienceId is required")]
        public Guid ExperienceId { get; set; }
    }
}
