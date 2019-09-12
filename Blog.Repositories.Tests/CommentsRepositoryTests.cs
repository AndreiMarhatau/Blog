using Data;
using Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class CommentsRepositoryTests
    {
        [Fact]
        public async void AddToken()
        {
            //Arrange
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetComments = new Mock<DbSet<Comment>>();

            mockDbSetComments
                .Setup(a => a.Add(new Comment()))
                .Returns(
                () => {
                    Assert.True(true);
                    return (EntityEntry<Comment>)null;
                });
            mockDbContext.Setup(a => a.Comments).Returns(mockDbSetComments.Object);

            var commentsRepo = new CommentsRepository(mockDbContext.Object);
            //Act
            await commentsRepo.AddComment(new Comment());
        }
    }
}
