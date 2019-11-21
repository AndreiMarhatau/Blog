using BL;
using DALInterfaces;
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
            string postText = string.Empty;
            var mockRepository = new Mock<IPostsRepository>();
            var postsService = new PostsService(mockRepository.Object);
            Guid guid = Guid.NewGuid();

            var result = postsService.AddPost(guid, postText);

            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
    }
}
