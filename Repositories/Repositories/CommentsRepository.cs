using DomainModels;
using Interfaces;
using System.Threading.Tasks;

namespace Repositories
{
    public class CommentsRepository : ICommentsRepository
    {
        private DatabaseContext db;

        public CommentsRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddComment(DomainModels.Comment comment)
        {
            db.Comments.Add(comment.ToEntityModel());
            await db.SaveChangesAsync();
        }
    }
}
