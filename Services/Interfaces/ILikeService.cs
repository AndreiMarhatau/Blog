using BLModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IServices
{
    public interface ILikeService
    {
        Task AddOrRemoveLike(Guid userId, Post post);
        Task AddOrRemoveLike(Guid userId, Comment comment);
    }
}
