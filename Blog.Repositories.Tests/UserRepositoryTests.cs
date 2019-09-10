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
            var result = await userRepo.GetUserListByLoginNameSurname("login", "andre", "");

            //Assert
            Assert.Equal(2, result.Count);
        }
    }
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            return new TestAsyncEnumerable<TResult>(expression);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public T Current
        {
            get
            {
                return _inner.Current;
            }
        }

        public Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }
    }
}
