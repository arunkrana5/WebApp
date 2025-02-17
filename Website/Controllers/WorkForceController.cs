using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mail;
using System.Web.Mvc;
using Website.CommonClass;
using static DataModal.Models.DailySummary;

namespace Website.Controllers
{

    [CheckLoginFilter]
    public class WorkForceController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IWorkForceHelper workforce;

        public WorkForceController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            workforce = new WorkForceModal();
        }

        public ActionResult RequestForm(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.WorkForceID = GetQueryString[2];
            WorkForce.Add result = new WorkForce.Add();
            long WorkForceID = 0;
            long.TryParse(ViewBag.WorkForceID, out WorkForceID);
            getResponse.ID = WorkForceID;
            result = workforce.GetWorkForce(getResponse);

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "WFDocumentsType";
            ViewBag.WFDocumentsType = Common_SPU.GetDropDownList(getDropDownResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult RequestForm(string src, WorkForce.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.WorkForceID = GetQueryString[2];
            long WorkForceID;
            long.TryParse(ViewBag.WorkForceID, out WorkForceID);
            Result.SuccessMessage = "Request Can't Update";
            if (ModelState.IsValid)
            {
                Modal.WorkForceID = (int)WorkForceID;
                Modal.IPAddress = IPAddress;
                Modal.CreatedBy = (int)LoginID;
                Result = workforce.fnSetWorkForceRequest(Modal);
                WorkForceID = Result.ID;
                if (Modal.WFDocumentList != null)
                {
                    if (Modal.WFDocumentList.Count > 0)
                    {
                        foreach (var item in Modal.WFDocumentList)
                        {
                            if (item.upload != null)
                            {
                                UploadAttachment attachModal = new UploadAttachment();
                                attachModal.FixFileName = item.FileName;
                                attachModal.Description = item.Description;
                                attachModal.File = item.upload;
                                attachModal.LoginID = LoginID;
                                attachModal.IPAddress = IPAddress;
                                attachModal.AttachID = item.Attach_ID;
                                attachModal.Doctype = item.FileName;
                                attachModal.TableName = "WorkForce";
                                attachModal.tableid = WorkForceID;
                                var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                                item.Attach_ID = Attach.ID;

                                Result.ID = Attach.ID;
                                Result.SuccessMessage = Attach.SuccessMessage;
                                Result.Status = Attach.Status;
                                Result.StatusCode = Attach.StatusCode;
                                if (!Attach.Status)
                                {
                                    Result.SuccessMessage = Attach.SuccessMessage;
                                    return Json(Result, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/WorkForce/RequestList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/WorkForce/RequestList");
                Send_WorkForceMail(ClsCommon.Encrypt("-2*/WorkForce/Send_WorkForceMail*" + getResponse.LoginID + "*" + WorkForceID + "*ORF"));
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult RequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _RequestList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = GetQueryString[2];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = workforce.GetWorkForceRequestList(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public ActionResult WFApproval(string src, WorkForce.ApprovalAction Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (Modal.WorkForceID > 0)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = workforce.fnSetWorkForce_Approved(Modal);
                if (Result.Status)
                {
                    Send_WorkForceMail(ClsCommon.Encrypt("-2*/WorkForce/Send_WorkForceMail*" + LoginID + "*" + Modal.WorkForceID + "*ORF"));
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Send_WorkForceMail(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long workForceId = 0;
            long.TryParse(GetQueryString[3], out workForceId);
            string docType = GetQueryString[4];
            PostResponse result = new PostResponse();
            try
            {
                // Retrieve workforce details
                var requestModel = new Tab.Approval { LoginID = LoginID, ID = workForceId, Doctype = docType };
                var modal = workforce.GetWorkForce_View(requestModel);

                if (modal == null)
                {
                    result.Status = false;
                    result.SuccessMessage = "No workforce data found";
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                modal.mail.MailBody = modal.maildetails.MailBody;
                modal.mail.Subject = modal.maildetails.Subject;

                // Fetch configuration values
                var websiteUrl = ClsApplicationSetting.GetConfigValue("WebsiteURL");
                var websiteLogoPath = ClsApplicationSetting.GetConfigValue("WebsiteLogPath");

                // Render email template
                string workForceHtml = RenderRazorViewToString("_WorkForceMail", modal);
                //workForceHtml=Regex.Replace(workForceHtml, @"\s+", "").Trim(',');

                // Replace placeholders in mail body
                modal.mail.MailBody = modal.mail.MailBody
                    .Replace("#WEBSITEURL#", websiteUrl)
                    .Replace("#WEBSITELOGOPATH#", websiteLogoPath)
                    .Replace("#FORM#", workForceHtml);
                modal.mail.Subject = modal.mail.Subject.Replace("#DOCNO#", modal.request.DocNo);

                int highestApprovedLevel = modal.approvals?.Any() == true ? modal.approvals.Max(x => x.ApprovedLevel) : 0;

                // Determine recipient and CC list
                if (modal.request.Approved == 1)
                {
                    modal.mail.ToEmail = modal.request.CreatedByMail;
                    modal.mail.MailBody = modal.mail.MailBody.Replace("#USERNAME#", modal.request.CreatedBy);
                    modal.mail.CC = string.Join(";", modal.approvalsMasters.Select(x => x.Email).Distinct().Where(email => email != modal.mail.ToEmail));
                    result = MailConfigration.SendMail(modal.mail);
                }
                else if (modal.request.Approved == 2 || modal.request.Approved == 3)
                {
                    modal.mail.ToEmail = modal.request.CreatedByMail;
                    modal.mail.MailBody = modal.mail.MailBody.Replace("#USERNAME#", modal.request.CreatedBy);
                    modal.mail.CC = string.Join(";", modal.approvalsMasters.Where(x => x.ApprovedLevel <= highestApprovedLevel).Select(x => x.Email).Distinct().Where(email => email != modal.mail.ToEmail));
                    result = MailConfigration.SendMail(modal.mail);
                }
                else if (modal.approvals == null || modal.approvals.Count == 0)
                {
                    foreach (var approver in modal.approvalsMasters.Where(x => x.ApprovedLevel == 1))
                    {
                        result = SendApprovalMail(modal, approver, websiteUrl, workForceId);
                    }
                }
                else
                {
                    var topApprover = modal.approvals.OrderByDescending(x => x.ApprovalID).FirstOrDefault();
                    int nextApprovalLevel = (topApprover?.Approved == 1) ? Math.Min(topApprover.ApprovedLevel + 1, 4) : topApprover?.ApprovedLevel ?? 1;

                    foreach (var approver in modal.approvalsMasters.Where(x => x.ApprovedLevel == nextApprovalLevel))
                    {
                        result = SendApprovalMail(modal, approver, websiteUrl, workForceId);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.SuccessMessage = "Fail";
                Common_SPU.LogError(ex.Message, ex.ToString(), nameof(Send_WorkForceMail), nameof(Send_WorkForceMail), nameof(Send_WorkForceMail), 0, "");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private PostResponse SendApprovalMail(WorkForce_View modal, WorkForce_ApprovalsMaster approver, string websiteUrl, long workForceId)
        {
            PostResponse result = new PostResponse();
            modal.mail.ToEmail = approver.Email;
            modal.mail.CC = modal.request.CreatedByMail;
            modal.mail.MailBody = modal.mail.MailBody.Replace("#USERNAME#", approver.Name);

            string actionUrl = websiteUrl + "MailAction/Approval?src=" + ClsCommon.Encrypt($"0*/MailAction/Approval*{approver.LoginID}*{workForceId}*ORF");
            modal.mail.MailBody = modal.mail.MailBody.Replace("#ActionBtn#", actionUrl);

            result = MailConfigration.SendMail(modal.mail);
            return result;
        }

        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}