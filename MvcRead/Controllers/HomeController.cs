using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;

namespace MvcRead.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// PDF
        /// </summary>
        /// <returns></returns>
        public ActionResult PDF()
        {
            
            return View();
        }
        #region MyRegion
        /// <summary>
        /// ie pdf
        /// </summary>
        /// <returns></returns>
        public ActionResult Ie(string url)
        {
           //// return View();
           // //if (Request.Browser.Browser.ToLower() != "chrome")
           // //{

           //     //string filePath = Server.MapPath("/File/Pdf/helloworld.pdf");
           //     string filePath = Server.MapPath(url);
           //     Response.ClearContent();
           //     Response.ClearHeaders();
           //     string FilePost = filePath.Substring(filePath.Length - 3).ToLower();
           //     switch (FilePost)
           //     {
           //         case "pdf":
           //             Response.ContentType = "application/PDF";
           //             break;
           //         case "doc":
           //             Response.ContentType = "application/msword";
           //             break;
           //         case "xls":
           //             Response.ContentType = "application/vnd.ms-excel";
           //             break;
           //         default:
           //             Session["ErrorInfo"] = "不支持的文件格式:" + FilePost;
           //             Response.Redirect("ErrorPage.aspx");
           //             break;
           //     }
           //     Response.WriteFile(filePath);
           //     Response.Flush();
           //     Response.Close();
           //     Session.Remove("Report");

           // //}
           // //else if (Request.Browser.Browser.ToLower() == "chrome")
           // //{

           // //    string filePath = Server.MapPath(url);
           // //    Response.ClearContent();
           // //    Response.ClearHeaders();
           // //    string FilePost = filePath.Substring(filePath.Length - 3).ToLower();
           // //    Response.Clear();
           // //    Response.ClearHeaders();
           // //    Response.Buffer = false;

           // //    if (Request.Browser.Browser == "Firefox")
           // //        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + "1.pdf");
           // //    else
           // //        System.Web.HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("1.pdf", System.Text.Encoding.UTF8));

           // //    using (System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open))
           // //    {
           // //        byte[] by = new byte[fs.Length];
           // //        fs.Read(by, 0, by.Length);
           // //        Response.BinaryWrite(by);
           // //        Response.AddHeader("Accept-Language", "zh-tw");
           // //        Response.ContentType = "application/octet-stream";
           // //        Response.AppendHeader("Content-Length ", by.Length.ToString());
           // //        System.Web.HttpContext.Current.Response.Flush();
           // //        System.Web.HttpContext.Current.Response.End();
           // //    }
           // //}
            return View();
        }
        /// <summary>
        /// 非if pdf 
        /// </summary>
        /// <returns></returns>
        public ActionResult NoIe(string url,int id)
        {
            //ViewBag.URL = url;
            Session["url"] = url;
            Session["id"] = id;
            return View();
        }
        //[HttpPost]
        public string GetDataUrl()
        {
           // return Session["url"].ToString();
            if (Session["UserID"] != null)
            {
                return Session["url"].ToString();

            }
            else
            {
                IDataFactoty dataFactoty=new DataFactoty();
                List<int> cout = dataFactoty.CreatDataService().GetByAppID(Convert.ToInt32(Session["id"]));
                if (cout[0]>0)
                {
                    return Session["url"].ToString();
                }
               
            }
            return null;
            //ViewBag.URL = url;

        }
        #endregion
    }
}
