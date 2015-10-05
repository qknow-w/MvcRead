using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using DefaultConnection;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class DataService:IDataService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 分页查询资料
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic Get(int curreent, int perPage,string queryString)
        {
            var sql = String.Format(@"SELECT     bu_data.DataSeq, bu_data.DataName, bu_data.CaseReason, bu_data.suspect, bu_data.source, bu_data.totals, bu_data.DataURL, bu_data.DataSize, bu_data.DataDescription, bu_data.CreateDate, 
                      sys_user.RealName, bu_userDataMap.ID, bu_userDataMap.DataID, bu_userDataMap.UserID, bu_userDataMap.IsEnable, sys_user.UserName
FROM         bu_data INNER JOIN
                      bu_userDataMap ON bu_data.DataID = bu_userDataMap.DataID INNER JOIN
                      sys_user ON bu_userDataMap.UserID = sys_user.UserID
              where (bu_userDataMap.IsEnable=1 and DataSeq like '%" + queryString + "%') or  ( bu_userDataMap.IsEnable=1 and DataName like '%" + queryString + "%' )  ORDER BY bu_userDataMap.ID DESC ");
            var result = db.Page<Data>(curreent, perPage, sql);
            return result;
        }
        /// <summary>
        /// 修改资料信息
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public dynamic ModifyData(bu_datum datum)
        {
            try
            {
                db.BeginTransaction();

                var sql = String.Format(@"update bu_data set  DataName=@0,  DataDescription=@1,CaseReason=@2,suspect=@3,source=@4,totals=@5   where DataID=@6           ");
                var result = db.Execute(sql, datum.DataName,datum.DataDescription,datum.CaseReason,datum.suspect,datum.source,datum.totals, datum.DataID);

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
        /// 删除资料信息
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public dynamic DeleteData(bu_userDataMap userDataMap)
        {
            try
            {
                db.BeginTransaction();

                var sql = String.Format(@"update bu_userDataMap set  IsEnable=0   where ID=@0      ");
                var result = db.Execute(sql, userDataMap.ID);

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
        /// 添加审核码纪录
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public int AddApply(bu_apply apply)
        {
            try
            {
                db.BeginTransaction();

               int t=(int)db.Insert(apply);
               db.CompleteTransaction();
               
                return t;

            }
            catch (Exception)
            {

                return -1;
            }

            return -1;
        }
        /// <summary>
        /// 分页查询申请日志
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        public dynamic GetApply(int curreent, int perPage)
        {
            var sql = String.Format(@"SELECT     bu_data.DataSeq, bu_data.DataName, bu_apply.ApplyID, bu_apply.DataID, bu_apply.ApplyName, bu_apply.Depart,
 bu_apply.GenerateCode, CONVERT(varchar(20),bu_apply.StartTime,120) as StartTime ,CONVERT(varchar(20),bu_apply.EndTime,120) as EndTime  FROM     bu_apply INNER JOIN  bu_data ON bu_apply.DataID = bu_data.DataID  order by     ApplyID desc  ");
            var result = db.Page<Data>(curreent, perPage, sql);
            return result;
        }

        //根据id  查询
        public dynamic GetById(int id)
        {
            var sql = String.Format(@"SELECT     bu_apply.ApplyID, bu_data.DataSeq, bu_apply.GenerateCode FROM         bu_apply INNER JOIN
                      bu_data ON bu_apply.DataID = bu_data.DataID where bu_apply.ApplyID=@0   ");
            var result = db.Query<Data>(sql, id);
            return result;
        }

        /// <summary>
        /// 根据id判断
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public dynamic GetByAppID(int id)
        {
            var sql = String.Format(@"SELECT     COUNT(*)
FROM         bu_apply   where ApplyID=@0 and StartTime<=GETDATE() and GETDATE()<=EndTime
    ");
            var result = db.Query<int>(sql, id).ToList();
            return result;
        }

        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        public dynamic Uploaded(bu_datum datum,int userId)
        {
            try
            {
                db.BeginTransaction();

             
                int result = (int)db.Insert(datum);
                var sql = String.Format(@"insert bu_userDataMap(DataID,UserID,IsEnable) values(@0,@1,1) ");
                db.Execute(sql, result, userId);
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
        /// 根据时间 审核码 编号
        /// </summary>
        /// <returns></returns>
        public dynamic GetBySeq(string seq, string gene)
        {
            var sql = String.Format(@"SELECT     bu_data.DataID, bu_data.DataSeq, bu_data.DataName, bu_data.DataURL, bu_data.DataSize, bu_data.CreateDate, bu_apply.ApplyID, bu_apply.StartTime, bu_apply.EndTime
FROM         bu_apply INNER JOIN
                      bu_data ON bu_apply.DataID = bu_data.DataID
     where  bu_data.DataSeq=@0 and StartTime<=GETDATE() and GETDATE()<=EndTime and GenerateCode=@1 ");
            var result = db.Query<Data>(sql,seq,gene);
            return result;
        }
    }
    public class Data
    {
        public int DataID { get; set; }
        public string DataSeq { get; set; }
        public string DataName { get; set; }
        public string DataURL { get; set; }
        public string suspect { get; set; }
        public string source { get; set; }
        public string CaseReason { get; set; }
        public int CaseReasonId { get; set; }
        public int CaseReasonId_Paent { get; set; }
        public int totals { get; set; }
        public string DataSize { get; set; }
        public string DataDescription { get; set; }
        public DateTime? CreateDate { get; set; }
        public int ID { get; set; }
        public int? UserID { get; set; }
        public bool? IsEnable { get; set; }
        public string RealName { get; set; }
        public string UserName { get; set; }
        public int ApplyID { get; set; }
        public string ApplyName { get; set; }
        public string Depart { get; set; }
        public string GenerateCode { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}