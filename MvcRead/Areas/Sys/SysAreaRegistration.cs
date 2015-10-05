using System.Web.Http;
using System.Web.Mvc;

namespace MvcRead.Areas.Sys
{
    public class SysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Sys_default",
                "Sys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                this.AreaName + "Api",
                "api/" + this.AreaName + "/{controller}/{action}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );
           
        }
    }
}
