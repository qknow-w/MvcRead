using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using DefaultConnection;
using MvcRead.Areas.Sys.Models;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;
using SS.Utilities.Random;

namespace MvcRead.Areas.Sys.Controllers
{
    [AuthorizeExAttribute]
    public class DataApiController:ApiController
    {
        private readonly IDataFactoty dataFactoty;

        public DataApiController()
        {
            dataFactoty = new DataFactoty();
        }

        /// <summary>
        /// 分页查询资料
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
        [LogFilter(Target = "查看卷宗")]
        public dynamic GetData(int curreent = 1, int perPage = 10, string queryString= "")
        {
            return dataFactoty.CreatDataService().Get(curreent, perPage, queryString);
        }
        /// <summary>
        /// 修改资料信息
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
         [LogFilter(Target = "卷宗管理")]
        [System.Web.Http.HttpPost]
        public dynamic ModifyData(bu_datum datum)
        {
            if (dataFactoty.CreatDataService().ModifyData(datum))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 删除资料信息
        /// </summary>
        /// <param name="datum"></param>
        /// <returns></returns>
        [LogFilter(Target = "删除资料")]
        [System.Web.Http.HttpPost]
        public dynamic DeleteData(bu_userDataMap userDataMap)
        {
            if (dataFactoty.CreatDataService().DeleteData(userDataMap))
            {
                return "OK";
            }
            return "FAIL";
        }
        /// <summary>
        /// 添加审核码纪录
        /// </summary>
        /// <param name="apply"></param>
        /// <returns></returns>
       [LogFilter(Target = "生成审核码")]
        [System.Web.Http.HttpPost]
        public dynamic AddApply(bu_apply apply)
        {
            apply.GenerateCode = BaseRandom.GetRandom().ToString();
            int s = dataFactoty.CreatDataService().AddApply(apply);
            if (s>0)
            {
                return s;
            }
            return -1;
        }
        /// <summary>
        /// 分页查询申请日志
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
       [LogFilter(Target = "查看申请日志")]
        public dynamic GetApply(int curreent = 1, int perPage = 10)
        {
            return dataFactoty.CreatDataService().GetApply(curreent, perPage);
        }
       //[LogFilter(Target = "上传资料")]
       //[HttpPost]
       // public dynamic Uploaded(bu_datum datum, AnyModel model, List<HttpPostedFileBase> fileUpload)
       //{
       //    //return dataFactoty.CreatDataService().GetApply(curreent, perPage);
       //    return "KO";
       //}
        /// <summary>
        /// 根据时间 审核码 编号
        /// </summary>
        /// <returns></returns>
       //[LogFilter(Target = "前台查询")]
        public dynamic GetBySeq(string numberr, string genree)
        {
            return dataFactoty.CreatDataService().GetBySeq(numberr.Trim(), genree.Trim());
        }
        public dynamic GetById(int id)
        {
            return dataFactoty.CreatDataService().GetById(id);
        }
        
    }
}