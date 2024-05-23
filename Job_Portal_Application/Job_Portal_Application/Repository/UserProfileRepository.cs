using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Context;
using Job_Portal_Application.Exceptions;
using Job_Portal_Application.Interfaces;
using Job_Portal_Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_Application.Repository
{
    public class UserProfileRepository : UserRepository
    {

        private readonly JobportalContext _context;
        public UserProfileRepository(JobportalContext context) : base(context)
        {
        }
        public async Task<User> Get(Guid id)
        {
            var user= await _context.Users
                .Include(u => u.Educations)
                .Include(u => u.Experiences)
                .Include(u => u.UserSkills).ThenInclude(js => js.Skill)
     
                .Include(u => u.AreasOfInterests)
                .FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
                return user;

            throw new UserNotFoundException();
        }


        public async Task<IEnumerable<User>> GetAll()
        {
            var users= await _context.Users
                .Include(u => u.Educations)
                .Include(u => u.Experiences)
                .Include(u => u.UserSkills).ThenInclude(js => js.Skill)
   
                .Include(u => u.AreasOfInterests)
                .ToListAsync();
            if (users.Count == 0)
                throw new UserNotFoundException();

            return users;
        }
    }
}
