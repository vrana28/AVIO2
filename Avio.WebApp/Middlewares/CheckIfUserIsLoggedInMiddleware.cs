using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avio.WebApp.Middlewares
{
    public class CheckIfUserIsLoggedInMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckIfUserIsLoggedInMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            int? id = httpContext.Session.GetInt32("userid");
            if (id == null && httpContext.Request.Path != "/User/Login")
            {
                httpContext.Response.Redirect("/User/Login");
            }
            return _next(httpContext);
        }
    }

    public static class CheckIfUserIsLoggedInMiddlewareExtensions
    {
        public static IApplicationBuilder UseCheckIfUserIsLoggedInMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckIfUserIsLoggedInMiddleware>();
        }
    }
}
