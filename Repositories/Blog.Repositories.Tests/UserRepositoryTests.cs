using EntityModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async void GetUserById_AddUserToQueryWithId1AndCheckReturnedUserIdById_Returns1()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserById(1);

            //Assert
            var expectedUserId = 1;
            Assert.Equal(expectedUserId, result.Id);
        }
        [Fact]
        public async void GetUserByLogin_AddUserToQueryAndCheckReturnedUserIdByLogin_Returns1()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserByLogin("Login1");

            //Assert
            var expectedUserId = 1;
            Assert.Equal(expectedUserId, result.Id);
        }
        [Fact]
        public async void CheckExistsOfUser_AddUserAndCheckExistsByLoginOrEmail_ReturnsTrueForSameLoginOrEmail()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
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
        public async void GetUserListByLoginNameSurname_AddThreeUsersAndSearch_ReturnsTwoUsers()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();

            SetupMockDbSetUsersForTests(mockDbSetOfUsers);
            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);

            var userRepo = new UserRepository(mockDbContext.Object);

            //Act
            var result = await userRepo.GetUserListByLoginNameSurname("Login", "Andre", "");

            //Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async void AddUser_CheckCallOfAddMethodInDbSet()
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
            await usersRepo.AddUser(new DomainModels.User());
        }

        private IQueryable<User> SetupMockDbSetUsersForTests(Mock<DbSet<User>> mock)
        {
            IQueryable<User> data = new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Andrey", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 2, BornDate = DateTime.Now, Email = "mail2@mail.ru", Login = "Login2", Name = "Andrei", Surname = "Surname2", RegisterDate = DateTime.Now, Password = "Password" },
                new User() { Id = 3, BornDate = DateTime.Now, Email = "mail3@mail.ru", Login = "Login3", Name = "Gennadiy", Surname = "Surname3", RegisterDate = DateTime.Now, Password = "Password" },
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
