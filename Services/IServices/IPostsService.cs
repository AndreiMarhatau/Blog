using System.Threading.Tasks;

namespace IServices
{
    public interface IPostsService
    {
        Task AddPost(int UserId, string Text);
    }
}
