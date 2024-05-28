
using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Dto.AreasOfInterestDtos
{
    public class AreasOfInterestDto : AddAreasOfInterestDTO
    {
        [Required(ErrorMessage = "AreasOfInterestId is required")]
        public Guid AreasOfInterestId { get; set; }



    }
}
