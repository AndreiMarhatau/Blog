﻿using DomainModels;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITokenRepository
    {
        Task<int> GetUserIdByToken(string token);
        Task AddToken(Token token);
        Task RemoveToken(Token token);
        Task<bool> CheckUserByToken(Token token);
    }
}
