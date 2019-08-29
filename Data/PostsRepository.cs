﻿using Domain.Core;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class PostsRepository:IPostsRepository
    {
        DatabaseContext db;
        public PostsRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddComment(Comment comment)
        {
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
        }

        public async Task AddPost(Post post)
        {
            db.Posts.Add(post);
            await db.SaveChangesAsync();
        }

        public async Task<List<Post>> GetPostsByUserId(int id)
        {
            var a = await db.Posts
                .Where(i => i.UserId == id)
                .Include(i => i.Comments)
                .ToListAsync();
            return a;
        }
    }
}
