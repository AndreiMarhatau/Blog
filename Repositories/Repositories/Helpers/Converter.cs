using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repositories
{
    internal static class Converter
    {
        internal static DomainModels.Token ToDomainModel(this Token model)
        {
            return new DomainModels.Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }
        internal static Token ToEntityModel(this DomainModels.Token model)
        {
            return new Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }

        internal static DomainModels.User ToDomainModel(this User model)
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
        internal static User ToEntityModel(this DomainModels.User model)
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

        internal static DomainModels.Post ToDomainModel(this Post model)
        {
            return new DomainModels.Post()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                Comments = model.Comments.Select(i => i.ToDomainModel()).ToList()
            };
        }
        internal static Post ToEntityModel(this DomainModels.Post model)
        {
            return new Post()
            {
                Id = model.Id,
                UserId = model.Author.Id,
                Date = model.Date,
                Text = model.Text,
            };
        }

        internal static DomainModels.Comment ToDomainModel(this Comment model)
        {
            return new DomainModels.Comment()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                CommentId = model.CommentId,
                PostId = model.PostId
            };
        }
        internal static Comment ToEntityModel(this DomainModels.Comment model)
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
    }
}
