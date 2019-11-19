using System;
using System.Threading.Tasks;

namespace IServices
{
    public interface IPostsService
    {
        Task AddPost(Guid UserId, string Text);
    }
}
