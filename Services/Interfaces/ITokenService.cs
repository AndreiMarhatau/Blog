using System.Threading.Tasks;

namespace IServices
{
    public interface ITokenService
    {
        Task<int> GetUserIdByToken(string token);
        Task AddToken(string StrToken, int UserId);
        Task RemoveToken(string StrToken);
    }
}
