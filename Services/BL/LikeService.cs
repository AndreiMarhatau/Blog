using BL.Helpers;
using BLInterfaces;
using BLModels;
using DALInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class LikeService : ILikeService
    {
        private ILikeRepository likeRepository;
        public LikeService(ILikeRepository likeRepository)
        {
            this.likeRepository = likeRepository;
        }

        public async Task AddOrRemoveLike(Guid userId, Post post)
        {
            await likeRepository.AddOrRemoveLike(userId, post.ToDomainModel());
        }

        public async Task AddOrRemoveLike(Guid userId, Comment comment)
        {
            await likeRepository.AddOrRemoveLike(userId, comment.ToDomainModel());
        }
    }
}
