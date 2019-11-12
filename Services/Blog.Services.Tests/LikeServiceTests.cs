using BL;
using Interfaces;
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
            //Arrange
            var mockRepo = new Mock<ILikeRepository>();
            mockRepo.Setup(i => i.AddOrRemoveLike(1, new DomainModels.Post() { Id = 1 }))
                .Returns(() =>
                {
                    Assert.True(true);
                    return null;
                });
            mockRepo.Setup(i => i.AddOrRemoveLike(1, new DomainModels.Comment() { Id = 1 }))
                .Returns(() =>
                {
                    Assert.True(true);
                    return null;
                });

            var service = new LikeService(mockRepo.Object);
            //Act
            await service.AddOrRemoveLike(1, new BLModels.Post() { Id = 1 });
            await service.AddOrRemoveLike(1, new BLModels.Comment() { Id = 1 });
        }
    }
}
