﻿using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories.Helpers
{
    public static class Converter
    {
        public static DomainModels.Token ToDomainModel(this Token model)
        {
            return new DomainModels.Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }
        public static Token ToEntityModel(this DomainModels.Token model)
        {
            return new Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }

        public static DomainModels.User ToDomainModel(this User model)
        {
            return new DomainModels.User(
                model.Id,
                model.Login,
                model.Name,
                model.Surname,
                model.BornDate,
                model.RegisterDate,
                model.Email,
                model.Password);
        }
        public static User ToEntityModel(this DomainModels.User model)
        {
            return new User()
            {
                Id = model.Id,
                Login = model.Login,
                Name = model.Name,
                Surname = model.Surname,
                BornDate = model.BornDate,
                RegisterDate = model.RegisterDate,
                Email = model.Email,
                Password = model.Password
            };
        }

        public static DomainModels.Post ToDomainModel(this Post model)
        {
            return new DomainModels.Post()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                Comments = model.Comments.Select(i => i.ToDomainModel()).ToList(),
                Likes = model.Likes.Select(i => i.ToDomainModel()).ToList()
            };
        }
        public static Post ToEntityModel(this DomainModels.Post model)
        {
            return new Post()
            {
                Id = model.Id,
                AuthorId = model.Author.Id,
                Date = model.Date,
                Text = model.Text
            };
        }

        public static DomainModels.Comment ToDomainModel(this Comment model)
        {
            return new DomainModels.Comment()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                CommentId = model.CommentId,
                PostId = model.PostId,
                Likes = model.Likes.Select(i => i.ToDomainModel()).ToList()
            };
        }
        public static Comment ToEntityModel(this DomainModels.Comment model)
        {
            return new Comment()
            {
                Id = model.Id,
                AuthorId = model.Author.Id,
                CommentId = model.CommentId,
                Date = model.Date,
                PostId = model.PostId,
                Text = model.Text
            };
        }

        public static Like ToEntityModel(this DomainModels.Like model)
        {
            var temp = new Like()
            {
                Id = model.Id,
                UserId = model.UserId
            };
            if (model.LikableType == DomainModels.LikableType.Post)
                temp.PostId = model.LikableId;
            else
                temp.CommentId = model.LikableId;
            return temp;
        }
        public static DomainModels.Like ToDomainModel(this Like model)
        {
            var temp = new DomainModels.Like()
            {
                Id = model.Id,
                UserId = model.UserId
            };
            if (model.PostId.HasValue)
            {
                temp.LikableId = model.PostId.Value;
                temp.LikableType = DomainModels.LikableType.Post;
            }
            else
            {
                temp.LikableId = model.CommentId.Value;
                temp.LikableType = DomainModels.LikableType.Comment;
            }
            return temp;
        }
    }
}
