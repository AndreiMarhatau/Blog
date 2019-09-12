using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserById(int id);
        Task<User> GetUserByLogin(string Login);
        Task<bool> CheckExistsOfUser(string login, string email);
        Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname);
    }
}
