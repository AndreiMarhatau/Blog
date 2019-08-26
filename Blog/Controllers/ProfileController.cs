using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Blog.Controllers
{
    public class ProfileController : Controller
    {
        Db db;
        public ProfileController(Db db)
        {
            this.db = db;
        }

        public IActionResult Index(int? id)
        {
            var token = GenerateToken(HttpContext);

            string model = "";
            bool isOwner = false;
            
            //Check get data
            if(id == null)
            {
                try
                {
                    id = db.Tokens.Where(i => i.StrToken == token).Single().UserId;
                    isOwner = true;
                }
                catch (Exception e)
                {
                    if (e is ArgumentNullException || e is InvalidOperationException)
                        return RedirectToAction("SignIn", "Home");
                    throw;
                }
            }
            else
            {
                try
                {
                    if(id == db.Tokens.Where(i => i.StrToken == token).Single().UserId)
                        isOwner = true;
                }
                catch (Exception e)
                {
                    if (e is ArgumentNullException || e is InvalidOperationException)
                        return RedirectToAction("SignIn", "Home");
                    throw;
                }
            }
            //Check owner of profile and show/not show button to add post
            if(isOwner || db.Tokens.Where(i => i.StrToken == token).Single().UserId == id)
                model +=
                    "<div class=\"post_send\"><form>" +
                    "<textarea name = \"Text\"></textarea>" +
                    "<input type=\"submit\" formmethod=\"post\" " +
                    "value=\"Добавить пост\" formaction=\"AddPost\"/>" +
                    "</form></div><br><br>";

            //Add content on the page
            try
            {
                ViewBag.User = db.Users.Where(i => i.Id == id).Single();
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToAction("SignIn", "Home");
                throw;
            }

            //Add strings of comments and posts for view
            model += GetAllPostsAndComments(
            db
                .Posts
                .Where(i => i.UserId == id)
                .ToList(),
            db
                .Comments
                .Where(i => i.UserId == id)
                .ToList()
            , id.Value);

            return View("Profile", model);
        }

        public IActionResult CheckAuth(HttpContext httpContext)
        {
            var token = GenerateToken(HttpContext);
            if (db.Tokens.Where(i => i.StrToken == token).Count() <= 0)
                return RedirectToAction("SignIn", "Home");
            else
                return null;
        }

        public IActionResult SignOut()
        {
            //Remove token from cookies and DB and redirect
            string token;
            if (HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                token = HttpContext.Request.Cookies["Token"];
                HttpContext.Response.Cookies.Delete("Token");
                try
                {
                    db.Tokens.Remove(db.Tokens.Where(i => i.StrToken == token).Single());
                }
                catch(Exception e)
                {
                    if (!(e is ArgumentNullException || e is InvalidOperationException))
                        throw;
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(string Text)
        {
            if (Text.Replace(" ", "") == "")
                RedirectToAction("Index");

            int userId = db.Tokens.Where(i => i.StrToken == GenerateToken(HttpContext)).Single().UserId;
            db.Posts.Add(new Post()
            {
                Text = Text,
                Date = DateTime.Now,
                UserId = userId
            });
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
            //ToDo работа над ошибками
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int PostId, int UserId, string Text, int CommentId)
        {
            if (Text.Replace(" ", "") == "")
                RedirectToAction("Index");

            db.Comments.Add(new Comment()
            {
                PostId = PostId,
                UserId = UserId,
                Text = Text,
                CommentId = CommentId,
                Date = DateTime.Now,
                AuthorId = db.Tokens.Where(i => i.StrToken == GenerateToken(HttpContext)).Single().UserId
            });
            await db.SaveChangesAsync();

            return Redirect(HttpContext.Request.PathBase + "Index?id=" + UserId);
            //ToDo работа над ошибками
        }
        

        private string GenerateToken(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey("Token"))
            {
                return httpContext.Request.Cookies["Token"];
            }

            //Generate new token and add to cookie
            byte[] bytes = new byte[512];
            new Random().NextBytes(bytes);
            var token = Encoding.UTF8.GetString(bytes);
            httpContext.Response.Cookies.Append("Token", token);

            return token;
        }
        
        public string GetAllPostsAndComments(List<Post> posts, List<Comment> comments, int userId)
        {
            string result = "";
            //Сортируем посты в нисходящем порядке (для отображения от новых к старым)
            posts = posts.OrderByDescending(i => i.Id).ToList();

            foreach (var post in posts)
            {
                //Вычисляем автора поста и добавляем его имя к посту
                var post_author = db.Users.Where(i => i.Id == post.UserId).Single();
                result +=
                    post_author.Name + " " + post_author.Surname + " пишет: " +
                    post.GetHtml(userId);

                //Формируем комменты к постам, сортируем
                List<Comment> tempComments = comments.Where(i => i.PostId == post.Id).OrderBy(i => i.Id).ToList();
                foreach (var i in tempComments.Where(j => j.CommentId == -1))
                {
                    //И отправляем в рекурсионный поиск
                    result += RecursionBuildCommentsHtmlString(userId, i, tempComments);
                }
            }
            return result;
        }
        
        public string RecursionBuildCommentsHtmlString(int userId, Comment comment, List<Comment> comments, int nestingLevel = 1)
        {
            //Вычисляем автора коммента и добавляем его имя к нему
            var author = db.Users.Where(i => i.Id == comment.AuthorId).Single();
            string result =
                $" <div class=\"comment_nesting_{nestingLevel}\">" +
                author.Name + " " + author.Surname + " отвечает: " +
                comment.GetHtml(userId) +
                "</div>";

            //Ищем детей-комментариев с учетом уровня вложенности (ограничиваем его числом 6)
            foreach (var i in comments)
            {
                if (comment.Id == i.CommentId)
                {
                    result += RecursionBuildCommentsHtmlString(userId, i, comments, (nestingLevel >= 5) ? 6 : nestingLevel + 1);
                }
            }
            return result;
        }
    }
}