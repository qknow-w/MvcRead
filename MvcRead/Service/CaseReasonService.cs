using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DefaultConnection;
using MvcRead.IService;

namespace MvcRead.Service
{
    public class  CaseReasonService:ICaseReasonService
    {
        PetaPoco.Database db = new PetaPoco.Database("DefaultConnection");
        /// <summary>
        /// 查询出所有内容
        /// </summary>
        /// <returns></returns>
        public List<ww_CaseReason> Get()
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            List<ww_CaseReason> list = db.Query<ww_CaseReason>("select CaseReasonId ,CaseReasonName from ww_CaseReason where CaseReasonId_Paent=0 order by  CaseReasonId  ").ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }

        public List<ww_CaseReason> GetById(int id)
        {
            db.BeginTransaction();
            //IEnumerable<CaseJoinCabinet> list = db.Query<CaseJoinCabinet>("SELECT     ww_Case.*, ww_Cabinet.Num " +
            //                                                              "FROM      ww_Cabinet INNER JOIN ww_Res ON ww_Cabinet.CabinetIId = ww_Res.CabinetIId INNER JOIN " +
            //                                                              "ww_Case ON ww_Res.ResID = ww_Case.ResID  where ww_Case.ResID=@0", ResID).ToList();
            List<ww_CaseReason> list = db.Query<ww_CaseReason>("select CaseReasonId ,LTRIM(CaseReasonName) as CaseReasonName from ww_CaseReason where CaseReasonId_Paent=@0 order by  CaseReasonId  ", id).ToList();
            db.CompleteTransaction();
            //List<ww_Re>  i= list.ToList();
            return list;
        }
    }
}
