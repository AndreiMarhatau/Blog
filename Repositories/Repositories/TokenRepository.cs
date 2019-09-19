using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private DatabaseContext db;

        public TokenRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddToken(DomainModels.Token token)
        {
            db.Tokens.Add(token.ToEntityModel());
            await db.SaveChangesAsync();
        }

        public async Task<int> GetUserIdByToken(string token)
        {
            return (await db.Tokens.Where(j => j.StrToken.Equals(token)).SingleAsync()).UserId;
        }

        public async Task RmToken(DomainModels.Token token)
        {
            db.Tokens.Remove(token.ToEntityModel());
            await db.SaveChangesAsync();
        }
    }
}
