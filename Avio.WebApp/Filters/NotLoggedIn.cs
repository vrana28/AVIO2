using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avio.WebApp.Filters
{
    public class NotLoggedIn : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.Filters.OfType<NotLoggedIn>().Any())
            {
                return;
            }

            if (context.HttpContext.Session.GetInt32("userid") == null)
            {
                context.HttpContext.Response.Redirect("/User/Login");
                return;
            }
        }
    }
}
