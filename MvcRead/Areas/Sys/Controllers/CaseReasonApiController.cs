using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MvcRead.Factoty;
using MvcRead.IFactory;

namespace MvcRead.Areas.Sys.Controllers
{
    public class CaseReasonApiController : ApiController
    {
        //
        // GET: /Sys/CaseReasonApi/
        private readonly ICaseReasonFactory caseReasonFactory;
        public CaseReasonApiController()
        {
            caseReasonFactory=new CaseReasonFactory();
        }
        public dynamic GetData()
        {
            return caseReasonFactory.CreatCaseReason().Get();
        }
        public dynamic GetDataByID(int id)
        {
            return caseReasonFactory.CreatCaseReason().GetById(id);
        }

    }
}
