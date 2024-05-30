using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces.IRepository;
using Job_Portal_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Application.Repository.UserRepos
{
    public class UserSkillsRepository : IUserSkillsRepository
    {
        private readonly JobportalContext _context;

        public UserSkillsRepository(JobportalContext context)
        {
            _context = context;
        }

        public async Task<UserSkills> Add(UserSkills entity)
        {
            _context.UserSkills.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(UserSkills userSkills)
        {
            _context.UserSkills.Remove(userSkills);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserSkills> Get(Guid id)
        {
            return await _context.UserSkills
                .Include(us => us.User)
                .Include(us => us.Skill)
                .FirstOrDefaultAsync(us => us.UserSkillsId == id);
        }

        public async Task<IEnumerable<UserSkills>> GetAll()
        {
            return await _context.UserSkills
                .ToListAsync();
        }


        public async Task<IEnumerable<UserSkills>> GetByUserId(Guid userId)
        {
            return await _context.UserSkills
                .Include(us => us.Skill)
                .Where(us => us.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserSkills> GetByUserIdAndSkillId(Guid userId, Guid skillId)
        {
            return await _context.UserSkills
                .Include(us => us.Skill)
                .FirstOrDefaultAsync(us => us.UserId == userId && us.SkillId == skillId);
        }

        public async Task<UserSkills> Update(UserSkills entity)
        {
            _context.UserSkills.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
