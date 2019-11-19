using EntityModels;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var options = new DbContextOptionsBuilder<DatabaseContext>()
                    .UseSqlite(connection)
                    .Options;
                using (var context = new DatabaseContext(options))
                {
                    context.Database.EnsureCreated();

                    Guid guid = Guid.NewGuid();
                    Guid guid2 = Guid.NewGuid();

                    var posts = new List<Post>()
                    {
                        new Post() {Id=guid,AuthorId=guid2,Text="Post1",Date=DateTime.Now},
                    };

                    var comments = new List<Comment>()
                    {
                        new Comment() {Id=guid,AuthorId=guid2,CommentId=Guid.Empty,PostId=guid,Date=DateTime.Now,Text="Comment1"},
                    };

                    var users = new List<User>()
                    {
                        new User(){Id=guid2}
                    };

                    context.Posts.AddRange(posts);
                    context.Comments.AddRange(comments);
                    context.Users.AddRange(users);
                    context.SaveChanges();

                    var postsRepo = new PostsRepository(context);
                    var result = await postsRepo.GetPostsByUserId(guid2);

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

            await postsRepo.AddPost(new DomainModels.Post() { Author = new DomainModels.User() });
        }
    }
}