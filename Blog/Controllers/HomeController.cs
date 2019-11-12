using Blog.Models;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private Random random;
        private IUserService userService;
        private ITokenService tokenService;

        public HomeController(Random random, IUserService userService, ITokenService tokenService)
        {
            this.random = random;
            this.userService = userService;
            this.tokenService = tokenService;
        }

        public async Task<IActionResult> Index()
        {
            return await Sign(SignMethod.SignIn);
        }
        public async Task<IActionResult> SignIn()
        {
            return await Sign(SignMethod.SignIn);
        }
        public async Task<IActionResult> SignUp()
        {
            return await Sign(SignMethod.SignUp);
        }

        public enum SignMethod
        {
            SignIn,
            SignUp
        }

        [NonAction]
        public async Task<IActionResult> Sign(SignMethod sign)
        {
            string defaultView = sign == SignMethod.SignIn ? "signin" : "signup";

            if (!HttpContext.Request.Cookies.ContainsKey("Token"))
            {
                ViewBag.Token = GenerateToken();
                return View(defaultView);
            }
            //Check token in the cookie
            var token = GenerateToken();
            try
            {
                //Check token and user in DB
                await userService.GetUserById(await tokenService.GetUserIdByToken(token));
                return RedirectToAction("Index", "Profile");
            }
            //Catching all exceptions for returning to user a view (DataBase is in safety)
            catch (Exception)
            {
                return View(defaultView);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(string Login, string Password)
        {
            try
            {
                int id = await userService.CheckUser(Login, Password);
                await tokenService.AddToken(GenerateToken(), id);
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                if (e is InvalidOperationException)
                {
                    ViewBag.Alert = "Неверный логин или пароль";
                    return View();
                }
                else if (e is ArgumentNullException)
                {
                    ViewBag.Alert = "Одно или несколько полей пустые";
                    return View();
                }
                else
                    throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string Login, string Name, string Surname, DateTime BornDate, string Email, string Password)
        {
            try
            {
                int id = await userService.AddUser(Login, Name, Surname, BornDate, Email, Password);
                await tokenService.AddToken(GenerateToken(), id);
                return RedirectToAction("Index", "Profile");
            }
            catch (Exception e)
            {
                if (e is ArgumentNullException)
                {
                    ViewBag.Alert = "Одно или несколько полей пустые";
                    return View();
                }
                else if (e is ArgumentException)
                {
                    ViewBag.Alert = "Логин и(или) Email заняты";
                    return View();
                }

                else
                    throw;
            }
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}