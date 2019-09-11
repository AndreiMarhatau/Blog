using Data;
using Domain.Core;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Moq;
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


            ////Arrange
            //var mockDbContext = new Mock<DatabaseContext>();
            //var mockDbSetOfPosts = new Mock<DbSet<Post>>();
            //IQueryable<Post> data = new List<Post>()
            //{
            //    new Post() {Id=1,UserId=1000,Text="Post1",Date=DateTime.Now},
            //}.AsQueryable();

            //mockDbSetOfPosts.As<IQueryable<Post>>().Setup(m => m.Provider).Returns(
            //    new TestAsyncQueryProvider<Post>(data.Provider)
            //    );
            //mockDbSetOfPosts.As<IQueryable<Post>>().Setup(m => m.Expression).Returns(data.Expression);
            //mockDbSetOfPosts.As<IQueryable<Post>>().Setup(m => m.ElementType).Returns(data.ElementType);
            //mockDbSetOfPosts.As<IQueryable<Post>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            //var mockDbSetOfComments = new Mock<DbSet<Comment>>();
            //IQueryable<Comment> dataComments = new List<Comment>()
            //{
            //    new Comment() {Id=1,AuthorId=1000,CommentId=-1,PostId=1,UserId=1000,Date=DateTime.Now,Text="Comment1"},
            //}.AsQueryable();

            //mockDbSetOfComments.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(
            //    new TestAsyncQueryProvider<Comment>(dataComments.Provider)
            //    );
            //mockDbSetOfComments.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(dataComments.Expression);
            //mockDbSetOfComments.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(dataComments.ElementType);
            //mockDbSetOfComments.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(dataComments.GetEnumerator());

            //mockDbContext.Setup(a => a.Posts).Returns(mockDbSetOfPosts.Object);
            //mockDbContext.Setup(a => a.Comments).Returns(mockDbSetOfComments.Object);

            //var postRepo = new PostsRepository(mockDbContext.Object);

            ////Act
            //var result = await postRepo.GetPostsByUserId(1000);
            //Dont work include comment to post

            ////Assert
            //Assert.Single(result);
            //Assert.Single(result.Single().Comments);
        }
    }
}
