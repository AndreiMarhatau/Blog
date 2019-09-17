using System.Linq;

namespace Repositories.Helpers
{
    public static class Converter
    {
        public static Domain.Core.Token ToDomainModel(this EntityModels.Token model)
        {
            return new Domain.Core.Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }
        public static EntityModels.Token ToEntityModel(this Domain.Core.Token model)
        {
            return new EntityModels.Token()
            {
                StrToken = model.StrToken,
                UserId = model.UserId
            };
        }

        public static Domain.Core.User ToDomainModel(this EntityModels.User model)
        {
            return new Domain.Core.User(
                model.Id,
                model.Login,
                model.Name,
                model.Surname,
                model.BornDate,
                model.RegisterDate,
                model.Email,
                model.Password);
        }
        public static EntityModels.User ToEntityModel(this Domain.Core.User model)
        {
            return new EntityModels.User()
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

        public static Domain.Core.Post ToDomainModel(this EntityModels.Post model)
        {
            return new Domain.Core.Post()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                Comments = model.Comments.Select(i => i.ToDomainModel()).ToList()
            };
        }
        public static EntityModels.Post ToEntityModel(this Domain.Core.Post model)
        {
            return new EntityModels.Post()
            {
                Id = model.Id,
                UserId = model.Author.Id,
                Date = model.Date,
                Text = model.Text,
            };
        }

        public static Domain.Core.Comment ToDomainModel(this EntityModels.Comment model)
        {
            return new Domain.Core.Comment()
            {
                Id = model.Id,
                Date = model.Date,
                Text = model.Text,
                Author = model.Author.ToDomainModel(),
                CommentId = model.CommentId,
                PostId = model.PostId,
                UserId = model.UserId
            };
        }
        public static EntityModels.Comment ToEntityModel(this Domain.Core.Comment model)
        {
            return new EntityModels.Comment()
            {
                Id = model.Id,
                AuthorId = model.Author.Id,
                CommentId = model.CommentId,
                Date = model.Date,
                PostId = model.PostId,
                Text = model.Text,
                UserId = model.UserId
            };
        }
    }
}
