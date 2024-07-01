
using System.ComponentModel.DataAnnotations;
using Job_Portal_Application.Dto.CompanyDtos;

namespace Job_Portal_Application.Dto.CompanyDto
{
    public class CompanyDto : CompanyRegisterDto
    {


        [Required(ErrorMessage = "CompanyId is required")]
        public Guid CompanyId { get; set; }
        public string? logoUrl { get;  set; }

        [Range(1, int.MaxValue, ErrorMessage = "Company size must be greater than 0")]
        public int? CompanySize { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string? CompanyWebsite { get; set; }

        public string? CompanyDescription { get; set; }

     
    }
}
