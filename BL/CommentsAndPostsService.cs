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
        ICommentsRepository _commentsRepository;
        IPostsRepository _postsRepository;
        IUserRepository _userRepository;

        public CommentsAndPostsService(ICommentsRepository commentsRepository, IPostsRepository postsRepository, IUserRepository userRepository)
        {
            _commentsRepository = commentsRepository;
            _postsRepository = postsRepository;
            _userRepository = userRepository;
        }

        public async Task<List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>>
            GetCommentsAndPostsByUserId(int id)
        {
            List<Post> posts = await _postsRepository.GetPostsByUserId(id);

            return await GetAllPostsAndCommentsByUserId(posts, id);
        }


        #region PrivateMethods

        private async Task<List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>>
            GetAllPostsAndCommentsByUserId(List<Post> posts, int userId)
        {
            List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>> result =
                new List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>();

            //posts = posts.OrderByDescending(i => i.Id).ToList();
            User user = await _userRepository.GetUserById(userId);

            foreach (var post in posts)
            {
                List<Dictionary<string, string>> resultComments = new List<Dictionary<string, string>>();

                var tempComments = post.Comments.ToList();
                foreach (var tempComment in tempComments.Where(i => i.CommentId == -1).ToList())
                {
                    resultComments = await GetCommentsRecursive(tempComment, tempComments);
                }

                result.Add(new Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>()
                {
                    {
                        new Dictionary<string, string>()
                        {
                            { "Id", post.Id.ToString() },
                            { "UserName", user.Name },
                            { "UserSurname", user.Surname },
                            { "UserId",post.UserId.ToString() },
                            { "Text", post.Text },
                            { "Date", post.Date.ToString() }
                            //Add other information if needed
                        },
                        resultComments
                    }
                });
            }
            return result;
        }

        private async Task<List<Dictionary<string, string>>> GetCommentsRecursive(Comment comment, List<Comment> comments, int nestingLevel = 0)
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();
            User user = await _userRepository.GetUserById(comment.AuthorId);

            result.Add(
                new Dictionary<string, string>()
                {
                    { "Id", comment.Id.ToString() },
                    { "AuthorName", user.Name },
                    { "AuthorSurname", user.Surname },
                    {"PostId",comment.PostId.ToString() },
                    {"UserId",comment.UserId.ToString() },
                    {"AuthorId",comment.AuthorId.ToString() },
                    {"Text",comment.Text },
                    {"Date",comment.Date.ToString() },
                    {"Nesting", nestingLevel.ToString() }
                    //Add other information if needed
                }
                );


            foreach (var _comment in comments.Where(i => i.CommentId == comment.Id))
            {
                result.AddRange(await GetCommentsRecursive(_comment, comments, nestingLevel + 1));
            }

            return result;
        }
        #endregion
    }
}
