using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcRead.Filter;

namespace MvcRead.Controllers
{
    [AuthorizeEx]
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Role()
        {
            return View();
        }
        public ActionResult Log()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        //public ActionResult ModifyPass()
        //{
        //    return View();
        //}
        public ActionResult Department()
        {
            return View();
        }
        public ActionResult Departmentt()
        {
            return View();
        }
       
    }
}
