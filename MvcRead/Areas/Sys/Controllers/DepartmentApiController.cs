using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using DefaultConnection;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;

namespace MvcRead.Areas.Sys.Controllers
{
    public class DepartmentApiController:ApiController
    {
        private IDepartmentFactory departmentFactory;
        public DepartmentApiController()
        {
            departmentFactory=new DepartmentFactory();
        }
        /// <summary>
        /// 修改部门
        /// </summary>
        /// <returns></returns>
        public dynamic Get()
        {
            return departmentFactory.CreatService().Get();
        }
        [LogFilter(Target = "修改部门")]
        [System.Web.Http.HttpPost]
        public dynamic EditDepart(sys_department department)
        {
            if (departmentFactory.CreatService().EditDepart(department))
            {
                return "OK";
            }
            return "FAIL";

            // return new RedirectResult("/user/index");

            //return "<script>alert('123')</script>";
            ;
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [LogFilter(Target = "删除部门")]
        [System.Web.Http.HttpPost]
        public dynamic DeleteDepart(sys_department department)
        {
            if (departmentFactory.CreatService().DeleteDepart(department))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [LogFilter(Target = "添加部门")]
        [System.Web.Http.HttpPost]
        public dynamic AddDepart(sys_department department)
        {
            if (departmentFactory.CreatService().AddDepart(department))
            {
                return "OK";
            }
            return "FAIL";
        }
    }
}
