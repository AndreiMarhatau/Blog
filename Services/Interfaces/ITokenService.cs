using System;
using System.Threading.Tasks;

namespace IServices
{
    public interface ITokenService
    {
        Task<Guid> GetUserIdByToken(string token);
        Task AddToken(string StrToken, Guid UserId);
        Task RemoveToken(string StrToken);
    }
}
