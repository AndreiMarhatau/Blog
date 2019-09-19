using BLModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsAndPostsService
    {
        Task<List<Post>> GetCommentsAndPostsByUserId(int id);
    }
}
