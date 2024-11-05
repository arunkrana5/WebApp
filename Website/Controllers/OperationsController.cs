using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class OperationsController : Controller
    {

        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IDealerHelper _dealerHelper;
        public OperationsController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            _dealerHelper = new DealerModal();
        }
        public ActionResult MyStoreShufflingList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _MyStoreShufflingList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = _dealerHelper.GetDealerChangeRequestList(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }


        public ActionResult StoreShufflingList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _StoreShufflingList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = _dealerHelper.GetDealerChangeRequestList_All(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }


        public ActionResult _StoreShufflingAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChangeID = GetQueryString[2];
            long ChangeID = 0;
            long.TryParse(ViewBag.ChangeID, out ChangeID);
            DealerChangeRequest.Add result = new DealerChangeRequest.Add();
            getResponse.ID = ChangeID;
            result = _dealerHelper.GetDealerChangeRequest(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _StoreShufflingAdd(string src, DealerChangeRequest.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChangeID = GetQueryString[2];
            long ChangeID = 0;
            long.TryParse(ViewBag.ChangeID, out ChangeID);
            Result.SuccessMessage = "Masters Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ChangeID = ChangeID;
                Result = _dealerHelper.fnSetDealerChangeRequest(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult _StoreShufflingApprove(string src, DealerChangeRequest.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approved = 0;
            int.TryParse(Command, out Approved);
            if (!string.IsNullOrEmpty(Modal.ChangeIDs))
            {
                Modal.Approved = Approved;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ChangeIDs = Modal.ChangeIDs.TrimEnd(',');
                Result = _dealerHelper.fnSetDealerChangeRequest_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _AddTerminateRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            TerminateRequest.Add Modal = new TerminateRequest.Add();
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Modal.EMPID = EMPID;
            if (ClsApplicationSetting.GetSessionValue("RoleName").ToLower()=="tl")
            {
                GetDropDownResponse getRes = new GetDropDownResponse();
                getRes.Doctype = "LinkedEmployees_ForTermination";
                getRes.LoginID = LoginID;
                Modal.EmployeeList= Common_SPU.GetDropDownList(getRes);
                return PartialView("_AddTerminateRequest_TL", Modal);
            }
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _AddTerminateRequest(TerminateRequest.Add Modal)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Action not taken";
            string Role = ClsApplicationSetting.GetSessionValue("RoleName").ToLower();
            DateTime dtLastWorkingDay;
            DateTime.TryParse(Modal.LastWorkingDay,out dtLastWorkingDay);
            if (ModelState.IsValid)
            {
                if (Role == "ssr")
                {
                    if ((dtLastWorkingDay.Date - DateTime.Now).Days < 15 && !Modal.SalaryDeductionConfirmation)
                    {
                        Result.SuccessMessage = "Please check the checkbox";
                        Result.StatusCode = -1;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                    Modal.Category = "Resignation";
                    if(((dtLastWorkingDay.Date - DateTime.Now).Days >= 15))
                    {
                        Modal.IsNPCompleted = "Yes";
                       
                    }
                    else
                    {
                        Modal.IsNPCompleted = "No";
                    }
                    
                    
                }
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = _dealerHelper.fnSetTerminationRequest(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TerminateRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            getResponse.Doctype = ViewBag.TableName;
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _TerminateRequestList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = _dealerHelper.GetTerminationRequestList(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult MyTerminateRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            getResponse.Doctype = ViewBag.TableName;
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _MyTerminateRequestList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = _dealerHelper.GetTerminationRequest_MyList(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }
        [HttpPost]
        public ActionResult _TerminateRequestApprove(string src, TerminateRequest.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approved = 0;
            int.TryParse(Command, out Approved);
            if (!string.IsNullOrEmpty(Modal.TerminateIDs))
            {
                Modal.Approved = Approved;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.TerminateIDs = Modal.TerminateIDs.TrimEnd(',');
                Result = _dealerHelper.fnSetTerminationRequest_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }

    
}