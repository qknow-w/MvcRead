using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class MenuService : IMenuService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 查询当前用户菜单
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public dynamic Get(int userID)
        {
            //var UserCode = this.User.Identity.Name;
            var sql = String.Format(@"SELECT     sys_roleMenuMap.ID, sys_roleMenuMap.RoleID, sys_roleMenuMap.MenuID, sys_menu.ParentID, sys_menu.MenuName, sys_menu.URL,
             sys_menu.IsVisible, sys_menu.IsEnable , sys_roleMenuMap.isEnble  FROM      sys_roleMenuMap INNER JOIN  sys_menu ON sys_roleMenuMap.MenuID = sys_menu.MenuID
where RoleID in( select RoleID from sys_user where userid=@0) and IsVisible=1 and IsEnable=1 
");
            var result = db.Query<Menu>(sql, userID).ToList();
            return result;
        }
        /// <summary>
        /// 查询当前用户菜单,根据角色id
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public dynamic GetByRoleID(int RoleID)
        {
            //var UserCode = this.User.Identity.Name;
            var sql = String.Format(@"SELECT     sys_roleMenuMap.ID, sys_roleMenuMap.RoleID, sys_roleMenuMap.MenuID, sys_menu.ParentID, sys_menu.MenuName, sys_menu.URL,
             sys_menu.IsVisible, sys_menu.IsEnable , sys_roleMenuMap.isEnble  FROM      sys_roleMenuMap INNER JOIN  sys_menu ON sys_roleMenuMap.MenuID = sys_menu.MenuID
where RoleID =@0 and IsVisible=1 and IsEnable=1 
");
            var result = db.Query<Menu>(sql, RoleID).ToList();
            return result;
        }
        
       
    }

    public class Menu
    {      
        public int RoleID { get; set; }
        public int RoleSeq { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public int ID { get; set; }
        public int MenuID { get; set; }
        public int? ParentID { get; set; }
        public string MenuName { get; set; }
        public string URL { get; set; }
        public string IconClass { get; set; }
        public string IconURL { get; set; }
        public string MenuSeq { get; set; }
        public bool? IsVisible { get; set; }
        public bool? IsEnable { get; set; }
        public string Class { get; set; }
        public string LiClass { get; set; }
        public bool? isEnble { get; set; }
    }
}