using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICommentsRepository
    {
        Task<List<Comment>> GetCommentsByUserId(int id);
        Task AddComment(Comment post);
    }
}
