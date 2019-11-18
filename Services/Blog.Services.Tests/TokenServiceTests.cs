using BL;
using DomainModels;
using Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Services.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async void AddToken_AddExistsToken_ReturnsThrowArgumentException()
        {
            string strToken = "1";
            int userId = 1;
            var token = new Token() { StrToken = strToken, UserId = userId };

            var tokenRepo = new Mock<ITokenRepository>();
            tokenRepo.Setup(a => a.CheckUserByToken(token)).Returns(Task.Run(() => true));
            TokenService tokenService = new TokenService(tokenRepo.Object);
            //Act
            var result1 = tokenService.AddToken(strToken, userId);
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await result1);
        }
    }
}
