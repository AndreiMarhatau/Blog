using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsAndPostsService
    {
        Task<string> GetCommentsAndPostsByUserId(int id);
    }
}
