using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcRead.Handlers
{
    public class MyHttphandler:IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
           context.Response.Write("1233");
        }

        public bool IsReusable { get; private set; }
    }
}