using Job_Portal_Application.Dto.EducationDto;
using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IEducationService
    {
        Task<Education> AddEducation(AddEducationDto educationDto);
        Task<Education> UpdateEducation(EducationDto educationDto);
        Task<bool> DeleteEducation(Guid educationId);
        Task<EducationDto> GetEducation(Guid educationId);
        Task<IEnumerable<EducationDto>> GetAllEducations();
    }
}
