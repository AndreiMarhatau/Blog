
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface IUserService
    {
        Task<int> AddUser(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password);
        Task<Dictionary<string, string>> GetUserById(int id);
        Task<Dictionary<string, string>> GetUserByLogin(string Login);
        Task<int> CheckUser(string Login, string Password);
        Task<List<Dictionary<string, string>>> SearchUsers(string Login, string Name, string Surname);
    }
}
