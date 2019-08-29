using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsAndPostsService
    {
        Task<List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>> GetCommentsAndPostsByUserId(int id);
    }
}
