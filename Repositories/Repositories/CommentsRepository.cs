using DomainModels;
using Interfaces;
using Repositories.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private EntityModels.DatabaseContext db;

        public CommentsRepository(EntityModels.DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddComment(Comment comment)
        {
            db.Comments.Add(comment.ToEntityModel());
            await db.SaveChangesAsync();
        }
    }
}
