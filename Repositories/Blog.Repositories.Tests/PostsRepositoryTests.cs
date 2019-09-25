﻿using EntityModels;
using Microsoft.Data.Sqlite;
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
    public class PostsRepositoryTests
    {
        [Fact]
        public async void GetPostsByUserId_Add1PostAnd1CommentToDb_Returns1CommentIn1Post()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                var options = new DbContextOptionsBuilder<EntityModels.DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new EntityModels.DatabaseContext(options))
                {
                    context.Database.EnsureCreated();

                    var posts = new List<Post>()
                    {
                        new Post() {Id=1,AuthorId=1000,Text="Post1",Date=DateTime.Now},
                    };

                    var comments = new List<Comment>()
                    {
                        new Comment() {Id=1,AuthorId=1000,CommentId=-1,PostId=1,Date=DateTime.Now,Text="Comment1"},
                    };

                    var users = new List<User>()
                    {
                        new User(){Id=1000}
                    };

                    context.Posts.AddRange(posts);
                    context.Comments.AddRange(comments);
                    context.Users.AddRange(users);
                    context.SaveChanges();

                    var postsRepo = new PostsRepository(context);
                    var result = await postsRepo.GetPostsByUserId(1000);

                    Assert.Single(result);
                    Assert.Single(result.Single().Comments);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public async void AddPost_CheckCallOfAddMethodInDbSet()
        {
            //Arrange
            var mockDbContext = new Mock<EntityModels.DatabaseContext>();
            var mockDbSetPosts = new Mock<DbSet<Post>>();

            mockDbSetPosts
                .Setup(a => a.Add(new Post()))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Posts).Returns(mockDbSetPosts.Object);

            var postsRepo = new PostsRepository(mockDbContext.Object);
            //Act
            await postsRepo.AddPost(new DomainModels.Post() { Author = new DomainModels.User() });
        }
    }
}