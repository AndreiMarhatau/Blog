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
        Task<List<User>> GetUserList();
        Task<User> GetUserById(int id);
        Task<User> GetUserByLogin(string Login);
    }
}
