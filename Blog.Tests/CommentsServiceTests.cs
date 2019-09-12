using BL;
using Domain.Core;
using Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Services.Tests
{
    public class CommentsServiceTests
    {
        [Fact]
        public async void AddComment_AddInvalidComment()
        {
            //Arrange
            var mockRepository = new Mock<ICommentsRepository>();
            var commentsService = new CommentsService(mockRepository.Object);
            //Act
            var result = commentsService.AddComment(0, 0, 0, 0, "");
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() => await result);
        }
    }
}
