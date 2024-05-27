
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto.CompanyDtos;

namespace Job_Portal_Application.Dto.CompanyDto
{
    public class CompanyDto : CompanyRegisterDto
    {


        [Required(ErrorMessage = "CompanyId is required")]
        public Guid CompanyId { get; set; }

    }
}
