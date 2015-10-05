using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcRead.Filter
{
    public class ExceptionAttribute:HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //filterContext.HttpContext.Response.Redirect("/admin/error");
            base.OnException(filterContext);
        }
    }
}