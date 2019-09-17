using Domain.Core;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ITokenRepository
    {
        Task<int> GetUserIdByToken(string token);
        Task AddToken(Token token);
        Task RmToken(Token token);
    }
}
