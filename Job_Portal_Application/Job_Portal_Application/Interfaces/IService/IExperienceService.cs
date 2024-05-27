using Job_Portal_Application.Dto.EducationDto;
using Job_Portal_Application.Dto.ExperienceDto;
using Job_Portal_Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IExperienceService
    {
        Task<Experience> AddExperience(AddExperienceDto experienceDto);
        Task<Experience> UpdateExperience(ExperienceDto experienceDto);
        Task<bool> DeleteExperience(Guid experienceId);
        Task<Experience> GetExperience(Guid experienceId);
        Task<IEnumerable<Experience>> GetAllExperiences();
    }
}
