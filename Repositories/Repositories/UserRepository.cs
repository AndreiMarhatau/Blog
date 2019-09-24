using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private DatabaseContext db;

        public UserRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddUser(DomainModels.User user)
        {
            db.Users.Add(user.ToEntityModel());
            await db.SaveChangesAsync();
        }
        
        public async Task<DomainModels.User> GetUserById(int id)
        {
            return (await db.Users.Where(i => i.Id == id).SingleAsync()).ToDomainModel();
        }
        public async Task<DomainModels.User> GetUserByLogin(string Login)
        {
            return (await db.Users.Where(i => i.Login.Equals(Login)).SingleAsync()).ToDomainModel();
        }
        public async Task<bool> CheckExistsOfUser(string login, string email)
        {
            return await db.Users.Where(i => i.Login.Equals(login) ||
                                           i.Email.Equals(email))
                                          .AnyAsync();
        }

        public async Task<List<DomainModels.User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname)
        {
            return (await db.Users
                .Where(i => i.Login.Contains(Login) &&
                            i.Name.Contains(Name) &&
                            i.Surname.Contains(Surname))
                .ToListAsync())
                .Select(i => i.ToDomainModel())
                .ToList();
        }
    }
}
