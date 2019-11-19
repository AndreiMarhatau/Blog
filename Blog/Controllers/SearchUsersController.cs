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
    public class SearchUsersController : Controller
    {
        private IUserService userService;
        private ITokenService tokenService;

        public SearchUsersController(IUserService userService, ITokenService tokenService)
        {
            this.userService = userService;
            this.tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> SearchUsers(string Name, string Surname, string Login)
        {
            //Check authenticate before give access
            var token = CookieController.GetOrGenerateToken(HttpContext);
            try
            {
                await tokenService.GetUserIdByToken(token);
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToAction("SignIn", "Home");
                throw;
            }

            //Create result list of search
            List<User> resultList = await userService.SearchUsers(Login, Name, Surname);
            string pathBase = HttpContext.Request.PathBase;

            return View("Search", new Tuple<List<User>, string>(resultList, pathBase));
        }
        public async Task<IActionResult> SearchUsers()
        {
            return await SearchUsers("", "", "");
        }
    }
}