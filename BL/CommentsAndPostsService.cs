using Domain.Core;
using Interfaces;
using IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CommentsAndPostsService : ICommentsAndPostsService
    {
        private ICommentsRepository _commentsRepository;
        private IPostsRepository _postsRepository;
        private IUserRepository _userRepository;

        public CommentsAndPostsService(ICommentsRepository commentsRepository, IPostsRepository postsRepository, IUserRepository userRepository)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            _userRepository = userRepository;
        }

        public async Task<List<PostWithComments>>
            GetCommentsAndPostsByUserId(int id)
        {
            List<Post> posts = await _postsRepository.GetPostsByUserId(id);

            List<PostWithComments> postsWithComments = new List<PostWithComments>();
            User user = await _userRepository.GetUserById(id);

            foreach (var post in posts)
            {
                //Preparation comments
                List<CommentInPost> commentsInPost = new List<CommentInPost>();
                foreach (var comment in post.Comments)
                {
                    var author = await _userRepository.GetUserById(comment.AuthorId);
                    commentsInPost.Add(new CommentInPost(new Dictionary<string, string>()
                    {
                        { "Id", comment.Id.ToString() },
                        { "AuthorName", author.Name },
                        { "AuthorSurname", author.Surname },
                        {"PostId",comment.PostId.ToString() },
                        {"UserId",comment.UserId.ToString() },
                        {"AuthorId",comment.AuthorId.ToString() },
                        {"Text",comment.Text },
                        {"Date",comment.Date.ToString() },
                        {"CommentId",comment.CommentId.ToString() }
                        //Add other information if needed
                    }));
                }

                //Preparation post and add to result list
                postsWithComments.Add(new PostWithComments(new Dictionary<string, string>()
                {
                    { "Id", post.Id.ToString() },
                    { "UserName", user.Name },
                    { "UserSurname", user.Surname },
                    { "UserId",post.UserId.ToString() },
                    { "Text", post.Text },
                    { "Date", post.Date.ToString() }
                    //Add other information if needed
                }, commentsInPost));
            }

            return postsWithComments;
        }
    }
}
