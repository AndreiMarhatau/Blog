using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Interfaces;
using Data;
using Domain.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Blog.Repositories.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async void GetUserById_AddUserToQueryAndCheckReturnedUserById()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Name1", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserById(1);

            //Assert
            Assert.Equal(data.Single(), result);
        }
        [Fact]
        public async void GetUserByLogin_AddUserToQueryAndCheckReturnedUserByLogin()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Name1", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserByLogin("Login1");

            //Assert
            Assert.Equal(data.Single(), result);
        }
        [Fact]
        public async void CheckExistsOfUser_AddUserAndCheckExistsByLoginOrEmail()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Name1", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var resultUserWithExistsEmail = await userRepo.CheckExistsOfUser("NonExistsLogin", "mail1@mail.ru");
            var resultUserWithExistsLogin = await userRepo.CheckExistsOfUser("Login1", "nonexistsmail@mail.ru");
            var resultUserWithNonExistsEmailAndLogin = await userRepo.CheckExistsOfUser(
                "NonExistsLogin",
                "nonexistsmail@mail.ru");
            //Assert
            Assert.True(resultUserWithExistsEmail);
            Assert.True(resultUserWithExistsLogin);
            Assert.False(resultUserWithNonExistsEmailAndLogin);
        }
        [Fact]
        public async void GetUserListByLoginNameSurname_AddThreeUsersAndGetTwoOfThemByLoginNameSurname()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Andrey", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 2, BornDate = DateTime.Now, Email = "mail2@mail.ru", Login = "Login2", Name = "Andrei", Surname = "Surname2", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 3, BornDate = DateTime.Now, Email = "mail3@mail.ru", Login = "Login3", Name = "Gennadiy", Surname = "Surname3", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserListByLoginNameSurname("Login", "Andre", "");

            //Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async void AddUser()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetUsers = new Mock<DbSet<User>>();

            mockDbSetUsers
                .Setup(a => a.Add(new User()))
                .Returns(
                () => {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetUsers.Object);

            var usersRepo = new UserRepository(mockDbContext.Object);
            //Act
            await usersRepo.AddUser(new User());
        }
        [Fact]
        public async void GetManyUsersByIds()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Andrey", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 2, BornDate = DateTime.Now, Email = "mail2@mail.ru", Login = "Login2", Name = "Andrei", Surname = "Surname2", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 3, BornDate = DateTime.Now, Email = "mail3@mail.ru", Login = "Login3", Name = "Gennadiy", Surname = "Surname3", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetManyUsersByIds(1,2);

            //Assert
            Assert.Equal(2, result.Count);
        }
    }
}
