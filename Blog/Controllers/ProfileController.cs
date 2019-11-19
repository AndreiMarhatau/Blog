using BLModels;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class ProfileController : Controller
    {
        private Random random;
        private IUserService userService;
        private ITokenService tokenService;
        private ICommentsAndPostsService commentsAndPostsService;
        private IPostsService postsService;
        private ICommentsService commentsService;
        private ILikeService likeService;

        public ProfileController(
            Random random,
            IUserService userService,
            ITokenService tokenService,
            ICommentsAndPostsService commentsAndPostsService,
            IPostsService postsService,
            ICommentsService commentsService,
            ILikeService likeService)
        {
            this.random = random;
            this.userService = userService;
            this.tokenService = tokenService;
            this.commentsAndPostsService = commentsAndPostsService;
            this.postsService = postsService;
            this.commentsService = commentsService;
            this.likeService = likeService;
        }

        public async Task<IActionResult> Index(string id)
        {
            var token = GenerateToken();
            bool isOwner = false;

            var userId = await tokenService.GetUserIdByToken(token);
            Guid Id;
            //Check get data
            try
            {
                if (id == null || id == userId.ToString())
                {
                    Id = userId;
                    isOwner = true;
                }
                else
                    Id = Guid.Parse(id);
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
                ViewBag.User = await userService.GetUserById(Id);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToAction("SignIn", "Home");
                throw;
            }

            //Add list of posts and comments to view model
            Tuple<bool, List<Post>> Model =
                new Tuple<bool, List<Post>>(
                    isOwner,
                    await commentsAndPostsService.GetCommentsAndPostsByUserId(Id)
                    );

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
                    await tokenService.RemoveToken(token);
                }
                catch (Exception e)
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
        public async Task<IActionResult> AddComment(string PostId, string UserId, string Text, string CommentId)
        {
            try
            {
                await commentsService.AddComment(Guid.Parse(PostId), await tokenService.GetUserIdByToken(GenerateToken()), Guid.Parse(CommentId), Text);
            }
            catch (Exception e)
            {
                if (!(e is ArgumentNullException || e is InvalidOperationException))
                    throw;
                return RedirectToAction("SignIn", "Home");
            }
            return RedirectToAction("Index", new { id = UserId });
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveLikeToPost(string UserId, string PostId)
        {
            await likeService.AddOrRemoveLike(
                await tokenService
                .GetUserIdByToken(
                    GenerateToken()),
                    new Post()
                    { Id = Guid.Parse(PostId) });

            return RedirectToAction("Index", new { id = UserId });
        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveLikeToComment(string UserId, string CommentId)
        {
            await likeService.AddOrRemoveLike(
                await tokenService
                .GetUserIdByToken(
                    GenerateToken()),
                new Comment() 
                { Id = Guid.Parse(CommentId) });

            return RedirectToAction("Index", new { id = UserId });
        }

        private string GenerateToken()
        {
            if (HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                return HttpContext.Request.Cookies["Token"];
            }

            //Generate new token and add to cookie
            byte[] bytes = new byte[256];
            this.random.NextBytes(bytes);
            var token = Encoding.UTF8.GetString(bytes);
            HttpContext.Response.Cookies.Append("Token", token);

            return token;
        }
    }
}