using BL;
using Interfaces;
using Moq;
using System;
using Xunit;

namespace Blog.Services.Tests
{
    public class PostsServiceTests
    {
        [Fact]
        public async void AddPost_AddInvalidPost_ReturnsThrowArgumentException()
        {
            //Arrange
            var mockRepository = new Mock<IPostsRepository>();
            var postsService = new PostsService(mockRepository.Object);
            Guid guid = Guid.NewGuid();
            //Act
            var result = postsService.AddPost(guid, "");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
    }
}
