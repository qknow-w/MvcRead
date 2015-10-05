using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;
using MvcRead.IService;
using MvcRead.Service;

namespace MvcRead.Areas.Sys.Controllers
{
    [AuthorizeEx]
    public class MenuController : Controller
    {
        //
        // GET: /Sys/Menu/      
        public ActionResult Index()
        {
            return View();
            
        }
        
    }

    public class MenuApiController : ApiController
    {
        private readonly IMenuFactory MenuFactory;
        public MenuApiController()
        {
            MenuFactory = new MenuFacoty();
        }
        public dynamic GetByRole(int RoleID)
        {
            return MenuFactory.CreatMenuService().GetByRoleID(RoleID);
            //return "234";
        }
        
        
    }




}
