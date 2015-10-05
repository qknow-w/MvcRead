using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using DefaultConnection;
using MvcRead.Factoty;
using MvcRead.IFactory;
using SS.Utilities.IP;

namespace MvcRead.Filter
{
   
    public class LogFilter:ActionFilterAttribute
    {
        private readonly ILogFactory logFactory;
        public LogFilter()
        {
            logFactory = new LogFactory();
        }
        public string Target { get; set; }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //HttpContext.Current
            if (HttpContext.Current.Session != null && HttpContext.Current.Session.Count > 0)
            {
                sys_log log = new sys_log()
                {
                    IP = GetIp.getIp(),
                    UserName = HttpContext.Current.Session["UserName"].ToString(),
                    Target = Target,
                    Date = DateTime.Now
                };
                logFactory.CreatLogService().AddLog(log);
            }
            else
            {
                HttpContext.Current.Response.Redirect("/admin/error");
            }
            base.OnActionExecuted(actionExecutedContext);
        }

        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    if (filterContext.HttpContext.Session != null && filterContext.HttpContext.Session.Count>0)
        //    {
        //        sys_log log = new sys_log()
        //        {               
        //            IP = GetIp.getIp(),
        //            UserName = filterContext.HttpContext.Session["UserName"].ToString(),
        //            Target = Target,
        //            Date = DateTime.Now
        //        };
        //        logFactory.CreatLogService().AddLog(log);
        //    }
        //    else
        //    {
        //        filterContext.HttpContext.Response.Redirect("/admin/error");
        //    }
        //    base.OnResultExecuted(filterContext);
        //}

       
    }
}