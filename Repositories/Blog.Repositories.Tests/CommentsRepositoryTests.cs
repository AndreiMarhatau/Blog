using EntityModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class CommentsRepositoryTests
    {
        [Fact]
        public async void AddToken_CheckCallOfAddMethodInDbSet()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetComments = new Mock<DbSet<Comment>>();

            mockDbSetComments
                .Setup(a => a.Add(new Comment()))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Comments).Returns(mockDbSetComments.Object);

            var commentsRepo = new CommentsRepository(mockDbContext.Object);

            await commentsRepo.AddComment(new DomainModels.Comment() { Author = new DomainModels.User() });
        }
    }
}
