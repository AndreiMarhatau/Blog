﻿using BL.Helpers;
using BLInterfaces;
using BLModels;
using DALInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL
{
    public class CommentsAndPostsService : ICommentsAndPostsService
    {
        private IPostsRepository _postsRepository;

        public CommentsAndPostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<List<BLModels.Post>> GetCommentsAndPostsByUserId(Guid id)
        {
            var posts = (await _postsRepository.GetPostsByUserId(id)).OrderByDescending(p => p.Date);
            foreach (var post in posts)
                post.Comments = post.Comments.OrderBy(c => c.Date).ToList();
            return posts.ToList().ToBLModel();
        }
    }
}
