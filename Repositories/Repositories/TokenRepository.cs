using Repositories.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using DALInterfaces;

namespace Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private EntityModels.DatabaseContext db;

        public TokenRepository(EntityModels.DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddToken(DomainModels.Token token)
        {
            db.Tokens.Add(token.ToEntityModel());
            await db.SaveChangesAsync();
        }

        public async Task<bool> CheckUserByToken(DomainModels.Token token)
        {
            var entityToken = token.ToEntityModel();
            return db.Tokens.Where(t => t.StrToken == entityToken.StrToken && t.UserId == entityToken.UserId).Count() > 0;
        }

        public async Task<Guid> GetUserIdByToken(string token)
        {
            return (await db.Tokens.Where(j => j.StrToken.Equals(token)).SingleAsync()).UserId;
        }

        public async Task RemoveToken(DomainModels.Token token)
        {
            db.Tokens.Remove(token.ToEntityModel());
            await db.SaveChangesAsync();
        }
    }
}
