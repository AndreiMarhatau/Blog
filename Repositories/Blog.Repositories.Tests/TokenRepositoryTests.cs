using EntityModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Blog.Repositories.Tests
{
    public class TokenRepositoryTests
    {
        [Fact]
        public async void GetUserIdByToken_AddTokenWithUserId1AndGetUserId_Returns1()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetOfTokens = new Mock<DbSet<Token>>();
            Guid guid = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            string strToken = "Token";
            IQueryable<Token> data = new List<Token>()
            {
                new Token() { Id = guid, StrToken = strToken, UserId = guid2 },
            }.AsQueryable();

            mockDbSetOfTokens.As<IQueryable<Token>>().Setup(m => m.Provider).Returns(
                new TestAsyncQueryProvider<Token>(data.Provider)
                );
            mockDbSetOfTokens.As<IQueryable<Token>>().Setup(m => m.Expression).Returns(data.Expression);
            mockDbSetOfTokens.As<IQueryable<Token>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockDbSetOfTokens.As<IQueryable<Token>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockDbContext.Setup(a => a.Tokens).Returns(mockDbSetOfTokens.Object);

            var tokenRepo = new TokenRepository(mockDbContext.Object);

            var result = await tokenRepo.GetUserIdByToken(strToken);

            Assert.Equal(guid2, result);
        }
        [Fact]
        public async void AddToken_CheckCallOfAddMethodInDbSet()
        {
            var mockDbContext = new Mock<DatabaseContext>();
            var mockDbSetTokens = new Mock<DbSet<Token>>();

            mockDbSetTokens
                .Setup(a => a.Add(new Token()))
                .Returns(
                () =>
                {
                    Assert.True(true);
                    return null;
                });
            mockDbContext.Setup(a => a.Tokens).Returns(mockDbSetTokens.Object);

            var tokensRepo = new TokenRepository(mockDbContext.Object);

            await tokensRepo.AddToken(new DomainModels.Token());
        }
    }
}
