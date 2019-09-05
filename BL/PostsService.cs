using Domain.Core;
using Interfaces;
using IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class PostsService :IPostsService
    {
        private IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task AddPost(int UserId, string Text)
        {
            Post post = new Post()
            {
                UserId = UserId,
                Text = Text
            };

            if (post.IsValidData())
                await _postsRepository.AddPost(post);
            else
                throw new ArgumentException("Invalid arguments");
        }
    }
}
