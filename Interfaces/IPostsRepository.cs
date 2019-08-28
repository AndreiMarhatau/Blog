using Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPostsByUserId(int id);
        Task AddPost(Post post);
    }
}
