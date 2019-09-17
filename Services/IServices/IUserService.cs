using Domain.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        Task<int> AddUser(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password);
        Task<User> GetUserById(int id);
        Task<User> GetUserByLogin(string Login);
        Task<int> CheckUser(string Login, string Password);
        Task<List<User>> SearchUsers(string Login, string Name, string Surname);
    }
}
