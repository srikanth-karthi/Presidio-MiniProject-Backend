using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.AreasOfInterestDtos
{
    public class AddAreasOfInterestDTO
    {


        [Required(ErrorMessage = "TitleId is required")]
        public Guid TitleId { get; set; }

        [Required(ErrorMessage = "Lpa is required")]
        [Range(0, float.MaxValue, ErrorMessage = "Lpa must be a positive number")]
        public float Lpa { get; set; }


    }
}
