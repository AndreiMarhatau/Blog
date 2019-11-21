using Interfaces;
using Microsoft.EntityFrameworkCore;
using Repositories.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class PostsRepository : IPostsRepository
    {
        private EntityModels.DatabaseContext db;

        public PostsRepository(EntityModels.DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddPost(DomainModels.Post post)
        {
            db.Posts.Add(post.ToEntityModel());
            await db.SaveChangesAsync();
        }

        public async Task<List<DomainModels.Post>> GetPostsByUserId(Guid id)
        {
            return
                (await db.Posts
                .Where(p => p.AuthorId == id)
                .OrderByDescending(p => p.Id)
                .Include(i => i.Comments)
                    .ThenInclude(c => c.Author)
                .Include(i => i.Comments)
                    .ThenInclude(c => c.Likes)
                .Include(i => i.Author)
                .Include(i => i.Likes)
                .ToListAsync()).Select(i => i.ToDomainModel()).ToList();
        }
    }
}
