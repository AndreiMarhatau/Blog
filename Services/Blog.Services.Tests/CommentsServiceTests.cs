using BL;
using DALInterfaces;
using Moq;
using System;
using Xunit;

namespace Blog.Services.Tests
{
    public class CommentsServiceTests
    {
        [Fact]
        public async void AddComment_AddInvalidComment_ReturnsThrowArgumentException()
        {
            var mockRepository = new Mock<ICommentsRepository>();
            var commentsService = new CommentsService(mockRepository.Object);
            string commentText = string.Empty;
            Guid guid = Guid.NewGuid();

            var result = commentsService.AddComment(guid, guid, guid, commentText);

            await Assert.ThrowsAsync<ArgumentException>(async () => await result);
        }
    }
}
