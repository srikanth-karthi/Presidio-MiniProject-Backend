using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Services.UsersServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Portal_Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly ISkillService _skillservice;
        private readonly ITitleService _titleservice;

        public AdminService(ISkillService skillservice, ITitleService titleservice)
        {
            _skillservice = skillservice;
            _titleservice = titleservice;
        }

        public async Task<Skill> CreateSkill(Skill skill)
        {
            if (skill == null || string.IsNullOrWhiteSpace(skill.SkillName))
            {
                throw new ArgumentNullException(nameof(skill), "Skill or Skill name cannot be null or empty.");
            }

            var existingSkills = await _skillservice.GetAllSkills();
            if (existingSkills.Any(s => s.SkillName != null && s.SkillName.Trim().ToLower() == skill.SkillName.Trim().ToLower()))
            {
                throw new SkillAlreadyExisitException("Skill already exists.");
            }

            return await _skillservice.AddSkills(skill);
        }

        public async Task<bool> DeleteSkill(Guid skillId)
        {
            var skill = await _skillservice.GetSkill(skillId) ?? throw new SkillNotFoundException("Skill not found.");
            return await _skillservice.DeleteSkills(skill);
        }

        public async Task<Title> CreateTitle(Title title)
        {
            if (title == null || string.IsNullOrWhiteSpace(title.TitleName))
            {
                throw new ArgumentNullException(nameof(title), "Title or Title name cannot be null or empty.");
            }

            var existingTitles = await _titleservice.GetAllTitles();
            if (existingTitles.Any(t => t.TitleName != null && t.TitleName.Trim().ToLower() == title.TitleName.Trim().ToLower()))
            {
                throw new TitleAlreadyExisitException("Title already exists.");
            }

            return await _titleservice.AddSkills(title);
        }

        public async Task<bool> DeleteTitle(Guid titleId)
        {
            var title = await _titleservice.GetTitle(titleId) ?? throw new TitleNotFoundException("Title not found.");
            return await _titleservice.DeleteSkills(title);
        }
    }
}
