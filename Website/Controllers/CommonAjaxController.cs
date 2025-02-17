using DataModal.CommanClass;
using DataModal.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Website.Controllers
{

    public class CommonAjaxController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public CommonAjaxController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDateTimeJson()
        {
            string MyTime = DateTime.Now.ToString("dddd, dd-MMM-yyyy hh:mm:ss tt");
            return Json(MyTime, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult EncryptJSON(string Value)
        {
            return Json(ClsCommon.Encrypt(Value), JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult DecryptQueryStringJSON(string Value)
        {
            return Json(ClsApplicationSetting.DecryptQueryString(Value), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateColumn_CommonJson(GetUpdateColumnResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnGetUpdateColumnResponse(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDropDownListJson(GetDropDownResponse Modal)
        {
            List<DropDownlist> Result = new List<DropDownlist>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Common_SPU.GetDropDownList(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult DelRecordJson(GetResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            PostResult.SuccessMessage = "Action not saved";
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnDelRecord(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]

        public ActionResult IsSessionEndJSON(string ReturnURL)
        {
            PostResponse PostResult = new PostResponse();
            if (ClsApplicationSetting.IsSessionExpired("LoginID"))
            {
                if (!string.IsNullOrEmpty(ReturnURL))
                {
                    PostResult.RedirectURL = "/Accounts/Login?ReturnURL=" + ClsCommon.Encrypt(ReturnURL);
                }
                PostResult.Status = true;
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetCheckRecordExistJson(GetRecordExitsResponse Modal)
        {
            PostResponse PostResult = new PostResponse();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            PostResult = Common_SPU.fnGetCheckRecordExist(Modal);
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetCalenderEventsJson(GetResponse Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            List<Calender.Events> result = new List<Calender.Events>();
            result = Common_SPU.GetCalenderEvents(Modal);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetSales_GraphJson(GetResponse Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return Json(Common_SPU.GetSales_Graph(Modal), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetUserCurrent_LocationJson(UserCurrent_Location Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Modal.EMPID = EMPID;
            return Json(Common_SPU.fnSetUserCurrent_Location(Modal), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetClearDeviceJSON(GetResponse Modal)
        {
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return Json(Common_SPU.fnSetClearUserDevice(Modal), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDealerDetailsJson(long DealerID)
        {
            return Json(Common_SPU.GetDealerByID(DealerID), JsonRequestBehavior.AllowGet);
        }

        public ActionResult _MasterDetails(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            ViewBag.Doctype = GetQueryString[3];
            string Doctype = ViewBag.Doctype;
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            getResponse.Doctype = ViewBag.Doctype;
            var Result = Common_SPU.GetMasterDetails(getResponse);
            switch (Doctype.ToLower())
            {
                case "dealer":
                    return PartialView("_MasterDetails_Dealer", Result);
                case "employee":
                    return PartialView("_MasterDetails_EMP", Result);
                case "clientvisittran":
                    return PartialView("_MasterDetails_ClientVisitTran", Result);
                case "onboarding_applications":
                    return PartialView("_MasterDetails_Onboarding_Applications", Result);
                case "emptalentpool":
                    return PartialView("_MasterDetails_EMPTalentPool", Result);
                case "dealerchangerequest":
                    return PartialView("_MasterDetails_DealerChangeRequest", Result);
                case "workforce":
                    return PartialView("_MasterDetails_WorkForce", Result);
                default:
                    return PartialView(Result);
            }


        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDealerSearchJson(DealerSearch Modal)
        {
            List<DropDownlist> Result = new List<DropDownlist>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Common_SPU.GetDealerSearchList(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetDealerByRouteNumber(DealerSearchNew Modal)
        {
            List<DropDownlist> Result = new List<DropDownlist>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Common_SPU.GetDealerByRouteNumber(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetEMPTalentByDoc(string src)
        { 
            GetResponse Modal = new GetResponse();
            Requirement.Application.List Result = new Requirement.Application.List();
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.MenuID = GetQueryString[0];
            Modal.Doctype = GetQueryString[2];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Common_SPU.GetEMPTalentByDoc(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDealerDetailsByCodeJson(string DealerCode)
        {
            return Json(Common_SPU.GetDealerByCode(DealerCode), JsonRequestBehavior.AllowGet);
        }
    }
}