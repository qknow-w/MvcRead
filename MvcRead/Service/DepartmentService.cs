using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.Areas.Sys.Models;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class DepartmentService:IDepartmentService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 分页部门
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic Get()
        {
//            var sql = String.Format(@"SELECT     DepartmentID, DepartmentName, Description, CreateDate
//FROM         sys_department  where IsEnable=1 order by DepartmentID desc  ");
            var sql = String.Format(@"SELECT   DepartmentID AS ID, DepartmentName AS text,ParentID AS parentID
FROM     sys_department  where IsEnable=1 order by DepartmentID desc  ");
            var result = db.Query<TreeNode>(sql);
            return result;
        }
        /// <summary>
        /// 编辑部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic EditDepart(sys_department department)
        {
            try
            {
                db.BeginTransaction();
                var sql = String.Format(@"update sys_department
 set DepartmentName=@0 ,Description=@1
 where DepartmentID=@2 ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                db.Execute(sql, department.DepartmentName, department.Description, department.DepartmentID);

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
        /// 编辑部门
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public dynamic DeleteDepart(sys_department department)
        {
            try
            {
                db.BeginTransaction();
                var sql = String.Format(@"update sys_department
 set IsEnable=0 
 where DepartmentID=@0 ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                db.Execute(sql, department.DepartmentID);

                db.CompleteTransaction();
                return true;

            }
            catch (Exception)
            {

                return false;
            }

            return false;
        }
        public dynamic AddDepart(sys_department department)
        {
            try
            {
                department.IsEnable = true;
                department.CreateDate = DateTime.Now;
                db.BeginTransaction();
                // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                var result = (int)db.Insert(department);

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
}