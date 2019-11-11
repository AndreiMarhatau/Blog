using BLModels;
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

        public CommentsAndPostsService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<List<BLModels.Post>> GetCommentsAndPostsByUserId(int id)
        {
            var posts = (await _postsRepository.GetPostsByUserId(id)).OrderByDescending(p => p.Id);
            foreach (var post in posts)
                post.Comments = post.Comments.OrderBy(c => c.Id).ToList();
            return posts.ToList().ToBLModel();
        }
    }
}
