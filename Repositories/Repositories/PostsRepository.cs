﻿using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories
{
    public class PostsRepository:IPostsRepository
    {
        private DatabaseContext db;

        public PostsRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddPost(DomainModels.Post post)
        {
            db.Posts.Add(post.ToEntityModel());
            await db.SaveChangesAsync();
        }

        public async Task<List<DomainModels.Post>> GetPostsByUserId(int id)
        {
            return
                (await db.Posts
                .Where(p => p.AuthorId == id)
                .OrderByDescending(p => p.Id)
                .Include(i => i.Comments)
                    .ThenInclude(c => c.Author)
                .Include(i => i.Author)
                .ToListAsync()).Select(i => i.ToDomainModel()).ToList();
        }
    }
}
