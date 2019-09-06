using BL;
using Domain.Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void AddUser_AddUserAndCheckExists()
        {
            //Arrange
            var userService = new UserService(new RepositoriesForTests());
            //Act
            await userService.AddUser("Test", "Name", "Surname", DateTime.Now, "Email@email.com", "Password");
            var actual = (await userService.GetUserByLogin("Test"))["Login"];
            //Assert
            Assert.Equal("Test", actual);
        }
        private class RepositoriesForTests : IUserRepository
        {
            private List<User> users = new List<User>();
            
            public async Task AddUser(User user)
            {
                users.Add(user);
            }

            public async Task<bool> CheckExistsOfUser(string login, string email)
            {
                return users.Where(i => i.Login.Equals(login) ||
                                               i.Email.Equals(email))
                                              .Any();
            }
            
            public async Task<User> GetUserById(int id)
            {
                return users.Where(i => i.Id == id).Single();
            }

            public async Task<User> GetUserByLogin(string Login)
            {
                return users.Where(i => i.Login.Equals(Login)).Single();
            }
            
            public async Task<List<User>> GetUserListByLoginNameSurname(string Login, string Name, string Surname)
            {
                throw new NotImplementedException();
            }
        }
    }
}
