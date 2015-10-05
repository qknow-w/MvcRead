using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DefaultConnection;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;
using MvcRead.Service;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcRead.Areas.Sys.Controllers
{
    [AuthorizeExAttribute]
    public class RoleController : Controller
    {
        //
        // GET: /Sys/Role/

        public ActionResult Index()
        {
            return null;
        }
        

    }

    public class RoleApiController : ApiController
    {
        private readonly IRoleFactory roleFactory;
        public RoleApiController()
        {
            roleFactory=new RoleFactory();
        }

        public dynamic Get(int curreent=1,int perPage=10)
        {
            return roleFactory.CreatRoleService().Get(curreent, perPage);
        }
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
       [LogFilter(Target = "修改角色权限")]
        [System.Web.Http.HttpPost]
        public dynamic Change(dynamic data)
        {
            //List<Menu> list=JsonConvert.DeserializeObject<List<Menu>>(data);

            if (roleFactory.CreatRoleService().ChangeRoleMenu(data))
            {
                return "OK";
            }
            return "FAIL";
           
            //return "234";
        }
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [LogFilter(Target = "修改角色信息")]
        [System.Web.Http.HttpPost]
        public dynamic ModifyRole(sys_role data)
        {
            if (roleFactory.CreatRoleService().ModifyRole(data))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        [LogFilter(Target = "修改角色")]
        public dynamic DelectRole(sys_role data)
        {
            if (roleFactory.CreatRoleService().DelectRole(data))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [LogFilter(Target = "新增角色")]
        [System.Web.Http.HttpPost]
        public dynamic AddRole(sys_role data)
        {
            if (roleFactory.CreatRoleService().AddRole(data))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 查询出角色，绑定下拉杠
        /// </summary>
        /// <returns></returns>
        public dynamic Gett()
        {
            return roleFactory.CreatRoleService().Gett();
        }
        
    }
}
