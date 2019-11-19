using DomainModels;
using EntityModels;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private DatabaseContext db;
        public LikeRepository(DatabaseContext db)
        {
            this.db = db;
        }
        public async Task AddOrRemoveLike(Guid userId, DomainModels.Post post)
        {
            var like = db.Likes.Where(l => l.PostId == post.Id && l.UserId == userId);
            if (like.Count() != 0)
            {
                db.Likes.Remove(like.Single());
            }
            else
                db.Likes.Add(new EntityModels.Like()
                {
                    PostId = post.Id,
                    UserId = userId
                });
            await db.SaveChangesAsync();
        }

        public async Task AddOrRemoveLike(Guid userId, DomainModels.Comment comment)
        {
            var like = db.Likes.Where(l => l.CommentId == comment.Id && l.UserId == userId);
            if (like.Count() != 0)
            {
                db.Likes.Remove(like.Single());
            }
            else
                db.Likes.Add(new EntityModels.Like()
                {
                    CommentId = comment.Id,
                    UserId = userId
                });
            await db.SaveChangesAsync();
        }
    }
}
