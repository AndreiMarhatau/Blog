using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Domain.Core;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        DatabaseContext db;
        public UserRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task<List<User>> GetUserList()
        {
            return await db.Users.ToListAsync();
        }
        public async Task<User> GetUserById(int id)
        {
            return await db.Users.Where(i => i.Id == id).SingleAsync();
        }
        public async Task<User> GetUserByLogin(string Login)
        {
            return await db.Users.Where(i => i.Login == Login).SingleAsync();
        }
    }
}
