using DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DALInterfaces
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByLogin(string Login);
        Task<bool> CheckExistsOfUser(string login, string email);
        Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname);
    }
}
