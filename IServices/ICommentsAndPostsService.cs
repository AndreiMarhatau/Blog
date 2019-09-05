using BL;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ICommentsAndPostsService
    {
        Task<List<PostWithComments>> GetCommentsAndPostsByUserId(int id);
    }
}
