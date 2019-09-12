using Domain.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Helpers
{
    public static class ConvertForModelsExtensions
    {
        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel(
                user.Id,
                user.Login,
                user.Name,
                user.Surname,
                user.BornDate,
                user.RegisterDate,
                user.Email,
                user.Password);
        }
        public static PostViewModel ToPostViewModel(this Post post, IEnumerable<CommentInPost> comments, UserInfo user)
        {
            return new PostViewModel(
                post.Id,
                user.Name,
                user.Surname,
                post.UserId,
                post.Text,
                post.Date,
                comments.ToList());
        }
        public static CommentInPost ToCommentInPost(this Comment comment, UserInfo author)
        {
            return new CommentInPost(
                comment.Id,
                author.Name,
                author.Surname,
                comment.PostId,
                comment.UserId,
                comment.AuthorId,
                comment.Text,
                comment.Date,
                comment.CommentId);
        }
        public static UserInfo ToUserInfo(this User user)
        {
            return new UserInfo()
            {
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
