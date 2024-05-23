using Job_Portal_Application.Context;
using Job_Portal_Application.Interfaces;
using Job_Portal_Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Job_Portal_Application.Models;

namespace Job_Portal_Application.Repository
{
    public class UserRepository : IRepository<Guid, User>
    {
       private readonly JobportalContext _context;

        public UserRepository(JobportalContext context)
        {
            _context = context;
        } 

        public async Task<User> Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var user = await Get(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> Get(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null)
                return user;

            throw new UserNotFoundException();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            if (users.Count == 0)
                throw new UserNotFoundException();

            return users;
        }

        public async Task<User> Update(User entity)
        {
            var user = await Get(entity.UserId);
            _context.Users.Update(entity);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
