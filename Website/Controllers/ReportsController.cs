using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ReportsController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IReportHelper report;
        ITransactionHelper Transaction;
        IApprovalsHelper approval;
        public ReportsController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            report = new ReportModal();
            Transaction = new TransactionModal();
            approval = new ApprovalsModal();

        }


        public ActionResult AttendanceLog_Day(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDFExport = "True";
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM-dd");

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);

            return View(result);
        }


        [HttpPost]
        public ActionResult _AttendanceLog_Day(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;

            return PartialView(report.GetAttendance_Log_Daily(Modal));
        }

        public ActionResult AttendanceLog_Monthly(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");
            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(result);

        }
        [HttpPost]
        public ActionResult _AttendanceLog_Monthly(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = "Monthly";
            ViewBag.Date = Modal.Month;
            return PartialView(report.GetAttendance_Log_MonthlyList(Modal));
        }


        public ActionResult AttendanceLog_Monthly_InOut(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(result);

        }
        [HttpPost]
        public ActionResult _AttendanceLog_Monthly_InOut(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Date = Modal.Month;
            return PartialView(report.GetAttendance_Log_Monthly_InOutList(Modal));
        }


        public ActionResult Attendance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(result);

        }
        [HttpPost]
        public ActionResult _Attendance(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Date = Modal.Month;
            ViewBag.Approved = Modal.Approved;
            return PartialView(report.GetAttendance(Modal));
        }

        public ActionResult Attendance_Final(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "RoleList_Filter";
            ViewBag.RoleDropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(result);

        }
        [HttpPost]
        public ActionResult _Attendance_Final(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Date = Modal.Month;
            ViewBag.Approved = Modal.Approved;
            return PartialView(report.GetAttendance_Final(Modal));
        }
        public ActionResult _UpdateFinalAttendence(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Date = GetQueryString[3];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            Tab.Approval Modal = new Tab.Approval();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.SeperatedIDs = EMPID.ToString();
            Modal.Month = ViewBag.Date;
            DataSet ds = report.GetAttendance_EMPWise(Modal);
            return PartialView(ds);
        }
        [HttpPost]
        public ActionResult _UpdateFinalAttendence(string src, FormCollection Form)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "can't updated";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.Date = GetQueryString[3];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            string StatusID = "0", hdnDate = "";
            StatusID = Form.GetValue("StatusID").AttemptedValue;
            hdnDate = Form.GetValue("hdnDate").AttemptedValue;
            if (string.IsNullOrEmpty(StatusID))
            {
                Result.SuccessMessage = "Data can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("StatusID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("EMPID", typeof(int)));
            if (StatusID.Contains(","))
            {
                for (int i = 0; i < StatusID.Split(',').Length; i++)
                {
                    DataRow dr = dataTable.NewRow();
                    dr["Date"] = hdnDate.Split(',')[i];
                    dr["StatusID"] = StatusID.Split(',')[i];
                    dr["EMPID"] = EMPID.ToString();
                    dataTable.Rows.Add(dr);
                }
            }
            else
            {
                DataRow dr = dataTable.NewRow();
                dr["Date"] = hdnDate;
                dr["StatusID"] = StatusID;
                dr["EMPID"] = EMPID.ToString();
                dataTable.Rows.Add(dr);
            }
            if (dataTable.Rows.Count > 0)
            {
                Result = report.fnSetUpdateAttendance(getResponse, dataTable);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }




        public ActionResult SalesEntryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _SalesEntryReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetSaleEntryReport(Modal));
        }


        public ActionResult CounterDisplayReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _CounterDisplayReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CounterDisplay.Report> Result = new List<CounterDisplay.Report>();
            Modal.LoginID = LoginID;
            Result = report.GetCounterDisplayReport(Modal);
            return PartialView(Result);
        }

        public ActionResult CompetitionEntryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _CompetitionEntryReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView("_DataSetBind", Common_SPU.GetCompetitionEntry_Report(Modal));

        }

        public ActionResult MOPReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _MOPReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MOP.Report> reult = new List<MOP.Report>();
            Modal.LoginID = LoginID;
            reult = report.GetMOPReportList(Modal);
            return PartialView(reult);
        }



        public ActionResult RFCRequestReport(string src)
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

        public ActionResult _RFCRequestReport(string src, Tab.Approval Modal)
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





        public ActionResult MTDReportList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");
            return View(result);
        }
        [HttpPost]
        public ActionResult _MTDReportList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            DateTime date;
            DateTime.TryParse(Modal.Month, out date);
            ViewBag.date = date;
            return PartialView(report.GetMTD_Report(Modal));
        }


        public ActionResult SSRMappingList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View(report.GetAllSSR_Hierarchy_Report(getResponse));
        }


        public ActionResult TAReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");
            return View(result);
        }
        [HttpPost]
        public ActionResult _TAReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            DateTime date;
            DateTime.TryParse(Modal.Month, out date);
            ViewBag.date = date;
            return PartialView(report.GetTargetVsAchievement_Report(Modal));
        }




        public ActionResult SummaryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _SummaryReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            DailySummary.Data result = report.GetDailyReportSummaryData(Modal);
            if (Modal.Approved == 1)
            {
                return PartialView("_SummaryReport_BranchWise", result);
            }
            else
            {
                return PartialView("_SummaryReport_RegionWise", result);
            }

        }


        public ActionResult PJPEntryReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _PJPEntryReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetPJPEntryReport(Modal));

        }

        public ActionResult CustomerList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _CustomerList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(Common_SPU.GetCustomerList(Modal));
        }

        public ActionResult SaleEntryWithCustomer(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.StartDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            Modal.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _SaleEntryWithCustomer(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetSaleEntry_WithCustomer(Modal));
        }

        public ActionResult PJPEntryExpense(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _PJPEntryExpense(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(Common_SPU.GetPJPEntry_Expense(Modal));
        }

        public ActionResult TLTrackerList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _TLTrackerList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Date = Modal.Month;
            return PartialView(report.GetTLTracker_Report(Modal));
        }

        public ActionResult CompetitionSummaryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        public ActionResult _CompetitionSummaryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(Common_SPU.GetCompetitionSummary_Report(Modal));
        }

        public ActionResult TravelExpenseList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        public ActionResult _TravelExpenseList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetTravel_Expenses_Report(Modal));
        }

        public ActionResult TravelVisitReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        public ActionResult _TravelVisitReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetTravel_Visit_Report(Modal));
        }
        public ActionResult MiscellaneousReports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "MiscellaneousReports";
            respdrop.LoginID = LoginID;
            respdrop.IPAddress = IPAddress;
            ViewBag.DropdownList = Common_SPU.GetDropDownList(respdrop);

            return View(result);
        }

        public ActionResult AbsentTracker(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _AbsentTracker(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetAbsentTracker(Modal));
        }
        public ActionResult IncentiveCalculator(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _IncentiveCalculator(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetEMP_Incentive(Modal));
        }

        public ActionResult DealerPerformance_Reports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _DealerPerformance_Reports(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView("_DataSetBind", report.GetDealerPerformance_Reports(Modal));
        }

        public ActionResult OnboardProHRMS_Reports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _OnboardProHRMS_Reports(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView("_DataSetBind", report.GetGetOnboardFor_ProHRMS(Modal));
        }
        public ActionResult EMPWiseMonthly(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");

            GetDropDownResponse respdrop = new GetDropDownResponse();
            respdrop.Doctype = "EMPList";
            respdrop.LoginID = LoginID;
            respdrop.IPAddress = IPAddress;
            ViewBag.DropdownList = Common_SPU.GetDropDownList(respdrop);
            return View(Modal);
        }
        public ActionResult _EMPWiseMonthly(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(report.GetAttendance_Log_Monthly_InOut_EMPWise(Modal));
        }
        public ActionResult AttendanceLog_Change(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDFExport = "True";
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _AttendanceLog_Change(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.FromDate = GetQueryString[2];
            ViewBag.ToDate = GetQueryString[3];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            param.OtherValue1 = ViewBag.FromDate;
            param.OtherValue2 = ViewBag.ToDate;
            var Result = report.GetAttendanceChangeLog(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult PJPReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDFExport = "True";
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _PJPReport(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Month = GetQueryString[2]; 
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            param.OtherValue1 = ViewBag.Month; 
            var Result = report.GetPJPReport(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult PJPReports(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PDFExport = "True";
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _PJPReports(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Month = GetQueryString[2];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            param.OtherValue1 = ViewBag.Month;
            var Result = report.GetPJPReports(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PJPEntriesReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _PJPEntriesReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(report.GetPJPEntriesReport(Modal));
        }

        public ActionResult PJPExpReport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        public ActionResult _PJPExpReport(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            ViewBag.Approved = Modal.Approved;
            return PartialView(Common_SPU.GetPJPEntries_Expense(Modal));
        }
    }
}