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
            var actual = (await userService.GetUserByLogin("Test")).Login;
            //Assert
            Assert.Equal("Test", actual);
        }
        [Fact]
        public async void AddUser_AddTwoUsersWithSameEmailOrLogin()
        {
            //Arrange
            var userService = new UserService(new RepositoriesForTests());
            //Act
            await userService.AddUser("Test", "Name", "Surname", DateTime.Now, "Email@email.com", "Password");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddUser("Test2", "Name2", "Surname2", DateTime.Now, "Email@email.com", "Password2"));
            await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddUser("Test", "Name3", "Surname3", DateTime.Now, "Email3@email.com", "Password3"));
        }
        [Fact]
        public async void CheckUser_CheckExistsUserByLoginPassword()
        {
            //Arrange
            var userService = new UserService(new RepositoriesForTests());
            await userService.AddUser("Test", "Name", "Surname", DateTime.Now, "Email@email.com", "Password");
            //Act
            var id = await userService.CheckUser("Test", "Password");
            //Assert
            Assert.Equal(0, id);
        }
        [Fact]
        public async void SearchUsers_GetUserListByLoginNameSurname()
        {
            //Arrange
            var userService = new UserService(new RepositoriesForTests());
            await userService.AddUser("Test", "Name", "Surname", DateTime.Now, "Email@email.com", "Password");
            await userService.AddUser("Test2", "Name2", "Surname2", DateTime.Now, "Email2@email.com", "Password");
            await userService.AddUser("Tset", "Name3", "Surname3", DateTime.Now, "Email3@email.com", "Password");
            //Act
            var count = (await userService.SearchUsers("Test", "Name", "Surname")).Count;
            //Assert
            Assert.Equal(2, count);
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
                return users.Where(i => i.Login.Contains(Login, StringComparison.OrdinalIgnoreCase) &&
                                       i.Name.Contains(Name, StringComparison.OrdinalIgnoreCase) &&
                                       i.Surname.Contains(Surname, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
    }
}
