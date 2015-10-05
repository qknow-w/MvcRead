using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.IService;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MvcRead.Service
{
    public class RoleService:IRoleService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 查询角色
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic Get(int curreent, int perPage )
        {
            var sql = String.Format(@"SELECT     RoleID, RoleSeq, RoleName, Description  FROM      sys_role
");
            var result = db.Page<sys_role>(curreent, perPage, sql);
            return result;
        }

        /// <summary>
        /// 更改角色菜单权限
        /// </summary>
        /// <returns></returns>
        public dynamic ChangeRoleMenu(dynamic jsonObject)
        {
            try
            {
                db.BeginTransaction();
                foreach (var j in jsonObject)
                {
                    //string i = j["ID"].ToString();

                    //int k = Convert.ToInt32(i);

                    //string q = j["isEnble"].ToString();
                    //bool w = Convert.ToBoolean(q);
                    //Convert.ToInt32(j["ID"]);
                    var sql = String.Format(@"update sys_roleMenuMap set isEnble=@0 where ID=@1
                                      ");
                    var result = db.Execute(sql, Convert.ToBoolean(j["isEnble"].ToString()), Convert.ToInt32(j["ID"].ToString()));
                    //return result;             
                }

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
        /// 修改角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public dynamic ModifyRole(sys_role role)
        {
            try
            {
                db.BeginTransaction();

                var sql = String.Format(@"update sys_role set  RoleName=@0  , Description=@1 where RoleID=@2                                     ");
                var result = db.Execute(sql, role.RoleName,role.Description,role.RoleID);
                   
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
        /// 删除角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public dynamic DelectRole(sys_role role)
        {
            try
            {
                db.BeginTransaction();

                var sql = String.Format(@"delete from sys_role  where RoleID=@0  ");
                var result = db.Execute(sql, role.RoleID);

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
        /// 新增角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public dynamic AddRole(sys_role role)
        {
            try
            {
                db.BeginTransaction();
               // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
              //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                int result = (int)db.Insert("sys_role", "RoleID", role);
                var sql1=String.Format(@"INSERT sys_roleMenuMap(RoleID,MenuID,isEnble) values (@0,1,1),(@1,2,1),(@2,3,1),(@3,4,1)
                                              ,(@4,5,1),(@5,6,1),(@6,7,1),(@7,8,1),,(@7,9,1)");
                db.Execute(sql1, result, result, result, result, result, result, result, result);
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
        /// 查询出角色，绑定下拉杠
        /// </summary>
        /// <returns></returns>
        public dynamic Gett()
        {
            var sql = String.Format(@"SELECT     RoleID, RoleName FROM         sys_role 
");
            var result = db.Query<sys_role>(sql);
            return result;
        }
    }
}