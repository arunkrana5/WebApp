using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System.Web.Mvc;
using Website.CommonClass;


namespace Website.Controllers
{
    [CheckLoginFilter]
    public class RecuritController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IRecuritHelper recur;
        public RecuritController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            recur = new RecuritModal();
        }

        public ActionResult _BranchWiseEMP_Dashboard(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return PartialView(recur.GetBranchWiseEMP_Max_Active(getResponse));
        }

        public ActionResult MyRequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MyRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(recur.GetRequirement_MyRequest(Modal));
        }

        public ActionResult _AddRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            Requirement.AddRequest result = new Requirement.AddRequest();
            result = recur.GetRequirement_Request(getResponse);
            return PartialView(result);
        }


        [HttpPost]
        public ActionResult _AddRequest(string src, Requirement.AddRequest Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Result.SuccessMessage = "Request Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ReqID = ReqID;
                Result = recur.fnSetRequirement_Request(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _RequestApplicationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return PartialView(recur.GetRequirement_ApplicationList(getResponse));
        }
        public ActionResult AddApplication(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Requirement.Application.Add modal = new Requirement.Application.Add();
            modal.ReqID = ReqID;
            return View(modal);
        }

        [HttpPost]
        public ActionResult AddApplication(string src, Requirement.Application.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ReqID = ReqID;
                Result = recur.fnSetRequirement_Application(Modal);
                if (Result.Status && Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.TableName = "recruit";
                    attachModal.tableid = Result.ID;
                    attachModal.Doctype = "recruit";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
               
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllRequestsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AllRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(recur.GetRequirement_RequestList(Modal));
        }
        public ActionResult ViewCompleteRequirement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return View(recur.GetRequirement_FullView(getResponse));
        }

        public ActionResult ApprovedApplication(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return View(recur.GetRequirement_FullView(getResponse));
        }

        [HttpPost]
        public ActionResult ApprovedApplication(string src, Requirement.FullView Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            int finalSubmit = 0;
            Result.SuccessMessage = "Please select Application!";
            if (string.IsNullOrEmpty(Modal.ApprovedRemarks) && Command == "FinalSubmit")
            {
                Result.SuccessMessage = "Remarks can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (Modal.ApplicationList != null)
            {
                int value = 0;
                if (Command == "FinalSubmit")
                {
                    value = 2;
                }
                finalSubmit = (Command == "FinalSubmit" ? 1 : 0);
                foreach (var item in Modal.ApplicationList)
                {
                    var appro = (!string.IsNullOrEmpty(item.IsChecked) ? 1 : value);
                    Result = recur.fnSetRequirementApplication_Approved(ReqID, item.ApplicationID, appro, item.ApprovedRemarks, Modal.ApprovedRemarks, finalSubmit, LoginID, IPAddress);
                    if (!Result.Status)
                    {
                        Result.SuccessMessage = Result.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            //if (!string.IsNullOrEmpty(Modal.IDs))
            //{
            //    Modal.LoginID = LoginID;
            //    Modal.IPAddress = IPAddress;
            //    Modal.IDs = Modal.IDs.TrimEnd(',');
            //    Result = recur.fnSetRequirementApplication_Approved(Modal);
            //    if (Result.Status)
            //    {
            //        Result.RedirectURL = "/Recurit/MyRequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Recurit/MyRequestsList");
            //    }
            //}
            Result.RedirectURL = "/Recurit/MyRequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Recurit/MyRequestsList");
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _RequestStatus(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ReqID = GetQueryString[2];
            long ReqID = 0;
            long.TryParse(ViewBag.ReqID, out ReqID);
            getResponse.ID = ReqID;
            return PartialView(recur.GetRequirement_FullView(getResponse));
        }
    }
}