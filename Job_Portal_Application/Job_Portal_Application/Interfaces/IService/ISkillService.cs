using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Interfaces.IService
{
    public interface ISkillService
    {
        Task<Skill> GetSkill(Guid id);
        Task<IEnumerable<Skill>> GetAllSkills();
        Task<Skill> AddSkills(Skill skill);
        Task<bool> DeleteSkills(Skill skill);

    }
}
