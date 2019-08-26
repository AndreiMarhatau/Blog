using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public Db db;

        public HomeController(Db db)
        {
            this.db = db;
        }

        public IActionResult Index(Boolean isSignIn = true)
        {
            var defaultViewToReturn = isSignIn ? "signin" : "signup";

            //If token in cookies is not exists add them and return
            if (!HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                ViewBag.Token = GenerateToken(HttpContext);
                return View(defaultViewToReturn);
            }
            //Check token in the cookie
            var token = GenerateToken(HttpContext);
            try
            {
                //Check token and user in DB
                db.Users.Where(i => i.Id == db.Tokens.Where(j => j.StrToken == token).Single().UserId).Single();
                return RedirectToAction("Index", "Profile");
            }
            //Catching all exceptions for returning to user a view (DataBase is in safety)
            catch(Exception)
            {

                return View(defaultViewToReturn);
            }
        }


        public IActionResult SignIn()
        {
            return Index(true);
        }
        public IActionResult SignUp()
        {
            return Index(false);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string Login, string Password)
        {
            //Check login and hash of password
            if (Login == null || Password == null)
            {
                ViewBag.Alert = "Остались пустые поля";
                return SignIn();
            }

            var hashPass = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)));
            var user = db.Users.Where(i => i.Login.Equals(Login) && i.Password.Equals(hashPass));

            if (user.Count() > 0)
            {
                //If authenticate is successfully add token to db
                var new_token = GenerateToken(HttpContext);
                db.Tokens.Add(new Token() { UserId = user.Single().Id, StrToken = new_token });
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Alert = "Неверный логин и(или) пароль";
                return SignIn();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password)
        {
            //Chech all parameters
            if (Login == null ||
                Name == null ||
                Surname == null ||
                BornDate == null ||
                !Email.Replace(" ", "").Contains("@") ||
                Password == null)
            {
                ViewBag.Alert = "Остались пустые или неверно введены поля";
                return SignUp();
            }

            //Check to busy login or email
            var user = db.Users.Where(i => i.Login.Equals(Login) ||
                                           i.Email.Equals(Email));
            if (user.Count() > 0)
            {
                ViewBag.Alert = "Логин или емейл уже заняты.";
                return SignUp();
            }

            else
            {
                //Add new user to db
                db.Users.Add(new Models.User()
                {
                    Login = Login,
                    Name = Name,
                    Surname = Surname,
                    BornDate = BornDate,
                    Email = Email,
                    Password = Encoding.UTF8.GetString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(Password)))
                });
                await db.SaveChangesAsync();

                //Add new token to db
                user = db.Users.Where(i => i.Login == Login);
                
                var new_token = GenerateToken(HttpContext);
                db.Tokens.Add(new Token() { UserId = user.Single().Id, StrToken = new_token });
                await db.SaveChangesAsync();

                return RedirectToAction("Index");
            }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}