
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsService
    {
        Task AddComment(int PostId, int UserId, int AuthorId, int CommentId, string Text);
    }
}
