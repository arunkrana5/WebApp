using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [ValidateToken]
    public class SchedularController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        IExportHelper export;
        ISchedularHelper schedular;
        GetResponse getResponse;
        IReportHelper report;
        public SchedularController()
        {
            getResponse = new GetResponse();
            export = new ExportModal();
            schedular = new SchedularModal();
            report = new ReportModal();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
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
        public string DailyReports(string Date)
        {
            string result = "Fail";
            try
            {
                if (!string.IsNullOrEmpty(Date))
                {
                    DateTime dt = DateTime.Now;
                    if (!string.IsNullOrEmpty(Date))
                    {
                        DateTime.TryParse(Date, out dt);
                    }

                    foreach (var item in Common_SPU.GetAutoMail_UsersList())
                    {
                        Send_Report(item.LoginID, dt.ToString("dd-MMM-yyyy"));
                    }
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "DailyReports", "DailyReports", "DataModal", 0, "");
            }

            result = "Success";
            return result;
        }



        public ActionResult Send_Report(long LoginID, string Date)
        {
            PostResponse result = new PostResponse();

            SMTPMail smtpMail = new SMTPMail();
            try
            {

                string AllAttachment = "";
                int Port = 0; bool EnableSsl;
                string Region_HTML = "", Branch_HTML = "";

                DateTime dt = DateTime.Now;
                if (!string.IsNullOrEmpty(Date))
                {
                    DateTime.TryParse(Date, out dt);
                }

                Tab.Approval GetModal = new Tab.Approval();
                GetModal.LoginID = LoginID;
                GetModal.Month = dt.ToString("dd-MMM-yyyy");
                DataSet Data = Common_SPU.GetAuto_ReportData(GetModal);

                if (Data != null)
                {

                    int.TryParse(Data.Tables[0].Rows[0]["Port"].ToString(), out Port);
                    bool.TryParse(Data.Tables[0].Rows[0]["EnableSsl"].ToString(), out EnableSsl);
                    smtpMail.Port = Port;
                    smtpMail.EnableSsl = EnableSsl;
                    smtpMail.SMTP = Data.Tables[0].Rows[0]["SMTP"].ToString();
                    smtpMail.SMTP_USER = Data.Tables[0].Rows[0]["SMTP_USER"].ToString();
                    smtpMail.SMTP_PASSWORD = Data.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                    smtpMail.SMTP_EMAIL = Data.Tables[0].Rows[0]["SMTP_EMAIL"].ToString();

                    UserDetails Usermodal = new UserDetails();
                    // getting and Update User Details into modal
                    Usermodal.EmailID = Data.Tables[1].Rows[0]["EmailID"].ToString();
                    Usermodal.LoginID = LoginID;
                    Usermodal.UserID = Data.Tables[1].Rows[0]["UserID"].ToString();
                    Usermodal.UserName = Data.Tables[1].Rows[0]["UserName"].ToString();
                    Usermodal.UserType = Data.Tables[1].Rows[0]["roleName"].ToString();

                    // getting and Update Email Template into modal
                    smtpMail.MailBody = Data.Tables[2].Rows[0]["Body"].ToString();
                    smtpMail.Subject = Data.Tables[2].Rows[0]["Subject"].ToString();
                    smtpMail.CC = Data.Tables[2].Rows[0]["CCMail"].ToString();
                    smtpMail.BCC = Data.Tables[2].Rows[0]["BCCMail"].ToString();
                    smtpMail.ToEmail = Usermodal.EmailID;

                    if (Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "kaff")
                    {
                        string TradeHTML = RenderRazorViewToString("_TradeWiseData", Common_SPU.GetTradeWiseData(GetModal));
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#BRANCHTABLE#", TradeHTML);
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#REGIONTABLE#", "");
                        smtpMail.MailBody = smtpMail.MailBody.Replace("Region wise Summary", "");
                        smtpMail.MailBody = smtpMail.MailBody.Replace("Branch wise Summary", "");
                    }
                    else
                    {
                        var aa = report.GetDailyReportSummaryData(GetModal);
                        if (Usermodal.UserType != "BSM")
                        {
                            Region_HTML = RenderRazorViewToString("_SummaryReport_RegionWise", aa);
                        }
                        Branch_HTML = RenderRazorViewToString("_SummaryReport_BranchWise", aa);
                        smtpMail.MailBody = smtpMail.MailBody.Replace("#REGIONTABLE#", Region_HTML);
                        if (Region_HTML == "")
                        {
                            smtpMail.MailBody = smtpMail.MailBody.Replace("Region wise Summary", "");
                        }

                        smtpMail.MailBody = smtpMail.MailBody.Replace("#BRANCHTABLE#", Branch_HTML);
                    }

                    string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + Usermodal.UserType);
                    IWorkbook workbook = new XSSFWorkbook();

                    if (Data.Tables.Count > 5)
                    {
                        if (Data.Tables[3] != null && Data.Tables[3].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[3], "Daily Attendance");

                        }
                        if (Data.Tables[4] != null && Data.Tables[4].Rows.Count > 0)
                        {

                            export.GetWorkbookSheet(workbook, Data.Tables[4], "Daily Sales");
                            //AllAttachment += ";" + SaveDataTable_Workbook(Data.Tables[4], Usermodal, "Daily Sales");
                        }
                        if (Data.Tables[5] != null && Data.Tables[5].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[5], "Trgt Vs Achv");
                            //AllAttachment += ";" + SaveDataTable_Workbook(Data.Tables[5], Usermodal, "Trgt Vs Achv");
                        }
                    }
                    if (Data.Tables.Count > 6 && Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "ogeneral")
                    {
                        if (Data.Tables[6] != null && Data.Tables[6].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[6], "Competition");

                            //AllAttachment += ";" + SaveDataTable_Workbook(Data.Tables[6], Usermodal, "Competition");
                        }
                    }
                    if (Data.Tables.Count > 7 && Usermodal.UserType == "NSM" && Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "blue star")
                    {
                        if (Data.Tables[7] != null && Data.Tables[7].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet_Shorted(workbook, Data.Tables[7], "TL Tracker");
                        }
                    }
                    if (Data.Tables.Count > 8 && Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "blue star")
                    {
                        if (Data.Tables[8] != null && Data.Tables[8].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet_Shorted(workbook, Data.Tables[8], "PJP Entry Reports");
                        }
                    }
                    //new pjp plan

                    if (Data.Tables.Count > 9 && Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "blue star")
                    {
                        if (Data.Tables[9] != null && Data.Tables[9].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet_Shorted(workbook, Data.Tables[9], "PJP Plan Report");
                        }
                    }

                    if (Data.Tables.Count > 10 && Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "blue star")
                    {
                        if (Data.Tables[10] != null && Data.Tables[10].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[10], "Team Mapping Report");
                        }
                    }

                    if (Data.Tables[0].Rows[0]["CompanyCode"].ToString().ToLower() == "ogeneral" && Usermodal.UserType == "NSM")
                    {
                        var saleSummary = report.spu_GetSalesSummary(GetModal);
                        if (saleSummary != null && saleSummary.Tables.Count > 0)
                        {
                            if (saleSummary.Tables[0] != null)
                                export.GetWorkbookSheet(workbook, saleSummary.Tables[0], "ISD Sales Summary Product Wise");

                            if (saleSummary.Tables[1] != null)
                                export.GetWorkbookSheet(workbook, saleSummary.Tables[1], "ISD Sales Summary Region Wise");
                        }
                    }

                    string FileName = "AutoReports_" + Usermodal.UserID.Replace(" ", "_") + ".xlsx";
                    if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
                    {
                        System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
                    }
                    string FilePath = Path.Combine(PhysicalPath, FileName);
                    using (var stream = System.IO.File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        workbook.Write(stream);
                        stream.Close();
                    }
                    smtpMail.AttachmenPath = FilePath;
                    result = MailConfigration.SendMail(smtpMail);
                    Common_SPU.fnSetAutoReport_Log(LoginID, dt.ToString("dd-MMM-yyyy"), Usermodal.EmailID, result.SuccessMessage, (result.Status ? 1 : 0), result.StatusCode.ToString(), getResponse.LoginID, getResponse.IPAddress);
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.SuccessMessage = "Fail";
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "Send_Report", "Send_Report", "Send_Report", 0, "");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ISDSummary_Report(string Date)
        {
            PostResponse result = new PostResponse();

            SMTPMail smtpMail = new SMTPMail();
            try
            {
                DateTime dt = DateTime.Now;
                if (!string.IsNullOrEmpty(Date))
                {
                    DateTime.TryParse(Date, out dt);
                }
                string AllAttachment = "";
                int Port = 0; bool EnableSsl;

                GetResponse getResponse = new GetResponse();
                getResponse.Date = dt.ToString("dd-MMM-yyyy");
                DataSet Data = Common_SPU.GetISDSummaryForMail(getResponse);

                if (Data != null)
                {

                    int.TryParse(Data.Tables[0].Rows[0]["Port"].ToString(), out Port);
                    bool.TryParse(Data.Tables[0].Rows[0]["EnableSsl"].ToString(), out EnableSsl);
                    smtpMail.Port = Port;
                    smtpMail.EnableSsl = EnableSsl;
                    smtpMail.SMTP = Data.Tables[0].Rows[0]["SMTP"].ToString();
                    smtpMail.SMTP_USER = Data.Tables[0].Rows[0]["SMTP_USER"].ToString();
                    smtpMail.SMTP_PASSWORD = Data.Tables[0].Rows[0]["SMTP_PASSWORD"].ToString();
                    smtpMail.SMTP_EMAIL = Data.Tables[0].Rows[0]["SMTP_EMAIL"].ToString();



                    // getting and Update Email Template into modal
                    smtpMail.MailBody = Data.Tables[2].Rows[0]["Body"].ToString();
                    smtpMail.Subject = Data.Tables[2].Rows[0]["Subject"].ToString();
                    smtpMail.CC = Data.Tables[2].Rows[0]["CCMail"].ToString();
                    smtpMail.BCC = Data.Tables[2].Rows[0]["BCCMail"].ToString();

                    string FileName = "FNAME_ISD_BSL_DAILY_FROM_" + dt.ToString("yyyyMMdd") + "_TO_" + dt.ToString("yyyyMMdd") + "_EXTTS_" + dt.ToString("yyyyMMddhhmmss") + ".xlsx";
                    IWorkbook workbook = new XSSFWorkbook();
                    if (Data.Tables.Count > 2)
                    {
                        if (Data.Tables[3] != null && Data.Tables[3].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[3], "Daily Attendance");

                        }
                        if (Data.Tables[4] != null && Data.Tables[4].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[4], "Daily Sales");
                        }
                        if (Data.Tables[5] != null && Data.Tables[5].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[5], "Employee Master");
                        }
                        if (Data.Tables[6] != null && Data.Tables[6].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[6], "Dealer Master");
                        }
                        if (Data.Tables[7] != null && Data.Tables[7].Rows.Count > 0)
                        {
                            export.GetWorkbookSheet(workbook, Data.Tables[7], "Employees Target");
                        }
                    }
                    string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("ISDSummaryReports");
                    string[] files = Directory.GetFiles(PhysicalPath);
                    foreach (string file in files)
                    {
                        System.IO.File.Delete(file);
                    }
                    string FilePath = Path.Combine(PhysicalPath, FileName);
                    using (var stream = System.IO.File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        workbook.Write(stream);
                        stream.Close();
                    }

                    smtpMail.AttachmenPath = FilePath;
                    foreach (DataRow item in Data.Tables[1].Rows)
                    {
                        smtpMail.ToEmail = item["EmailID"].ToString();
                        result = MailConfigration.SendMail(smtpMail);
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = false;
                result.SuccessMessage = "Fail";
                Common_SPU.LogError(ex.Message.ToString(), ex.ToString(), "ISDSummaryReports", "ISDSummaryReports", "ISDSummaryReports", 0, "");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public string SaveDataTable_Workbook(DataTable dt, UserDetails Modal, string Doctype)
        {
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + Modal.UserType);

            var workbook = export.GetDataTable_Workbook(dt, Doctype);
            string FileName = Doctype + "_" + Modal.UserID.Replace(" ", "_") + ".xlsx";
            if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
            {
                System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
            }
            string FilePath = Path.Combine(PhysicalPath, FileName);


            using (var stream = System.IO.File.Open(FilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                workbook.Write(stream);
                stream.Close();
            }
            return FilePath;
        }
        public string Attendence(Tab.Approval getModal, AutoReportUsers UD)
        {
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + UD.UserType);
            var workbook = export.GetAttendance_Workbook(getModal);
            string FileName = "FA_" + UD.UserID.Replace(" ", "_") + ".xlsx";

            if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
            {
                System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
            }
            string FilePath = Path.Combine(PhysicalPath, FileName);

            FileStream sw = System.IO.File.Create(FilePath);
            workbook.Write(sw);
            sw.Close();
            return FilePath;

        }

        public string SaleEntry(Tab.Approval getModal, AutoReportUsers UD)
        {
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + UD.UserType);
            var workbook = export.GetSaleEntry_Workbook(getModal);

            string FileName = "DailySales_" + UD.UserID.Replace(" ", "_") + ".xlsx";

            if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
            {
                System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
            }
            string FilePath = Path.Combine(PhysicalPath, FileName);

            FileStream sw = System.IO.File.Create(FilePath);
            workbook.Write(sw);
            sw.Close();
            return FilePath;

        }

        public string MTD(Tab.Approval getModal, AutoReportUsers UD)
        {

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + UD.UserType);
            var workbook = export.GetMTDReport_Workbook(getModal);

            string FileName = "MTD_" + UD.UserID.Replace(" ", "_") + ".xlsx";

            if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
            {
                System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
            }
            string FilePath = Path.Combine(PhysicalPath, FileName);

            FileStream sw = System.IO.File.Create(FilePath);
            workbook.Write(sw);
            sw.Close();

            return FilePath;

        }
        public string TargetVsAchievement(Tab.Approval getModal, AutoReportUsers UD)
        {

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("auto" + UD.UserType);
            var workbook = export.GetTargetVsAchievement_Workbook(getModal);

            string FileName = "TvsA_" + UD.UserID.Replace(" ", "_") + ".xlsx";

            if (System.IO.File.Exists(Path.Combine(PhysicalPath, FileName)))
            {
                System.IO.File.Delete(Path.Combine(PhysicalPath, FileName));
            }
            string FilePath = Path.Combine(PhysicalPath, FileName);
            FileStream sw = System.IO.File.Create(FilePath);
            workbook.Write(sw);
            sw.Close();
            return FilePath;

        }


        public ActionResult DownloadSalarySlip(string EMPCode, string Date)
        {
            DateTime dt;
            DateTime.TryParse(Date, out dt);
            string PhysicalPath = ClsApplicationSetting.GetConfigValue("SalarySlipPhysicalPath") + dt.ToString("MMMyyyy");
            string DocName = EMPCode + "_SalarySlip.pdf";

            try
            {
                PhysicalPath = Path.Combine(PhysicalPath, DocName);
                if (System.IO.File.Exists(PhysicalPath))
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(PhysicalPath);
                    return File(bytes, "application/octet-stream", EMPCode + ".pdf");
                }
            }
            catch (FileNotFoundException)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return new HttpStatusCodeResult(HttpStatusCode.NotFound);

        }

       
    }
}