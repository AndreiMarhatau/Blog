﻿using EntityModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class UserRepositoryTests
    {
        Guid guid = Guid.NewGuid();
        Guid guid2 = Guid.NewGuid();
        Guid guid3 = Guid.NewGuid();
        [Fact]
        public async void GetUserById_AddUserToQueryWithId1AndCheckReturnedUserIdById_Returns1()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            var result = await userRepo.GetUserById(guid);

            var expectedUserId = guid;
            Assert.Equal(expectedUserId, result.Id);
        }
        [Fact]
        public async void GetUserByLogin_AddUserToQueryAndCheckReturnedUserIdByLogin_Returns1()
        {
            string login = "Login1";
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);
            var result = await userRepo.GetUserByLogin(login);

            var expectedLogin = login;
            Assert.Equal(expectedLogin, result.Login);
        }
        [Fact]
        public async void CheckExistsOfUser_AddUserAndCheckExistsByLoginOrEmail_ReturnsTrueForSameLoginOrEmail()
        {
            string nonExistsLogin = "NonExistsLogin";
            string existsMail = "mail1@mail.ru";
            string existsLogin = "Login1";
            string nonExistsMail = "nonexistsmail@mail.ru";
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);


            var resultUserWithExistsEmail = await userRepo.CheckExistsOfUser(nonExistsLogin, existsMail);
            var resultUserWithExistsLogin = await userRepo.CheckExistsOfUser(existsLogin, nonExistsMail);
            var resultUserWithNonExistsEmailAndLogin = await userRepo.CheckExistsOfUser(
                nonExistsLogin,
                nonExistsMail);

            Assert.True(resultUserWithExistsEmail);
            Assert.True(resultUserWithExistsLogin);
            Assert.False(resultUserWithNonExistsEmailAndLogin);
        }
        [Fact]
        public async void GetUserListByLoginNameSurname_AddThreeUsersAndSearch_ReturnsTwoUsers()
        {
            string login = "Login", name = "Andre", surname = "";
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            var result = await userRepo.GetUserListByLoginNameSurname(login, name, surname);

            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async void AddUser_CheckCallOfAddMethodInDbSet()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetUsers = new Mock<DbSet<User>>();

            mockDbSetUsers
                .Setup(a => a.Add(new User()))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetUsers.Object);

            var usersRepo = new UserRepository(mockDbContext.Object);

            await usersRepo.AddUser(new DomainModels.User());
        }

        private IQueryable<User> SetupMockDbSetUsersForTests(Mock<DbSet<User>> mock)
        {
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = guid, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Andrey", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = guid2, BornDate = DateTime.Now, Email = "mail2@mail.ru", Login = "Login2", Name = "Andrei", Surname = "Surname2", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = guid3, BornDate = DateTime.Now, Email = "mail3@mail.ru", Login = "Login3", Name = "Gennadiy", Surname = "Surname3", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<User>(data.Provider)
                );
            mock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            return data;
        }
    }
}
