using Domain.Core;
using Interfaces;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;

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

        public async Task<List<PostViewModel>>
            GetCommentsAndPostsByUserId(int id)
        {
            List<Post> posts = await _postsRepository.GetPostsByUserId(id);

            List<PostViewModel> postsWithComments = new List<PostViewModel>();
            UserInfo user = (await _userRepository.GetUserById(id)).ToUserInfo();

            foreach (var post in posts)
            {
                //Preparation comments
                List<CommentInPost> commentsInPost = new List<CommentInPost>();
                foreach (var comment in post.Comments)
                {
                    UserInfo author = (await _userRepository.GetUserById(comment.AuthorId)).ToUserInfo();
                    commentsInPost.Add(comment.ToCommentInPost(author));
                }

                //Preparation post and add to result list
                postsWithComments.Add(post.ToPostViewModel(commentsInPost, user));
            }

            return postsWithComments;
        }
    }
}
