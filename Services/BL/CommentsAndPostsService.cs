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

            List<int> ids = new List<int>();
            foreach(var post in posts)
            {
                ids.Add(post.UserId);
                foreach(var comment in post.Comments)
                {
                    ids.Add(comment.AuthorId);
                }
            }
            ids = ids.Distinct().ToList();

            List<UserInfo> userInfoList = 
                (await _userRepository.GetManyUsersByIds(ids.ToArray()))
                .Select(u => u.ToUserInfo())
                .ToList();

            foreach (var post in posts)
            {
                //Preparation comments
                List<CommentInPost> commentsInPost = new List<CommentInPost>();
                foreach (var comment in post.Comments)
                {
                    commentsInPost.Add(comment.ToCommentInPost(userInfoList.Single(i => i.Id == comment.AuthorId)));
                }

                //Preparation post and add to result list
                postsWithComments.Add(post.ToPostViewModel(commentsInPost, userInfoList.Single(i => i.Id == post.UserId)));
            }

            return postsWithComments;
        }
    }
}
