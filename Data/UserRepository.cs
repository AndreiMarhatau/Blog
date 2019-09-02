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
        
        public async Task<User> GetUserById(int id)
        {
            return await db.Users.Where(i => i.Id == id).SingleAsync();
        }
        public async Task<User> GetUserByLogin(string Login)
        {
            return await db.Users.Where(i => i.Login == Login).SingleAsync();
        }
        public async Task<bool> CheckExistsOfUser(string login, string email)
        {
            return (await db.Users.Where(i => i.Login == login ||
                                           i.Email == email)
                                          .ToListAsync())
                                          .Count > 0;
        }

        public async Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname)
        {
            return await db.Users.Where(i => i.Login.ToLower() == Login.ToLower() &&
                                       i.Name.ToLower() == Name.ToLower() &&
                                       i.Surname.ToLower() == Surname.ToLower()).ToListAsync();
        }
    }
}
