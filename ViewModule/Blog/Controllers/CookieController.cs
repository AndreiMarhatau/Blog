using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Controllers
{
    public class CookieController
    {
        private static Random random;
        private static object _lock = new object();
        public CookieController(Random r)
        {
            random = r;
        }
        public static string GetOrGenerateToken(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey("Token"))
            {
                return httpContext.Request.Cookies["Token"];
            }

            //Generate new token and add to cookie
            byte[] bytes = new byte[256];
            random.NextBytes(bytes);
            var token = Encoding.UTF8.GetString(bytes);
            httpContext.Response.Cookies.Append("Token", token);

            return token;
        }
    }
}
