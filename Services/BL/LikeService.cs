using BLModels;
using Interfaces;
using IServices;
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

        public async Task AddOrRemoveLike(int userId, Post post)
        {
            await likeRepository.AddOrRemoveLike(userId, post.ToDomainModel());
        }

        public async Task AddOrRemoveLike(int userId, Comment comment)
        {
            await likeRepository.AddOrRemoveLike(userId, comment.ToDomainModel());
        }
    }
}
