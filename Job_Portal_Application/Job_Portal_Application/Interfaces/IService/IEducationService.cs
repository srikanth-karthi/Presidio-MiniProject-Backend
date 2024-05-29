using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IEducationService
    {
        Task<Education> AddEducation(AddEducationDto educationDto, Guid UserId);
        Task<Education> UpdateEducation(EducationDto educationDto, Guid UserId);
        Task<bool> DeleteEducation(Guid educationId, Guid UserId);
        Task<EducationDto> GetEducation(Guid educationId, Guid UserId);
        Task<IEnumerable<EducationDto>> GetAllEducations( Guid UserId);
    }
}
