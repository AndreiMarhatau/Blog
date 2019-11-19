using BLModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        Task<Guid> AddUser(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByLogin(string Login);
        Task<Guid> CheckUser(string Login, string Password);
        Task<List<User>> SearchUsers(string Login, string Name, string Surname);
    }
}
