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
            Guid guid = Guid.NewGuid();
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
                PostId = guid,
                UserId = guid
            };
            var likeComment = new Like()
            {
                CommentId = guid,
                UserId = guid
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
            await likeRepo.AddOrRemoveLike(guid, new DomainModels.Post() { Id = guid });
            await likeRepo.AddOrRemoveLike(guid, new DomainModels.Comment() { Id = guid });
        }
        [Fact]
        public async void AddOrRemoveLike_RemoveLike()
        {
            Guid guid = Guid.NewGuid();
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSet = new Mock<DbSet<Like>>();
            IQueryable<Like> data = new List<Like>()
            {
                new Like(){Id=guid,PostId=guid,UserId=guid}
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
                Assert.True(true);
                return null;
            });

            var likeRepo = new LikeRepository(mockDbContext.Object);

            await likeRepo.AddOrRemoveLike(guid, new DomainModels.Post() { Id = guid });
        }
    }
}
