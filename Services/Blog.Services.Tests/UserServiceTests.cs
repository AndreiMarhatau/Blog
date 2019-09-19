using BL;
using DomainModels;
using Interfaces;
using System;
using System.Collections.Generic;
using Xunit;
using Moq;

namespace Blog.Services.Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void AddUser_AddUserWithInvalidArguments_ReturnsThrowArgumentException()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            var userService = new UserService(userRepo.Object);
            //Act
            var result = userService.AddUser("","","",DateTime.MinValue,"","");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
        [Fact]
        public async void AddUser_AddTwoUsersWithSameEmailOrLogin_ReturnsThrowArgumentException()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.CheckExistsOfUser("Test", "mail@mail.ru")).ReturnsAsync(true);
            var userService = new UserService(userRepo.Object);
            //Act
            var result = userService.AddUser("Test", "Name", "Surname", DateTime.MinValue, "mail@mail.ru", "Password");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
        [Fact]
        public async void CheckUser_InputInvalidPassword_ReturnsThrowInvalidOperationException()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.GetUserByLogin("Login")).ReturnsAsync(new User() { Login = "Login", Password = "Password" });
            var userService = new UserService(userRepo.Object);
            //Act
            var result = userService.CheckUser("Login", "Pswd2");
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async() => await result);
        }
        [Fact]
        public void SearchUsers_Add3UsersAndGetByLoginNameSurname_Returns3Users()
        {
            //Arrange
            var userRepo = new Mock<IUserRepository>();
            userRepo.Setup(a => a.GetUserListByLoginNameSurname("Login", "Name", "Surname")).ReturnsAsync(new List<User>
            {
                new User{Login = "Login1", Name = "Name1", Surname = "Surname1", Email = "email1@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password1"},
                new User{Login = "Login2", Name = "Name2", Surname = "Surname2", Email = "email2@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password2"},
                new User{Login = "Login3", Name = "Name3", Surname = "Surname3", Email = "email3@mail.ru", BornDate = DateTime.MinValue, RegisterDate = DateTime.MinValue, Password = "Password3"}
            });
            var userService = new UserService(userRepo.Object);
            //Act
            var result = userService.SearchUsers("Login", "Name", "Surname");
            //Assert
            Assert.Equal(3, result.Result.Count);
        }
    }
}
