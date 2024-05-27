using Job_Portal_Application.Dto.ExperienceDto;
using System;
using System.ComponentModel.DataAnnotations;

namespace Job_Portal_Application.Validation
{
    public class ExperienceValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var experienceDto = (AddExperienceDto)validationContext.ObjectInstance;

            if (experienceDto.EndYear <= experienceDto.StartYear)
            {
                return new ValidationResult("EndYear must be greater than or equal to StartYear.");
            }

            return ValidationResult.Success;
        }
    }
}
