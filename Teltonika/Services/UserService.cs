using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teltonika.DataModels;
using Teltonika.DBContext;
using Teltonika.Interfaces;

namespace Teltonika.Services
{
    public class UserService : IUserService
    {
        readonly Context _context;

        public UserService(Context context)
        {
            _context = context;
        }

        public Task<string> Delete(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Get(int id)
        {
            return await _context.Users
                .Where(r => r.Id == id)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetByUsernamePassword(User user)
        {
            return await _context.Users
                .Where(r => r.Username == user.Username && r.Username == user.Password)
                .SingleOrDefaultAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users
                .Where(r => r.Username == username).FirstOrDefaultAsync();
        }

        public async Task Save(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
