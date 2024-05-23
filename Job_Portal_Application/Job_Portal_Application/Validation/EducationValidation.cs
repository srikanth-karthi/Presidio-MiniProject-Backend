using Job_Portal_Application.Dto;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Validation
{
    public class EducationValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var educationDto = (AddEducationDto)validationContext.ObjectInstance;

            if (!educationDto.IsCurrentlyStudying && !educationDto.EndYear.HasValue)
            {
                return new ValidationResult("EndYear is required if not currently studying.");
            }
            if (educationDto.EndYear.HasValue && educationDto.EndYear <= educationDto.StartYear)
            {
                return new ValidationResult("EndYear must be greater than StartYear.");
            }
            return ValidationResult.Success;
        }
    }
}
