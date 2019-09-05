using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Models;
using IServices;
using BL;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

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
            var token = GenerateToken(HttpContext);
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
            List<Dictionary<string,string>> resultList = await userService.SearchUsers(Login, Name, Surname);
            string pathBase = HttpContext.Request.PathBase;

            return View("Search", new Tuple<List<Dictionary<string,string>>, string>(resultList, pathBase));
        }
        public async Task<IActionResult> SearchUsers()
        {
            return await SearchUsers("", "", "");
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
    }
}