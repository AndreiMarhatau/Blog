using BL;
using DALInterfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Services.Tests
{
    public class LikeServiceTests
    {
        [Fact]
        public async void AddOrRemoveLike_CheckCallOfRepositoryMethods()
        {
            Guid guid = Guid.NewGuid();
            var mockRepo = new Mock<ILikeRepository>();
            mockRepo.Setup(i => i.AddOrRemoveLike(guid, new DomainModels.Post() { Id = guid }))
                .Returns(() =>
                {
                    Assert.True(true);
                    return null;
                });
            mockRepo.Setup(i => i.AddOrRemoveLike(guid, new DomainModels.Comment() { Id = guid }))
                .Returns(() =>
                {
                    Assert.True(true);
                    return null;
                });

            var service = new LikeService(mockRepo.Object);

            await service.AddOrRemoveLike(guid, new BLModels.Post() { Id = guid });
            await service.AddOrRemoveLike(guid, new BLModels.Comment() { Id = guid });
        }
    }
}
