using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using EntityModels;
using Repositories;
using Microsoft.Data.Sqlite;

namespace Blog.Repositories.Tests
{
    public class LikeRepositoryTests
    {
        [Fact]
        public async void AddOrRemoveLike_AddLike()
        {
            //Arrange
            var mockDbContext = new Mock<EntityModels.DatabaseContext>();
            var mockDbSet = new Mock<DbSet<Like>>();

            mockDbSet
                .Setup(a => a.Add(new Like()))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Likes).Returns(mockDbSet.Object);

            var likeRepo = new LikeRepository(mockDbContext.Object);
            //Act
            await likeRepo.AddOrRemoveLike(1, new DomainModels.Post());
            await likeRepo.AddOrRemoveLike(1, new DomainModels.Comment());
        }
        [Fact]
        public async void AddOrRemoveLike_RemoveLike()
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

                    var likes = new List<Like>()
                    {
                        new Like()
                        {
                            Id = 1,
                            CommentId = 1,
                            UserId = 1
                        }
                    };

                    context.Likes.AddRange(likes);
                    context.SaveChanges();

                    var likeRepo = new LikeRepository(context);
                    await likeRepo.AddOrRemoveLike(1, new DomainModels.Comment() { Id = 1 });

                    var result = await context.Likes.CountAsync();

                    Assert.Equal(0, result);
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
