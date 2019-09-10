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

namespace Blog.Repositories.Tests
{
    public class UserRepositoryTests
    {
        [Fact]
        public async void GetUserById()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfUsers = new Mock<DbSet<User>>();
            IQueryable<User> data = (IQueryable<User>)new List<User>()
            {
                new User() { Id = 1, BornDate = DateTime.Now, Email = "mail1@mail.ru", Login = "Login1", Name = "Name1", Surname = "Surname1", RegisterDate = DateTime.Now, Password = "Password" },
            }.AsQueryable();

            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfUsers.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            //mockDbSetOfUsers.Setup(m => m.SingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<User,bool>>>(), It.IsAny<CancellationToken>())).ReturnsAsync(data.ToList()[0]);

            mockDbContext.Setup(a => a.Users).Returns(mockDbSetOfUsers.Object);
            var userRepo = new UserRepository(mockDbContext.Object);
            //Act
            var result = await userRepo.GetUserById(1);
            //Assert
            Assert.Equal("Login1", result.Login);
        }
    }
}
