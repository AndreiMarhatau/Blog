using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLModels
{
    public static class Converter
    {
        public static User ToBLModel(this Domain.Core.User user)
        {
            return new User(
                user.Id,
                user.Login,
                user.Name,
                user.Surname,
                user.BornDate,
                user.RegisterDate,
                user.Email
                );
        }
        public static List<Post> ToBLModel(this List<Domain.Core.Post> posts)
        {
            return
                (from post in posts
                 select new Post()
                 {
                     Id = post.Id,
                     Author = post.Author.ToUserInfo(),
                     Text = post.Text,
                     Date = post.Date,
                     Comments = (from comment in post.Comments
                                 select new Comment()
                                 {
                                     Id = comment.Id,
                                     PostId = comment.PostId,
                                     UserId = comment.UserId,
                                     CommentId = comment.CommentId,
                                     Author = comment.Author.ToUserInfo(),
                                     Date = comment.Date,
                                     Text = comment.Text
                                 }).ToList()
                 }).ToList();
        }

        public static UserInfo ToUserInfo(this Domain.Core.User user)
        {
            return new UserInfo()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}
