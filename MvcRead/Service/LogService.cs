using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class LogService:ILogService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 分页查询日志
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic GetLog(int curreent, int perPage)
        {
            var sql = String.Format(@"SELECT     LogID, UserName, IP, Target, Date
FROM    sys_log order by LogID desc ");
            var result = db.Page<sys_log>(curreent, perPage, sql);
            return result;
      
        }
        /// <summary>
        /// 添加日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public dynamic AddLog(sys_log log)
        {
            try
            {
                //user.IsEnable = true;
                db.BeginTransaction();
                // var sql = String.Format(@"insert sys_role(RoleName,Description) values (@0,@1)  ");
                //  int result = (int)db.Insert("sys_role", "role.RoleName,", role.RoleName, role.Description);
                var result = (int)db.Insert(log);

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