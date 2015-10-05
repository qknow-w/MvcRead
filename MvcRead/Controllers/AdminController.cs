using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DefaultConnection;
using MvcRead.Areas.Sys.Models;
using MvcRead.Factoty;
using MvcRead.Filter;
using MvcRead.IFactory;
using SS.Utilities.IP;
using SS.Utilities.PDF;

namespace MvcRead.Controllers
{
   
    public class AdminController : Controller
    {
        
        //
        // GET: /Admin/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        [LogFilter(Target = "登陆")]
        [AuthorizeExAttribute]
        public ActionResult Index()
        {

            ICaseReasonFactory caseReasonFactory = new CaseReasonFactory();
            //  ViewBag.CaseReason = caseReasonFactory.CreatCaseReason().Get().Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });
            ViewBag.CaseReason =
                 caseReasonFactory.CreatCaseReason()
                     .Get()
                     .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString() + "first", Text = a.CaseReasonName });

            ViewBag.CaseReasonn =
                  caseReasonFactory.CreatCaseReason()
                      .GetById(61162)
                      .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>        
        [HttpPost]     
        public dynamic LoginIn(sys_user user)
        {
            IUserFactoty userFactoty=new UserFactoty();
            List<sys_user> loginUser = userFactoty.CreatUserService().LoginIn(user);
            if (loginUser.Count>0)
            {

                Session["UserName"] = user.UserName;
                Session["UserID"] = loginUser[0].UserID;
                return "/admin/index";
            }

            return "/admin/error"; ;
        }
        [AuthorizeExAttribute]
        public ActionResult Apply()
        {
            return View();
        }
        [AuthorizeExAttribute]
        public ActionResult AddData()
        { 

            //下拉罪名
          //  ICaseReasonFactory caseReasonFactory=new CaseReasonFactory();
          ////  ViewBag.CaseReason = caseReasonFactory.CreatCaseReason().Get().Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });
          //  ViewBag.CaseReason =
          //       caseReasonFactory.CreatCaseReason()
          //           .Get()
          //           .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString() + "first", Text = a.CaseReasonName });

          //  ViewBag.CaseReasonn =
          //        caseReasonFactory.CreatCaseReason()
          //            .GetById(61162)
          //            .Select(a => new SelectListItem() { Value = a.CaseReasonId.ToString(), Text = a.CaseReasonName });
            
            return View();
        }
        public ActionResult Error()
        {
            Session.RemoveAll();
            return View();
        }
        /// <summary>
        /// 退出
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {
              ILogFactory logFactory=new LogFactory();
                sys_log log = new sys_log()
                {
                    IP = GetIp.getIp(),
                    UserName = Session["UserName"].ToString(),
                    Target = "退出",
                    Date = DateTime.Now
                };
               logFactory.CreatLogService().AddLog(log);
               Session.RemoveAll();
               return RedirectToAction("index", "Home");
           
        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="datum"></param>
        /// <param name="model"></param>
        /// <param name="fileUpload"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public dynamic Uploaded(bu_datum datum, AnyModel model, List<HttpPostedFileBase> fileUpload)
        {
            //int Resid = BllBuilder.BuilRes().DataAdd(modelRe, Num, modelCase, modelSuspect);
            try
            {
                IDataFactoty dataFactoty = new DataFactoty();
                foreach (HttpPostedFileBase item in fileUpload)
                {

                    if (item != null && Array.Exists(model.FilesToBeUploaded.Split(','), s => s.Equals(item.FileName)))
                    {
                        // string path = AppDomain.CurrentDomain.BaseDirectory + "Picture/";
                        // string filename = Path.GetFileName(Request.Files[upload].FileName);
                        string dt =
                            (DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.Hour +
                             Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10)).Substring(0, 15);
                           

                        item.SaveAs(Server.MapPath(Path.Combine("~/File/Pdf", dt+".pdf")));
                        datum.CreateDate = DateTime.Now;
                        datum.DataURL = "/File/Pdfs/" + dt ;
                        datum.DataSize = item.ContentLength.ToString();
                        datum.DataSeq = dt;
                        int useid = (int) Session["UserID"];                      
                        dataFactoty.CreatDataService().Uploaded(datum, useid);
                       
                        
                       // PDFSetWaterMark.setWatermark(Server.MapPath(Path.Combine("~/File/Pdf", dt + ".pdf")), Server.MapPath(Path.Combine("~/File/Pdfs", dt + ".pdf")), "咸宁市人民检察院卷宗");
                        //System.IO.File.Delete(Server.MapPath(Path.Combine("~/File/Pdf", dt + ".pdf")));

                        PDFSetWaterMark.PDFStamp(Server.MapPath(Path.Combine("~/File/Pdf", dt + ".pdf")), Server.MapPath(Path.Combine("~/File/Pdfs", dt + ".pdf")), Server.MapPath("~/File/WarterImade/water.png"));
                        System.IO.File.Delete(Server.MapPath(Path.Combine("~/File/Pdf", dt + ".pdf")));
                        
                        
                        //ww_Photo myPhoto = new ww_Photo()
                        //{
                        //    BoolDel = false,
                        //    ResID = Resid,
                        //    PhotoURL = "/Picture/" + dt + item.FileName,
                        //};
                        //BllBuilder.BuilPhoto().Upload(myPhoto);
                        // item.SaveAs(Server.MapPath("~/Picture/NewFolder1"));
                        //Save or do your action -  Each Attachment ( HttpPostedFileBase item ) 
                    }
                }
                return Redirect("/admin/index");
            }
            catch (Exception)
            {
                
                throw;
            }
            return Redirect("/admin/index");
            //return "KO";
        }


        /// <summary>
        /// 审核码页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Look(int id)
        {
            ViewBag.dataid = id;
            return View();
        }

       
    }
}
