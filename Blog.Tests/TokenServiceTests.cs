using BL;
using Domain.Core;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Tests
{
    public class TokenServiceTests
    {
        [Fact]
        public async void AddToken_AddTokenAndGetUserId()
        {
            //Arrange
            TokenService tokenService = new TokenService(new RepositoriesForTests());
            //Act
            await tokenService.AddToken("token1", 1);
            var userId = await tokenService.GetUserIdByToken("token1");
            //Assert
            Assert.True(userId == 1);
        }
        [Fact]
        public async void RmToken_AddTokenAndRemove()
        {
            //Arrange
            TokenService tokenService = new TokenService(new RepositoriesForTests());
            //Act
            await tokenService.AddToken("token1", 1);
            await tokenService.RmToken("token1");
            //Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async() => await tokenService.GetUserIdByToken("token1"));
        }

        private class RepositoriesForTests : ITokenRepository
        {
            private List<Token> tokens = new List<Token>();
            
            public async Task AddToken(Token token)
            {
                tokens.Add(token);
            }
            public async Task<int> GetUserIdByToken(string token)
            {
                return tokens.Where(i => i.StrToken.Equals(token)).Single().UserId;
            }
            public async Task RmToken(Token token)
            {
                tokens.Remove(token);
            }
        }
    }
}
