using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ApprovalsController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IApprovalsHelper approval;
        ITravelHelper Travel;
        IReportHelper report;
        public ApprovalsController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            approval = new ApprovalsModal();
            report = new ReportModal();
            Travel = new TravelModal();

        }
        public string RenderRazorViewToString(string viewName, object model)
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
        public ActionResult RFCApprovalsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");

            GetDropDownResponse getRes = new GetDropDownResponse();
            getRes.Doctype = "EMPList_Linked";
            getRes.LoginID = LoginID;
            ViewBag.EMPList = Common_SPU.GetDropDownList(getRes);
            return View(Modal);
        }

        public ActionResult _RFCApprovalsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            getResponse.Approved = Modal.Approved;
            ViewBag.Approved = Modal.Approved;
            List<RFCEntry.List> result = new List<RFCEntry.List>();
            Modal.LoginID = LoginID;
            result = approval.GetRFCApprovalList(Modal);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult ApproveRFC(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            int Approved = 0;
            int.TryParse(Command, out Approved);
            string RFCIDs = form["RFCIDs"].ToString();
            if (!string.IsNullOrEmpty(RFCIDs))
            {
                RFCEntry.Action Modal = new RFCEntry.Action();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.RFCIDs = RFCIDs.TrimEnd(',');
                Result = approval.fnSetRFCApproved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult SaleApprovalList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _SaleApprovalList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<SaleEntry.ApprovalList> Result = new List<SaleEntry.ApprovalList>();
            Modal.LoginID = LoginID;
            Result = approval.GetSaleEntryApprovalList(Modal);
            return PartialView(Result);
        }


        [HttpPost]
        public ActionResult SaleApprove(string src, SaleEntry.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (!string.IsNullOrEmpty(Modal.SaleEntryIDs))
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.SaleEntryIDs = Modal.SaleEntryIDs.TrimEnd(',');
                Result = approval.fnSetSaleEntry_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult AttendenceApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _AttendenceApproval(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            ViewBag.Date = Modal.Month;
            ViewBag.Approved = Modal.Approved;
            Modal.LoginID = LoginID;
            return PartialView(report.GetAttendance(Modal));
        }
        [HttpPost]
        public ActionResult AttendenceApprove(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dt;
            int Approved = 0;
            int.TryParse(Command, out Approved);
            string EMPIDs = form["EMPIDs"].ToString();
            string Date = form["hdnDate"].ToString();
            DateTime.TryParse(Date, out dt);
            if (!string.IsNullOrEmpty(EMPIDs))
            {
                ApprovalAction Modal = new ApprovalAction();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.dt = dt;
                Modal.IDs = EMPIDs.TrimEnd(',');
                Result = approval.fnSetAttendenceApproved(Modal);
                if (Result.Status)
                {
                    // AttendenceApprovedMail(Modal);
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public string AttendenceApprovedMail(ApprovalAction Modal)
        {
            string result = "";
            int ApprovedCount = 0;
            try
            {
                if (!string.IsNullOrEmpty(Modal.IDs))
                {
                    ApprovedCount = 1;
                    if (Modal.IDs.Contains(','))
                    {
                        ApprovedCount = Modal.IDs.Split(',').Count();
                    }
                }
                getResponse.Doctype = "AttendanceApproval";
                getResponse.LoginID = LoginID;
                var smtpMail = Common_SPU.GetMail_Data(getResponse);

                Tab.Approval GetHTML = new Tab.Approval();
                GetHTML.SeperatedIDs = Modal.IDs;
                GetHTML.Approved = Modal.Approved;
                GetHTML.Month = Modal.dt.ToString("dd-MMM-yyyy");
                GetHTML.LoginID = LoginID;
                GetHTML.IPAddress = IPAddress;
                string Attendance_HTML = "";
                ViewBag.Date = Modal.dt.ToString("dd-MMM-yyyy");
                Attendance_HTML = RenderRazorViewToString("_Attendance_HTML", report.GetAttendance_Final(GetHTML));


                Tab.Approval getModal;
                foreach (var UD in smtpMail.AutoReportUsersList)
                {
                    getResponse.Doctype = "NSM";
                    getResponse.LoginID = UD.LoginID;
                    var AllNSM = Common_SPU.GetLinkedHierarchy_Users(getResponse);
                    if (AllNSM != null)
                    {
                        foreach (var item in AllNSM)
                        {
                            smtpMail.CC += item.EmailID + ";";
                        }
                        if (!string.IsNullOrEmpty(smtpMail.CC))
                        {
                            smtpMail.CC = smtpMail.CC.TrimEnd(';');
                        }
                    }
                    getModal = new Tab.Approval();
                    getModal.Month = Modal.dt.ToString("dd-MMM-yyyy");
                    getModal.LoginID = Modal.LoginID;
                    getModal.IPAddress = Modal.IPAddress;
                    // Send Mail
                    if (!string.IsNullOrEmpty(UD.EmailID))
                    {
                        // Send Mail
                        smtpMail.MailBody = smtpMail.TemplateData.Body;
                        smtpMail.Subject = smtpMail.TemplateData.Subject;
                        if (string.IsNullOrEmpty(smtpMail.CC))
                        {
                            smtpMail.CC = smtpMail.TemplateData.CCMail;
                        }
                        else
                        {
                            smtpMail.CC += ";" + smtpMail.TemplateData.CCMail;
                        }
                        smtpMail.BCC = smtpMail.TemplateData.BCCMail;
                        smtpMail.ToEmail = UD.EmailID;
                        smtpMail.Subject = smtpMail.Subject.Replace("#DATE#", Modal.dt.ToString("MMM-yyyyy"));
                        smtpMail.Subject = smtpMail.Subject.Replace("#APPROVEDEMPCOUNT#", ApprovedCount.ToString());
                        smtpMail.Subject = smtpMail.Subject.Replace("#APPROVEDSTATUS#", (Modal.Approved == 1 ? "Approved" : "Rejected"));
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#DATE#", Modal.dt.ToString("MMM-yyyyy"));
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#USERNAME#", UD.UserName);
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#APPROVEDEMPCOUNT#", ApprovedCount.ToString());
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#APPROVEDSTATUS#", (Modal.Approved == 1 ? "Approved" : "Rejected"));
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#Attendance_HTML#", Attendance_HTML);
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#WEBSITEURL#", smtpMail.TemplateData.ConfigSettingList.Where(x => x.ConfigKey == "WebsiteURL").Select(x => x.ConfigValue).FirstOrDefault());
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#WEBSITELOGOPATH#", smtpMail.TemplateData.ConfigSettingList.Where(x => x.ConfigKey == "WebsiteLogPath").Select(x => x.ConfigValue).FirstOrDefault());
                        MailConfigration.SendMail(smtpMail);

                    }
                }
            }
            catch (Exception ex)
            {
                result = "Fail";
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "AttendenceApprovedMail", "AttendenceApprovedMail", "Approvals", 0, "");
            }

            result = "Success";
            return result;
        }


        public ActionResult PJPExpenseApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _PJPExpenseApproval(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(approval.GetPJPEntry_Expense_ApprovalList(Modal));
        }
        [HttpPost]
        public ActionResult ApproveExpensePJP(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            DateTime dt;
            int Approved = 0;
            int.TryParse(Command, out Approved);
            string PJPEntryIDs = form["PJPEntryIDs"].ToString();
            if (!string.IsNullOrEmpty(PJPEntryIDs))
            {
                ApprovalAction Modal = new ApprovalAction();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = PJPEntryIDs.TrimEnd(',');
                Result = approval.fnSetPJPEntry_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult PJPPlanApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _PJPPlanApprovalList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<PJPPlan.ApprovalList> Result = new List<PJPPlan.ApprovalList>();
            Modal.LoginID = LoginID;
            Modal.RoleID = Convert.ToInt32(ClsApplicationSetting.GetSessionValue("RoleID"));
            Result = approval.GetPJPPlanApprovalList(Modal);
            return PartialView(Result);
        }


        [HttpPost]
        public ActionResult PJPPlanApprove(string src, PJPPlan.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (!string.IsNullOrEmpty(Modal.PJPIDs))
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PJPIDs = Modal.PJPIDs.TrimEnd(',');
                Result = approval.fnSetPJPPlan_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PJPExpApproval(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _PJPExpApprovalList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<PJPExpenses.List> Result = new List<PJPExpenses.List>();
            Modal.LoginID = LoginID;
            Modal.RoleID = Convert.ToInt32(ClsApplicationSetting.GetSessionValue("RoleID"));
            Result = approval.GetPJPExpApprovalList(Modal);
            return PartialView(Result);
        }


        [HttpPost]
        public ActionResult PJPExpApprove(string src, PJPExpenses.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            if (!string.IsNullOrEmpty(Modal.PJPExpIDs))
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PJPExpIDs = Modal.PJPExpIDs.TrimEnd(',');
                Result = approval.fnSetPJPExp_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}