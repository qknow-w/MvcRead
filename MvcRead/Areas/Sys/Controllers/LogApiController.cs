using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;

namespace MvcRead.Areas.Sys.Controllers
{
    [AuthorizeExAttribute]
    public class LogApiController:ApiController
    {
        private readonly ILogFactory logFactory;
        public LogApiController() 
        {
            logFactory=new LogFactory();
        }
        /// <summary>
        /// 分页查询日志
        /// </summary>
        /// <param name="curreent"></param>
        /// <param name="perPage"></param>
        /// <returns></returns>
       [LogFilter(Target = "查看日志")]
        public dynamic GetLog(int curreent = 1, int perPage = 10)
        {
            return logFactory.CreatLogService().GetLog(curreent, perPage);
        }

    }
}