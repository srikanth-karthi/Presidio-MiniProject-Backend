using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.EducationDtos
{
    public class EducationDto:AddEducationDto
    {


        [Required(ErrorMessage = "EducationId is required")]
        public Guid EducationId { get; set; }
    }
}
