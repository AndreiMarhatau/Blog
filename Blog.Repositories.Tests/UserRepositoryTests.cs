using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Interfaces;
using Data;
using Domain.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repositories.Tests
{
    public class UserRepositoryTests
    {
        //[Fact]
        //public async void AddUser_AddUserAndCheckExistsInDb()
        //{
        //    //Arrange
        //    DatabaseContext dbContext = new DatabaseContext(
        //        new Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext>()
        //        );

        //    dbContext.Database

        //    UserRepository userRepository = new UserRepository(dbContext);
        //    //Act
        //    await userRepository.AddUser(new User()
        //    {
        //        Login = "TestLogin",
        //        Email = "testemail@email.com",
        //        BornDate = DateTime.Now,
        //        Name = "TestName",
        //        Surname = "TestSurname",
        //        RegisterDate = DateTime.Now,
        //        Password = "Password"
        //    });

        //    //Assert
        //    Assert.Equal("testemail@email.com", dbContext.Users.Where(i => i.Login == "TestLogin").Single().Email);
        //}


        //private class DatabaseCont:DbContext
        //{
        //    public DbSet<User> Users { get; set; }
        //    public DbSet<Post> Posts { get; set; }
        //    public DbSet<Comment> Comments { get; set; }
        //    public DbSet<Token> Tokens { get; set; }


        //    public DatabaseCont(DbContextOptions<DatabaseContext> options)
        //        : base(options)
        //    {
        //        Database.EnsureCreated();
        //    }

        //    public override 
        //}
    }
}
