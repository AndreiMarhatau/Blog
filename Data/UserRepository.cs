using System;
using System.Collections.Generic;
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
    }
}
