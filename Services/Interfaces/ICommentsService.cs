using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsService
    {
        Task AddComment(int PostId, int AuthorId, int CommentId, string Text);
    }
}
