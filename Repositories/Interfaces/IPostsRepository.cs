using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels;

namespace Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPostsByUserId(int id);
        Task AddPost(Post post);
    }
}
