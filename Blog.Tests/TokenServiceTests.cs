using BL;
using Domain.Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace Blog.Services.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async void AddToken_AddTokenAndGetUserId()
        {
            //Arrange
            var tokenRepo = new Mock<ITokenRepository>();
            tokenRepo.Setup(a => a.AddToken(new Token() { StrToken = "1", UserId = 1 })).Returns(() => throw new Exception());
            TokenService tokenService = new TokenService(tokenRepo.Object);
            //Act
            var result1 = tokenService.AddToken("1", 1);
            //Assert
            await Assert.ThrowsAsync<ArgumentException>(async() => await result1);
        }
    }
}
