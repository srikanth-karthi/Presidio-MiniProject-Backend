using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Models;
using Job_Portal_Application.Services.UsersServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Guid, Skill> _skillRepository;
        private readonly IRepository<Guid, Title> _titleRepository;

        public AdminService(IRepository<Guid, Skill> skillRepository, IRepository<Guid, Title> titleRepository)
        {
            _skillRepository = skillRepository;
            _titleRepository = titleRepository;
        }

        public async Task<Skill> CreateSkill(Skill skill)
        {
            if (skill == null || string.IsNullOrWhiteSpace(skill.Skill_Name))
            {
                throw new ArgumentNullException(nameof(skill), "Skill or Skill name cannot be null or empty.");
            }

            var existingSkills = await _skillRepository.GetAll();
            if (existingSkills.Any(s => s.Skill_Name != null && s.Skill_Name.Trim().ToLower() == skill.Skill_Name.Trim().ToLower()))
            {
                throw new SkillAlreadyExisitException("Skill already exists.");
            }

            return await _skillRepository.Add(skill);
        }

        public async Task<bool> DeleteSkill(Guid skillId)
        {
            var skill = await _skillRepository.Get(skillId) ?? throw new SkillNotFoundException("Skill not found.");
            return await _skillRepository.Delete(skill);
        }

        public async Task<Title> CreateTitle(Title title)
        {
            if (title == null || string.IsNullOrWhiteSpace(title.TitleName))
            {
                throw new ArgumentNullException(nameof(title), "Title or Title name cannot be null or empty.");
            }

            var existingTitles = await _titleRepository.GetAll();
            if (existingTitles.Any(t => t.TitleName != null && t.TitleName.Trim().ToLower() == title.TitleName.Trim().ToLower()))
            {
                throw new TitleAlreadyExisitException("Title already exists.");
            }

            return await _titleRepository.Add(title);
        }

        public async Task<bool> DeleteTitle(Guid titleId)
        {
            var title = await _titleRepository.Get(titleId) ?? throw new TitleNotFoundException("Title not found.");
            return await _titleRepository.Delete(title);
        }
    }
}
