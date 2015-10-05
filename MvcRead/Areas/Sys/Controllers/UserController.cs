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

namespace MvcRead.Areas.Sys.Controllers
{
    [AuthorizeExAttribute]
    public class UserController:ApiController
    {
        private readonly IUserFactoty userFactoty;
        public UserController()
        {
            userFactoty=new UserFactoty();
        }
        /// <summary>
        /// 查询出所有 用户 
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
       [LogFilter(Target = "查看用户")]
        public dynamic GetUser(int curreent=1, int perPage=10)
        {
            return userFactoty.CreatUserService().GetUser(curreent, perPage);
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       [LogFilter(Target = "新增用户")]
        [System.Web.Http.HttpPost]
        public dynamic AddUser(sys_user user)
        {
               if ( userFactoty.CreatUserService().AddUser(user))
           {
               return "OK";
           }
           return "FAIL"; 
            //return new RedirectResult("/user/index");
          
            //return "<script>alert('123')</script>";
            ;
        }
        /// <summary>
        /// x编辑用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [LogFilter(Target = "修改用户")]
        [System.Web.Http.HttpPost]
       public dynamic EditUser(sys_user user)
       {
           if ( userFactoty.CreatUserService().EditUser(user))
           {
               return "OK";
           }
           return "FAIL"; 
          
          // return new RedirectResult("/user/index");

           //return "<script>alert('123')</script>";
           ;
       }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       [LogFilter(Target = "删除用户")]
        [System.Web.Http.HttpPost]
       public dynamic DeleteUser(sys_user user)
        {
            if (userFactoty.CreatUserService().DeleteUser(user))
            {
                return "OK";
            }
            return "FAIL"; 
        }
        /// <summary>
       /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
       [LogFilter(Target = "修改密码")]
       [System.Web.Http.HttpGet]
       public dynamic ModifyPassword(int userID,string pass)
        {
            //  return pass;
            if (userFactoty.CreatUserService().ModifyPassword(userID, pass))
            {
                return "OK";
            }
            return "Fail";
        }

    }
}