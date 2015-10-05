using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS.Utilities.IP
{
    public class GetIp
    {
        public static string getIp()
        {
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
            else
                return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
