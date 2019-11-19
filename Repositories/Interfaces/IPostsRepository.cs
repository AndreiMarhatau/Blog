using DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IPostsRepository
    {
        Task<List<Post>> GetPostsByUserId(Guid id);
        Task AddPost(Post post);
    }
}
