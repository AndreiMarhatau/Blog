using BLModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ILikeService
    {
        Task AddOrRemoveLike(int userId, Post post);
        Task AddOrRemoveLike(int userId, Comment comment);
    }
}
