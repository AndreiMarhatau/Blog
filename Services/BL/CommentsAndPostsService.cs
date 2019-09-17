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

            if (posts.Count > 0)
            {
                int[] idsArray = new int[] { posts.First().UserId }
                    .Union(
                    from post in posts
                    from comm in post.Comments
                    select comm.AuthorId
                    ).ToArray();

                List<UserInfo> userInfoList =
                    (await _userRepository.GetManyUsersByIds(idsArray))
                    .Select(u => u.ToUserInfo())
                    .ToList();

                postsWithComments.AddRange(
                    from post in posts
                    let commentsInPost =
                        from comm in post.Comments
                        orderby comm.Id
                        select comm.ToCommentInPost(userInfoList.Single(i => i.Id == comm.AuthorId))
                    select post.ToPostViewModel(commentsInPost, userInfoList.Single(i => i.Id == post.UserId)));
            }
            return postsWithComments;
        }
    }
}
