using BL;
using DomainModels;
using Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Blog.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void AddUser_AddUserWithInvalidArguments_ReturnsThrowArgumentException()
        {
            var userRepo = new Mock<IUserRepository>();
            var userService = new UserService(userRepo.Object);

            var result = userService.AddUser(string.Empty, string.Empty, string.Empty, DateTime.MinValue, string.Empty, string.Empty);

            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
        [Fact]
        public async void AddUser_AddTwoUsersWithSameEmailOrLogin_ReturnsThrowArgumentException()
        {
            string login = "Test", name = "Name", surname = "surname", mail = "mail@mail.ru", password = "Password";
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.CheckExistsOfUser(login, mail)).ReturnsAsync(true);
            var userService = new UserService(userRepo.Object);

            var result = userService.AddUser(login, name, surname, DateTime.MinValue, mail, password);

            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
        [Fact]
        public async void CheckUser_InputInvalidPassword_ReturnsThrowInvalidOperationException()
        {
            string login = "Login", correctPassword = "Password", incorrectPassword = "Pswd2";
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.GetUserByLogin(login)).ReturnsAsync(new User() { Login = login, Password = correctPassword });
            var userService = new UserService(userRepo.Object);

            var result = userService.CheckUser(login, incorrectPassword);

            await Assert.ThrowsAsync<InvalidOperationException>(async () => await result);
        }
        [Fact]
        public void SearchUsers_Add3UsersAndGetByLoginNameSurname_Returns3Users()
        {
            var user1 = new User { Login = "Login1", Name = "Name1", Surname = "Surname1", Email = "email1@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password1" };
            var user2 = new User { Login = "Login2", Name = "Name2", Surname = "Surname2", Email = "email2@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password2" };
            var user3 = new User { Login = "Login3", Name = "Name3", Surname = "Surname3", Email = "email3@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password3" };
            string login = "Login", name = "Name", surname = "Surname";

            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.GetUserListByLoginNameSurname(login, name, surname)).ReturnsAsync(new List<User>
            {
                user1, user2, user3
            });
            var userService = new UserService(userRepo.Object);

            var result = userService.SearchUsers(login, name, surname);

            Assert.Equal(3, result.Result.Count);
        }
    }
}
