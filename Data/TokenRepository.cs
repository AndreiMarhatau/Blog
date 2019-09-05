using Domain.Core;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class TokenRepository : ITokenRepository
    {
        private DatabaseContext db;

        public TokenRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddToken(Token token)
        {
            db.Tokens.Add(token);
            await db.SaveChangesAsync();
        }

        public async Task<int> GetUserIdByToken(string token)
        {
            return (await db.Tokens.Where(j => j.StrToken.Equals(token)).SingleAsync()).UserId;
        }

        public async Task RmToken(Token token)
        {
            db.Tokens.Remove(token);
            await db.SaveChangesAsync();
        }
    }
}
