using Data;
using Domain.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class PostsRepositoryTests
    {
        [Fact]
        public async void GetPostsByUserId_AddPostWithCommentsAndGetThem()
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

                    var posts = new List<Post>()
                    {
                        new Post() {Id=1,UserId=1000,Text="Post1",Date=DateTime.Now},
                    };

                    var comments = new List<Comment>()
                    {
                        new Comment() {Id=1,AuthorId=1000,CommentId=-1,PostId=1,UserId=1000,Date=DateTime.Now,Text="Comment1"},
                    };
                    context.Posts.AddRange(posts);
                    context.Comments.AddRange(comments);
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
        public async void AddPost()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetPosts = new Mock<DbSet<Post>>();

            mockDbSetPosts
                .Setup(a => a.Add(new Post()))
                .Returns(
                () => {
                    Assert.True(true);
                    return (EntityEntry<Post>)null;
                });
            mockDbContext.Setup(a => a.Posts).Returns(mockDbSetPosts.Object);

            var postsRepo = new PostsRepository(mockDbContext.Object);
            //Act
            await postsRepo.AddPost(new Post());
        }
    }
}
