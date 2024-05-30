using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Interfaces.IService;
using Job_Portal_Application.Models;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Exceptions;

namespace Job_Portal_Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Guid, Skill> _skillRepository;

        public SkillService(IRepository<Guid, Skill> skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<Skill> GetSkill(Guid id)
        {
            return await _skillRepository.Get(id) ?? throw new SkillNotFoundException("Skill not found");
        }

        public async Task<IEnumerable<Skill>> GetAllSkills()
        {
            var skills= await _skillRepository.GetAll();
            if(!skills.Any()) throw new SkillNotFoundException("Skill not found");
            return skills;
        }

        public async Task<Skill> AddSkills(Skill skill)
        { 
            return await _skillRepository.Add(skill);
        }

        public async Task<bool> DeleteSkills(Skill skill)
        {
            return await _skillRepository.Delete(skill);
        }
    }
}
