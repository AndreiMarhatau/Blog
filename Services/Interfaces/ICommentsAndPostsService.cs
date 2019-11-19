using BLModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsAndPostsService
    {
        Task<List<Post>> GetCommentsAndPostsByUserId(Guid id);
    }
}
