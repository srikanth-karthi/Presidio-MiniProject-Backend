using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Repository
{
    public class UserSkillsRepository : IRepository<Guid, UserSkills>
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

        public async Task<bool> Delete(Guid id)
        {
            var userSkills = await Get(id);
            _context.UserSkills.Remove(userSkills);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<UserSkills> Get(Guid id)
        {
            var userSkills = await _context.UserSkills
                .Include(us => us.User)
                .Include(us => us.Skill)
                .FirstOrDefaultAsync(us => us.UserSkillsId == id);

            if (userSkills != null)
                return userSkills;

            throw new UserSkillsNotFoundException();
        }

        public async Task<IEnumerable<UserSkills>> GetAll()
        {
            var userSkillsList = await _context.UserSkills
                .Include(us => us.User)
                .Include(us => us.Skill)
                .ToListAsync();

            if (userSkillsList.Count == 0)
                throw new UserSkillsNotFoundException();

            return userSkillsList;
        }

        public async Task<UserSkills> Update(UserSkills entity)
        {
            var userSkills = await Get(entity.UserSkillsId);
            _context.UserSkills.Update(entity);
            await _context.SaveChangesAsync();
            return userSkills;
        }
    }
}
