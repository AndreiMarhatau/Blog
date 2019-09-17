using Domain.Core;
using Interfaces;
using IServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL
{
    public class CommentsAndPostsService : ICommentsAndPostsService
    {
        private IPostsRepository _postsRepository;
        private IUserRepository _userRepository;

        public CommentsAndPostsService(IPostsRepository postsRepository, IUserRepository userRepository)
        {
            _postsRepository = postsRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Post>>
            GetCommentsAndPostsByUserId(int id)
        {
            var posts = (await _postsRepository.GetPostsByUserId(id)).OrderByDescending(p => p.Id);
            foreach (var post in posts)
                post.Comments = post.Comments.OrderBy(c => c.Id).ToList();
            return posts.ToList();
        }
    }
}
