using Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        Task<int> AddUser(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password);
        Task<UserViewModel> GetUserById(int id);
        Task<UserViewModel> GetUserByLogin(string Login);
        Task<int> CheckUser(string Login, string Password);
        Task<List<UserViewModel>> SearchUsers(string Login, string Name, string Surname);
    }
}
