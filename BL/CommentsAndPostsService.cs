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
    public class CommentsAndPostsService: ICommentsAndPostsService
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

        public async Task<string> GetCommentsAndPostsByUserId(int id)
        {
            List<Comment> comments = await _commentsRepository.GetCommentsByUserId(id);
            List<Post> posts = await _postsRepository.GetPostsByUserId(id);

            return await GetAllPostsAndComments(posts, comments, id);
        }


        #region PrivateMethods
        private async Task<string> GetAllPostsAndComments(List<Post> posts, List<Comment> comments, int userId)
        {
            string result = "";
            //Сортируем посты в нисходящем порядке (для отображения от новых к старым)
            posts = posts.OrderByDescending(i => i.Id).ToList();

            foreach (var post in posts)
            {
                //Вычисляем автора поста и добавляем его имя к посту
                var post_author = (await _userRepository.GetUserList()).Single(i => i.Id == post.UserId);
                result +=
                    $"{post_author.Name} {post_author.Surname} пишет: " +
                    post.GetHtml(userId);

                //Формируем комменты к постам, сортируем
                List<Comment> tempComments = comments.Where(i => i.PostId == post.Id).OrderBy(i => i.Id).ToList();
                foreach (var i in tempComments.Where(j => j.CommentId == -1))
                {
                    //И отправляем в рекурсионный поиск
                    result += await RecursionBuildCommentsHtmlString(userId, i, tempComments);
                }
            }
            return result;
        }
        private async Task<string> RecursionBuildCommentsHtmlString(int userId, Comment comment, List<Comment> comments, int nestingLevel = 1)
        {
            //Вычисляем автора коммента и добавляем его имя к нему
            var author = (await _userRepository.GetUserList()).Single(i => i.Id == comment.AuthorId);
            string result =
                $" <div class=\"comment_nesting_{nestingLevel}\">" +
                $"{author.Name} {author.Surname} отвечает: " +
                comment.GetHtml(userId) +
                "</div>";

            //Ищем детей-комментариев с учетом уровня вложенности (ограничиваем его числом 6)
            foreach (var i in comments)
            {
                if (comment.Id == i.CommentId)
                {
                    result += await RecursionBuildCommentsHtmlString(userId, i, comments, (nestingLevel >= 5) ? 6 : nestingLevel + 1);
                }
            }
            return result;
        }
        #endregion
    }
}
