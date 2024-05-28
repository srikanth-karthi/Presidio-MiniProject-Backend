
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.ExperienceDtos
{
    public class GetExperienceDto : AddExperienceDto
    {


        [Required(ErrorMessage = "ExperienceId is required")]
        public Guid ExperienceId { get; set; }
    }
}
