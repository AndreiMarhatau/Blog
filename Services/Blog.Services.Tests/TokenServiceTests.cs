using BL;
using DomainModels;
using Interfaces;
using System;
using Xunit;
using Moq;

namespace Blog.Services.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async void AddToken_AddExistsToken_ReturnsThrowArgumentException()
        {
            //Arrange
            var tokenRepo = new Mock<ITokenRepository>();
            tokenRepo.Setup(a => a.AddToken(new Token() { StrToken = "1", UserId = 1 })).Returns(() => throw new ArgumentException());
            TokenService tokenService = new TokenService(tokenRepo.Object);
            //Act
            var result1 = tokenService.AddToken("1", 1);
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() => await result1);
        }
    }
}
