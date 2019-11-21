using System;
using System.Threading.Tasks;

namespace BLInterfaces
{
    public interface ICommentsService
    {
        Task AddComment(Guid PostId, Guid AuthorId, Guid CommentId, string Text);
    }
}
