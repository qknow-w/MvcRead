using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcRead.Filter
{
    public class AuthorizeExAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Session != null && httpContext.Session.Count == 0)
            {
                httpContext.Response.Redirect("/admin/error");

            }

           // return base.AuthorizeCore(httpContext);
            return true;

        }
    }
}