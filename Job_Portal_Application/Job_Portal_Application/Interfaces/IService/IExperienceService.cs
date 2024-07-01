using Job_Portal_Application.Dto.EducationDtos;
using Job_Portal_Application.Dto.ExperienceDtos;
using Job_Portal_Application.Dto.profile;
using Job_Portal_Application.Dto.UserDto;
using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IExperienceService
    {
        Task<Experience> AddExperience(AddExperienceDto experienceDto, Guid UserId);
        Task<Experience> UpdateExperience(GetExperienceDto experienceDto, Guid UserId);
        Task<bool> DeleteExperience(Guid experienceId, Guid UserId);
        Task<Experience> GetExperience(Guid experienceId, Guid UserId);
        Task<IEnumerable<Experience>> GetAllExperiences( Guid UserId);
    }
}
