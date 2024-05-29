using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Job_Portal_Application.Dto.EducationDtos;

namespace Job_Portal_Application.Validation
{
    [ExcludeFromCodeCoverage]
    public class EducationValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var educationDto = (AddEducationDto)validationContext.ObjectInstance;


            if (!educationDto.IsCurrentlyStudying && !educationDto.EndYear.HasValue)
            {
                return new ValidationResult("EndYear is required if not currently studying.");
            }

    
            if (educationDto.IsCurrentlyStudying && educationDto.EndYear.HasValue)
            {
                return new ValidationResult("EndYear should not be provided if currently studying.");
            }

          
            if (educationDto.EndYear.HasValue && educationDto.EndYear <= educationDto.StartYear)
            {
                return new ValidationResult("EndYear must be greater than StartYear.");
            }

            return ValidationResult.Success;
        }
    }
}
