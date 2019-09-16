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
        private DatabaseContext db;

        public UserRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }
        
        public async Task<User> GetUserById(int id)
        {
            return await db.Users.Where(i => i.Id == id).SingleAsync();
        }
        public async Task<User> GetUserByLogin(string Login)
        {
            return await db.Users.Where(i => i.Login.Equals(Login)).SingleAsync();
        }
        public async Task<bool> CheckExistsOfUser(string login, string email)
        {
            return (await db.Users.Where(i => i.Login.Equals(login) ||
                                           i.Email.Equals(email))
                                          .ToListAsync())
                                          .Any();
        }

        public async Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname)
        {
            return await db.Users.Where(i => i.Login.Contains(Login, StringComparison.OrdinalIgnoreCase) &&
                                       i.Name.Contains(Name, StringComparison.OrdinalIgnoreCase) &&
                                       i.Surname.Contains(Surname, StringComparison.OrdinalIgnoreCase)).ToListAsync();
        }
        
        public async Task<List<User>> GetManyUsersByIds(params int[] ids)
        {
            return await db.Users.Where(i => ids.Contains(i.Id)).ToListAsync();
        }
    }
}
