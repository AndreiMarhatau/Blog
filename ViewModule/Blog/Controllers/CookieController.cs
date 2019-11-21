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
        private static Random Random
        {
            get
            {
                if (random == null)
                {
                    lock (_lock)
                    {
                        if (random == null)
                        {
                            var temp = new Random();
                            Volatile.Write(ref random, temp);
                        }
                    }
                }
                return random;
            }
        }
        public static string GetOrGenerateToken(HttpContext httpContext)
        {
            if (httpContext.Request.Cookies.ContainsKey("Token"))
            {
                return httpContext.Request.Cookies["Token"];
            }

            //Generate new token and add to cookie
            byte[] bytes = new byte[256];
            Random.NextBytes(bytes);
            var token = Encoding.UTF8.GetString(bytes);
            httpContext.Response.Cookies.Append("Token", token);

            return token;
        }
    }
}
