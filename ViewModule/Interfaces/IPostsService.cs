using System;
using System.Threading.Tasks;

namespace BLInterfaces
{
    public interface IPostsService
    {
        Task AddPost(Guid UserId, string Text);
    }
}
