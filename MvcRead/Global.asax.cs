using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;

namespace MvcRead
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static public SoftKey m_softkey;
        protected void Application_Start()
        {
            m_softkey = new SoftKey();
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();//清除xml  只返回json
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }
        public class SoftKey
        {
            [DllImport("kernel32.dll")]
            public static extern int lstrlenA(string InString);
            [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
            public static extern void CopyStringToByte(byte[] pDest, string pSourceg, int ByteLenr);
            [DllImport("kernel32.dll", EntryPoint = "RtlMoveMemory")]
            public static extern void CopyByteToString(StringBuilder pDest, byte[] pSource, int ByteLenr);
        }
    }
}