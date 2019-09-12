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
    public class CommentsRepository : ICommentsRepository
    {
        private DatabaseContext db;

        public CommentsRepository(DatabaseContext db)
        {
            this.db = db;
        }

        public async Task AddComment(Comment comment)
        {
            db.Comments.Add(comment);
            await db.SaveChangesAsync();
        }
    }
}
