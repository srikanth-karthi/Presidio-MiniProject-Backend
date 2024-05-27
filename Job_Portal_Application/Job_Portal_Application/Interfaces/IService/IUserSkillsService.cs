using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Dto;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface IUserSkillsService
    {
        Task<UserSkillsResponseDto> AddUserSkills(UserSkillsRequestDto request);
        Task<UserSkillsResponseDto> RemoveUserSkill(UserSkillsRequestDto request);
        Task<IEnumerable<UserSkillDto>> GetAllUserSkills();
    }
}
