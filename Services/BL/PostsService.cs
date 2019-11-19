using DomainModels;
using Interfaces;
using IServices;
using System;
using System.Threading.Tasks;

namespace BL
{
    public class PostsService : IPostsService
    {
        private IPostsRepository _postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task AddPost(Guid UserId, string Text)
        {
            Post post = new Post()
            {
                Author = new User() { Id = UserId },
                Text = Text
            };

            if (post.IsValidData())
                await _postsRepository.AddPost(post);
            else
                throw new ArgumentException("Invalid arguments");
        }
    }
}
