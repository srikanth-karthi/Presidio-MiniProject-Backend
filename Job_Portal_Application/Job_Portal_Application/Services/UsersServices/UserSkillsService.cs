using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Dto;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Repository.SkillRepos;

namespace Job_Portal_Application.Services
{
    public class UserSkillsService : IUserSkillsService
    {
        private readonly IUserSkillsRepository _userSkillsRepository;
     
        private readonly IRepository<Guid, Skill> _skillRepository;

        public UserSkillsService(IUserSkillsRepository userSkillsRepository, IRepository<Guid, Skill> skillRepository)
        {
            _userSkillsRepository = userSkillsRepository;
        
            _skillRepository = skillRepository;
        }

        public async Task<UserSkillsResponseDto> AddUserSkills(UserSkillsRequestDto request, Guid UserId)
        {
            var userId = UserId;
            var response = new UserSkillsResponseDto();

            foreach (var skillId in request.SkillIds)
            {
                var skill = await _skillRepository.Get(skillId);
                if (skill == null)
                {
                    response.InvalidSkills.Add(skillId);
                    continue;
                }

                var existingUserSkill = await _userSkillsRepository.GetByUserIdAndSkillId(userId, skillId);
                if (existingUserSkill != null)
                {
                  
                    continue;
                }

                var userSkill = new UserSkills
                {
                    UserId = userId,
                    SkillId = skillId,
                    Skill = skill
                };

                var addedUserSkill = await _userSkillsRepository.Add(userSkill);
                response.Skills.Add(MapToDto(addedUserSkill));
            }

            return response;
        }

        public async Task<UserSkillsResponseDto> RemoveUserSkill(UserSkillsRequestDto request, Guid UserId)
        {
            var userId = UserId;
            var response = new UserSkillsResponseDto();

            foreach (var skillId in request.SkillIds)
            {
                var userSkill = await _userSkillsRepository.GetByUserIdAndSkillId(userId, skillId);
                if (userSkill == null)
                {
                    response.InvalidSkills.Add(skillId);
                    continue;
                }

                await _userSkillsRepository.Delete(userSkill);
                response.Skills.Add(MapToDto(userSkill));
            }

            return response;
        }

        public async Task<IEnumerable<UserSkillDto>> GetAllUserSkills( Guid UserId)
        {
            var userId = UserId;
            var userSkills = await _userSkillsRepository.GetByUserId(userId);
            if (!userSkills.Any()) throw new UserSkillsNotFoundException("User has no skills");
            return userSkills.Select(MapToDto);
        }

        private UserSkillDto MapToDto(UserSkills userSkill)
        {
            return new UserSkillDto
            {
                UserSkillsId = userSkill.UserSkillsId,
                UserId = userSkill.UserId,
                SkillId = userSkill.SkillId,
                SkillName = userSkill.Skill.Skill_Name
            };
        }
    }
}
