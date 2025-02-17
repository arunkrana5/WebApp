using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    public class MailActionController : Controller
    {
        public ActionResult Approval(string src)
        {
            MailActionApproval modal = new MailActionApproval();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            modal.LoginID = Convert.ToInt32(GetQueryString[2]);
            modal.ID = Convert.ToInt32(GetQueryString[3]);
            modal.Doctype = GetQueryString[4];
            return View(modal);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approval(string src, MailActionApproval Modal)
        {
            IWorkForceHelper workforce = new WorkForceModal();
            PostResponse Result = new PostResponse();
            WorkForce.ApprovalAction approvalAction = new WorkForce.ApprovalAction();
            if (Modal.ID > 0)
            {
                approvalAction.LoginID = Modal.LoginID;
                approvalAction.WorkForceID = Modal.ID;
                approvalAction.Approved = Modal.Approved;
                approvalAction.ApprovedRemarks = Modal.Remarks;
                approvalAction.IPAddress = ClsApplicationSetting.GetIPAddress();
                Result = workforce.fnSetWorkForce_Approved(approvalAction);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/MailAction/Thanks";
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Thanks()
        {
            return View();
        }

    }
}