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
    public class SearchController : Controller
    {
        Db db;

        public SearchController(Db db)
        {
            this.db = db;
        }
        
        [HttpPost]
        public IActionResult SearchUsers(string Name, string Surname, string Login)
        {
            //Check authenticate before give access
            var token = GenerateToken(HttpContext);
            try
            {
                db.Tokens.Where(i => i.StrToken == token).Single();
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException || e is InvalidOperationException)
                    return RedirectToActionPermanent("SignIn", "Home");
                throw;
            }

            //Check params
            if (Name == null) Name = "";
            if (Surname == null) Surname = "";
            if (Login == null) Login = "";

            //Create result list of search
            var resultList = db.Users
                .Where(
                i => i.Name.ToLower().Contains(Name.ToLower()) &&
                i.Surname.ToLower().Contains(Surname.ToLower()) &&
                i.Login.ToLower().Contains(Login.ToLower())).ToList();

            //Create string result from result list
            string result = "";
            foreach (var i in resultList)
            {
                result +=
                    $"<a href=\"{HttpContext.Request.PathBase}/Profile/Index?id={i.Id}\"><br>" +
                    i.Login + ": " + i.Name + " " + i.Surname +
                    i.BornDate + "</a><br><br>";
            }

            return View("Search", result);
        }
        public IActionResult SearchUsers()
        {
            return SearchUsers("", "", "");
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