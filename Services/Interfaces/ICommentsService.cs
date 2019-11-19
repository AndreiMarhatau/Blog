using System;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsService
    {
        Task AddComment(Guid PostId, Guid AuthorId, Guid CommentId, string Text);
    }
}
