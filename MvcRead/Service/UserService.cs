using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class UserService:IUserService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic GetUser(int curreent, int perPage)
        {
            var sql = String.Format(@"SELECT     UserID, UserName, sys_user.Description, Password, sys_user.RoleID,sys_role.RoleName, Depart, Phone,sys_user.RealName
FROM      sys_user inner join sys_role on sys_user.RoleID=sys_role.RoleID where sys_user.IsEnable=1 order by UserID desc");
            var result = db.Page<user>(curreent, perPage, sql);
            return result;
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic AddUser(sys_user user)
        {
            try
            {
                user.IsEnable = true;
                db.BeginTransaction();
                // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                var result = (int)db.Insert(user);
                
                db.CompleteTransaction();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic EditUser(sys_user user)
        {
            try
            {
                user.IsEnable = true;
                db.BeginTransaction();
                // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                db.Update(user);

                db.CompleteTransaction();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic DeleteUser(sys_user user)
        {
            try
            {
                user.IsEnable = false;
                db.BeginTransaction();
                // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                var sql = String.Format(@"update sys_user set  IsEnable=0   where UserID=@0      ");
                var result = db.Execute(sql, user.UserID);

                db.CompleteTransaction();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic LoginIn(sys_user user)
        {
            var sql = String.Format(@"select *
from sys_user
where UserName=@0 and Password=@1 and IsEnable=1 ");
            return db.Query<sys_user>(sql,user.UserName, user.Password).ToList();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic ModifyPassword(int userID, string pass)
        {
            try
            {
                db.BeginTransaction();
                var sql = String.Format(@"update sys_user
set Password=@0
where UserID=@1 ");
                var result = db.Execute(sql, pass, userID);
                db.CompleteTransaction();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
    }

    public class user
    {       
        public int UserID { get; set; }
        public string UserSeq { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public int? RoleID { get; set; }
        public string RealName { get; set; }
        public string Depart { get; set; } 
        public string Phone { get; set; }
        public bool? IsEnable { get; set; }
        public string RoleName { get; set; }
    }
}