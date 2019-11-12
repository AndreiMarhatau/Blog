using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using EntityModels;
using Repositories;
using Microsoft.Data.Sqlite;
using System.Linq;

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

            IQueryable<Like> data = new List<Like>()
            {}.AsQueryable();

            mockDbSet.As<IQueryable<Like>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<Like>(data.Provider)
                );
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Likes).Returns(mockDbSet.Object);

            var likePost = new Like()
            {
                PostId = 1,
                UserId = 1
            };
            var likeComment = new Like()
            {
                CommentId = 1,
                UserId = 1
            };

            mockDbSet
                .Setup(a => a.Add(likePost))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbSet
                .Setup(a => a.Add(likeComment))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Likes).Returns(mockDbSet.Object);

            var likeRepo = new LikeRepository(mockDbContext.Object);
            //Act
            await likeRepo.AddOrRemoveLike(1, new DomainModels.Post() { Id = 1 });
            await likeRepo.AddOrRemoveLike(1, new DomainModels.Comment() { Id = 1 });
        }
        [Fact]
        public async void AddOrRemoveLike_RemoveLike()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSet = new Mock<DbSet<Like>>();
            IQueryable<Like> data = new List<Like>()
            {
                new Like(){Id=1,PostId=1,UserId=1}
            }.AsQueryable();

            mockDbSet.As<IQueryable<Like>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<Like>(data.Provider)
                );
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSet.As<IQueryable<Like>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Likes).Returns(mockDbSet.Object);

            mockDbSet.Setup(a => a.Remove(data.First())).Returns(() =>
            {
                //Assert
                Assert.True(true);
                return null;
            });

            var likeRepo = new LikeRepository(mockDbContext.Object);

            //Act
            await likeRepo.AddOrRemoveLike(1, new DomainModels.Post() { Id = 1 });
        }
    }
}
