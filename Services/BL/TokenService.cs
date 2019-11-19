using DomainModels;
using Interfaces;
using IServices;
using System;
using System.Threading.Tasks;

namespace BL
{
    public class TokenService : ITokenService
    {
        private ITokenRepository _tokenRepository;

        public TokenService(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        public async Task AddToken(string strToken, Guid userId)
        {
            Token token = new Token()
            {
                StrToken = strToken,
                UserId = userId
            };

            if(!token.IsValidData())
                throw new ArgumentException("Invalid arguments");

            bool isExistsInDb = await _tokenRepository.CheckUserByToken(token);


            if (!isExistsInDb)
                await _tokenRepository.AddToken(token);
            else
                throw new InvalidOperationException("Token exists already");
        }

        public async Task<Guid> GetUserIdByToken(string token)
        {
            return await _tokenRepository.GetUserIdByToken(token);
        }

        public async Task RemoveToken(string StrToken)
        {
            await _tokenRepository.RemoveToken(new Token() { StrToken = StrToken, UserId = await GetUserIdByToken(StrToken) });
        }
    }
}
