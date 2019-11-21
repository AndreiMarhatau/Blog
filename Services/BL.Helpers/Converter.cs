using BLModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BL.Helpers
{
    public static class Converter
    {
        public static User ToBLModel(this DomainModels.User user)
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
        public static List<Post> ToBLModel(this List<DomainModels.Post> posts)
        {
            return
                (from post in posts
                 select new Post()
                 {
                     Id = post.Id,
                     Author = post.Author.ToUserInfo(),
                     Text = post.Text,
                     Date = post.Date,
                     Likes = post.Likes.Select(i => i.ToBLModel()).ToList(),
                     Comments = (from comment in post.Comments
                                 select new Comment()
                                 {
                                     Id = comment.Id,
                                     PostId = comment.PostId,
                                     CommentId = comment.CommentId,
                                     Author = comment.Author.ToUserInfo(),
                                     Date = comment.Date,
                                     Text = comment.Text,
                                     Likes = comment.Likes.Select(i => i.ToBLModel()).ToList()
                                 }).ToList()
                 }).ToList();
        }

        public static UserInfo ToUserInfo(this DomainModels.User user)
        {
            return new UserInfo()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname
            };
        }

        public static Like ToBLModel(this DomainModels.Like model)
        {
            return new Like()
            {
                Id = model.Id,
                LikableId = model.LikableId,
                LikableType = (LikableType)model.LikableType,
                UserId = model.UserId
            };
        }

        public static DomainModels.Post ToDomainModel(this Post post)
        {
            return new DomainModels.Post()
            {
                Id = post.Id,
                Date = post.Date,
                Text = post.Text
            };
        }
        public static DomainModels.Comment ToDomainModel(this Comment comment)
        {
            return new DomainModels.Comment()
            {
                Id = comment.Id,
                Date = comment.Date,
                Text = comment.Text,
                CommentId = comment.CommentId,
                PostId = comment.PostId
            };
        }
    }
}
