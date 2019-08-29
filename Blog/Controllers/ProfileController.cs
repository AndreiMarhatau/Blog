using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using IServices;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class ProfileController : Controller
    {
        IUserService userService;
        ITokenService tokenService;
        ICommentsAndPostsService commentsAndPostsService;
        IPostsService postsService;
        ICommentsService commentsService;

        public ProfileController(
            IUserService userService, 
            ITokenService tokenService, 
            ICommentsAndPostsService commentsAndPostsService, 
            IPostsService postsService, 
            ICommentsService commentsService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
            this.commentsAndPostsService = commentsAndPostsService;
            this.postsService = postsService;
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var token = GenerateToken();

            string model = "";
            bool isOwner = false;

            var userId = await tokenService.GetUserIdByToken(token);

            //Check get data
            try
            {
                if (id == null || id == userId)
                {
                    id = userId;
                    isOwner = true;
                    model +=
                        "<div class=\"post_send\"><form>" +
                        "<textarea name = \"Text\"></textarea>" +
                        "<input type=\"submit\" formmethod=\"post\" " +
                        "value=\"Добавить пост\" formaction=\"/Profile/AddPost\"/>" +
                        "</form></div><br><br>";
                }
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToAction("SignIn", "Home");
                throw;
            }
            
            //Add content on the page
            try
            {
                ViewBag.User = await userService.GetUserById(id.Value);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToAction("SignIn", "Home");
                throw;
            }

            //Add strings of comments and posts for view

            Tuple<bool, List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>> Model =
                new Tuple<bool, List<Dictionary<Dictionary<string, string>, List<Dictionary<string, string>>>>>(
                    isOwner, 
                    await commentsAndPostsService.GetCommentsAndPostsByUserId(id.Value)
                    );
            
            model += await commentsAndPostsService.GetCommentsAndPostsByUserId(id.Value);

            return View("Profile", Model);
        }

        public async Task<IActionResult> SignOut()
        {
            //Remove token from cookies and DB and redirect
            string token;
            if (HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                token = HttpContext.Request.Cookies["Token"];
                HttpContext.Response.Cookies.Delete("Token");
                try
                {
                    await tokenService.RmToken(token);
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
            try
            {
                await postsService.AddPost(await tokenService.GetUserIdByToken(GenerateToken()), Text);
            }
            catch (Exception e)
            {
                if (!(e is ArgumentNullException || e is InvalidOperationException))
                    throw;
                return RedirectToAction("SignIn", "Home");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int PostId, int UserId, string Text, int CommentId)
        {
            try
            {
                await commentsService.AddComment(PostId, UserId, await tokenService.GetUserIdByToken(GenerateToken()), CommentId, Text);
            }
            catch (Exception e)
            {
                if (!(e is ArgumentNullException || e is InvalidOperationException))
                    throw;
                return RedirectToAction("SignIn", "Home");
            }
            return RedirectToAction("Index", new { id=UserId });
        }
        

        private string GenerateToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                return HttpContext.Request.Cookies["Token"];
            }

            //Generate new token and add to cookie
            byte[] bytes = new byte[512];
            new Random().NextBytes(bytes);
            var token = Encoding.UTF8.GetString(bytes);
            HttpContext.Response.Cookies.Append("Token", token);

            return token;
        }
    }
}