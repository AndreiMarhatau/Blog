using Data;
using Domain.Core;
using Interfaces;
using IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class TokenService : ITokenService
    {
        ITokenRepository _tokenRepository;
        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task AddToken(string StrToken, int UserId)
        {
            Token token = new Token()
            {
                StrToken = StrToken,
                UserId = UserId
            };

            if (token.IsValidData())
                await _tokenRepository.AddToken(token);
            else
                throw new ArgumentException("Invalid arguments");
        }

        public async Task<int> GetUserIdByToken(string token)
        {
            return await _tokenRepository.GetUserIdByToken(token);
        }

        public async Task RmToken(string StrToken)
        {
            await _tokenRepository.RmToken(new Token() { StrToken = StrToken, UserId = await GetUserIdByToken(StrToken) });
        }
    }
}
