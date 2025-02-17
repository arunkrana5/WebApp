using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginOnly]
    public class HomeController : Controller
    {
        long LoginID = 0;
        long RoleID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IHomeHelper Home;
        IMasterHelper _master;
        public HomeController()
        {

            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("RoleID"), out RoleID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            getResponse.RoleID = RoleID;
            Home = new HomeModal();
            _master = new MasterModal();

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

        public ActionResult NewDashboard()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            string RoleName = ClsApplicationSetting.GetSessionValue("RoleName");

            if (string.IsNullOrEmpty(RoleName))
            {
                return View();
            }
            else if (RoleName.ToUpper() == "TL")
            {
                return View("Dashboard_TL");
            }
            else if (RoleName.ToUpper() == "SSR")
            {
                return View("Dashboard_SSR");
            }
            else if (!string.IsNullOrEmpty(RoleName))
            {
                GetDropDownResponse respdrop = new GetDropDownResponse();
                respdrop.Doctype = "SalesGraphFilter";
                var dropdownData = Common_SPU.GetDropDownList(respdrop);
                ViewBag.RegionList = dropdownData.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.ID.ToString()
                }).ToList();
                ViewBag.InputDate = DateTime.Now.ToString("yyyy-MM");
                return View("Dashboard_Common");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult _DashboardCount()
        {
            return PartialView(Common_SPU.GetDashboard_Headers(getResponse));
        }

        public ActionResult _SSRListToday(string Doctype)
        {
            getResponse.Doctype = Doctype;
            return PartialView(Common_SPU.GetSSRListToday(getResponse));
        }
        public ActionResult _Banners(string Doctype)
        {
            getResponse.Doctype = "Banner";
            var result = _master.GetBannerList(getResponse);
            return PartialView(result);
        }

        public ActionResult _Marquee(string Doctype)
        {
            getResponse.Doctype = "Marquee";
            var result = _master.GetBannerList(getResponse);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _DailySales(GetResponse Modal)
        {

            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Common_SPU.GetSales_DateWise(Modal));
        }
        [OutputCache(Duration = 0)]
        public ActionResult MarkAttendence()
        {
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            MarkAttendence reult = new MarkAttendence();
            getResponse.Doctype = "Attendence";
            reult.AttendenceStatusList = Common_SPU.GetAttendenceStatus(getResponse);
            return PartialView(reult);
            //List<FlagsMismatchReason.Add> FlagList = new List<FlagsMismatchReason.Add>();
            //if (reult.Stop_InPunch)
            //{
            //    FlagsMismatchReason.Add FlagModal = new FlagsMismatchReason.Add();
            //    FlagModal.Date = DateTime.Now.AddDays(-1).ToString("dd-MMM-yyyy");
            //    FlagModal.Doctype = "SiteOutMissing";
            //    FlagModal.EMPID = EMPID;
            //    FlagModal.LoginID = LoginID;
            //    FlagModal.IPAddress = IPAddress;
            //    FlagList.Add(FlagModal);
            //}
            //if (reult.Stop_OutPunch)
            //{
            //    FlagsMismatchReason.Add FlagModal = new FlagsMismatchReason.Add();
            //    FlagModal.Date = DateTime.Now.ToString("dd-MMM-yyyy");
            //    FlagModal.Doctype = "NoSale";
            //    FlagModal.EMPID = EMPID;
            //    FlagModal.LoginID = LoginID;
            //    FlagModal.IPAddress = IPAddress;
            //    FlagList.Add(FlagModal);
            //}
            //if (FlagList.Count > 0)
            //{
            //    return PartialView("_FlagMismatchedReason", FlagList);
            //}
            //else
            //{
            //    return PartialView(reult);
            //}
        }



        [HttpPost]

        public ActionResult MarkAttendence(MarkAttendence Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("SSREntry");
            Result.SuccessMessage = "Attendence Can't Update";
            if (!string.IsNullOrEmpty(Modal.Flag_Doctype) && string.IsNullOrEmpty(Modal.Flag_Reason.Trim()))
            {
                Result.SuccessMessage = Modal.Flag_Doctype + " Reason is mandiatory";
                ModelState.AddModelError("Flag_Reason", Result.SuccessMessage);
            }
            else if ((Modal.StatusID == 1 || Modal.StatusID == 2) && string.IsNullOrEmpty(Modal.ImageBase64String))
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("ImageBase64String", Result.SuccessMessage);
            }

            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = EMPID;
                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "SSR";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Common_SPU.fnSetAttendenceLog(Modal);
                if (Result.Status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "Attendence marked Successfully";
                    ClsApplicationSetting.SetSessionValue("AttendenceStatus", Result.AdditionalMessage);
                }


            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        //[HttpPost]
        //public ActionResult _FlagMismatchedReason(List<FlagsMismatchReason.Add> Modal, string Command)
        //{
        //    PostResponse Result = new PostResponse();
        //    long EMPID = 0;
        //    long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
        //    if (ModelState.IsValid)
        //    {
        //        foreach (var item in Modal)
        //        {
        //            item.LoginID = LoginID;
        //            item.IPAddress = IPAddress;
        //            item.EMPID = EMPID;
        //            Result = Common_SPU.fnSetEMP_Flags_Mismatch_Reason(item);

        //        }
        //        if (Result.Status)
        //        {
        //            TempData["Success"] = "Y";
        //            TempData["SuccessMsg"] = Result.SuccessMessage;
        //        }
        //    }
        //    return Json(Result, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult MyProfile()
        {
            Profile Results = new Profile();
            Results = Common_SPU.GetProfile(getResponse);
            return View(Results);

        }

        [HttpPost]
        public ActionResult SaveProfileImageJson(Profile Modal)
        {
            PostResponse PostResult = new PostResponse();
            if (!string.IsNullOrEmpty(Modal.ImageBase64String))
            {
                FileResponse attachModal = new FileResponse();
                attachModal.ImageBase64String = Modal.ImageBase64String;
                attachModal.LoginID = LoginID;
                attachModal.IPAddress = IPAddress;
                attachModal.Doctype = "profilepic";
                PostResult = ClsApplicationSetting.UploadCameraImage(attachModal);
                ClsApplicationSetting.SetSessionValue("ImageURL", PostResult.SuccessMessage);

            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _ContactUs()
        {
            ContactUs_Query.AddQuery Modal = new ContactUs_Query.AddQuery();
            Modal.Name = ClsApplicationSetting.GetSessionValue("EMPName");
            Modal.Phone = ClsApplicationSetting.GetSessionValue("Phone");
            Modal.Email = ClsApplicationSetting.GetSessionValue("Email");

            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _ContactUs(ContactUs_Query.AddQuery Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Can't Update";

            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Common_SPU.fnSetContactUs_Query(Modal);

            }
            TempData["Success"] = (Result.Status ? "Y" : "N");
            TempData["SuccessMsg"] = Result.SuccessMessage;
            return RedirectToAction("Dashboard", "Home");

        }

        public ActionResult ChangePassword()
        {
            ChangePassword Results = new ChangePassword();
            return View(Results);

        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Can't Update";
            if (Modal.ConfirmPassword != Modal.NewPassword)
            {
                Result.SuccessMessage = "Confirm Password and New Password must be matched";
                ModelState.AddModelError("NewPassword", Result.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Common_SPU.fnSetChangePassword(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Home/Dashboard";
            }
            return Json(Result, JsonRequestBehavior.AllowGet);


        }



        [HttpPost]
        public ActionResult _TargetAchieved_MonthWise(GetResponse tabmodal)
        {
            tabmodal.LoginID = LoginID;
            tabmodal.IPAddress = IPAddress;
            DateTime dt;
            DateTime.TryParse(tabmodal.Date, out dt);
            tabmodal.Date = dt.ToString("dd-MMM-yyyy");
            return PartialView(Home.GetTargetAchieved_MonthWise(tabmodal));
        }


        public ActionResult TakePhoto()
        {
            return View();
        }

        [HttpPost]
        public ActionResult _PunchTime_DateWise(GetResponse tabmodal)
        {
            tabmodal.LoginID = LoginID;
            tabmodal.IPAddress = IPAddress;
            return PartialView(Home.GetPunchTime_DateWise(tabmodal));
        }

        public ActionResult SalarySlip()
        {
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        [HttpPost]
        public ActionResult SalarySlip(Tab.Approval Modal)
        {
            DateTime dt;
            DateTime.TryParse(Modal.Month, out dt);
            string EMPCode = ClsApplicationSetting.GetSessionValue("EMPCode");
            string basePathFromSQL = ClsApplicationSetting.GetConfigValue("SalarySlipPhysicalPath").TrimEnd('/');
            string dynamicPath = dt.ToString("MMMyyyy") + "/" + EMPCode + "_SalarySlip.pdf";
            try
            {
                var PhysicalPath = Path.Combine(basePathFromSQL, dynamicPath).Replace("\\", "/");
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

        public ActionResult _MyAnnouncementList()
        {
            PostResponse PostResult = new PostResponse();

            List<Announcement.My> Modal = new List<Announcement.My>();
            Modal = Home.GetMyAnnouncement(getResponse);
            if (Modal.Count > 0)
            {
                PostResult.Status = true;
                PostResult.ViewAsString = RenderRazorViewToString("_MyAnnouncementList", Modal);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult YTDGraph()
        {
            GetDropDownResponse getRes = new GetDropDownResponse();
            getRes.LoginID = LoginID;
            getRes.Doctype = "FinancialYearList";
            ViewBag.FinancialYearList = Common_SPU.GetDropDownList(getRes);
            getRes.Doctype = "MyRegionList";
            ViewBag.RegionList = Common_SPU.GetDropDownList(getRes);
            getRes.Doctype = "MyBranchList";
            ViewBag.BranchList = Common_SPU.GetDropDownList(getRes);
            return View();
        }

        public ActionResult _BirthdayList()
        {
            PostResponse PostResult = new PostResponse();

            List<BirthdayList> Modal = new List<BirthdayList>();
            Modal = Home.GetBirthdayList(getResponse);
            if (Modal.Count > 0)
            {
                PostResult.Status = true;
                PostResult.ViewAsString = RenderRazorViewToString("_BirthdayList", Modal);
            }
            return Json(PostResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyDoc()
        {
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);

            getResponse.ID = EMPID;

            var DocumentList = Common_SPU.GetEmployeeDocuments(getResponse);

            return View(DocumentList);
        }

        public ActionResult ReleaseLog()
        {
            return View();
        }
        [HttpPost]
        public ActionResult _ProductSalesGraph(GetGraphResponse getGraphResponse)
        { 
            getGraphResponse.Doctype = "ProductSalesGraph";
            getGraphResponse.LoginID = LoginID;
            var result = _master.GetSalesGraphData(getGraphResponse); 
            ViewBag.ProductSalesData = JsonConvert.SerializeObject(result.ProductSalesList); 
            return PartialView();
        }

        [HttpPost]
        public ActionResult _SalesGrowthGraph(GetGraphResponse getGraphResponse)
        {
            getGraphResponse.Doctype = "SalesGrowthData";
            getGraphResponse.LoginID = LoginID;
            var result = _master.GetSalesGrowthData(getGraphResponse);
            ViewBag.SalesGrowthData = JsonConvert.SerializeObject(result.SalesGrowthList);
            return PartialView();
        }

        [HttpPost]
        public ActionResult _BranchWiseSalesGraph(GetGraphResponse getGraphResponse)
        {
            getGraphResponse.Doctype = "BranchWiseSales";
            getGraphResponse.LoginID = LoginID;
            var result = _master.GetBranchWiseSales(getGraphResponse);
            ViewBag.BranchWiseSalesData = JsonConvert.SerializeObject(result.BranchWiseSalesList);
            return PartialView();
        }
        [HttpPost]
        public ActionResult _ISDCountGraph(GetGraphResponse getGraphResponse)
        {
            getGraphResponse.Doctype = "ISDCountGraph";
            getGraphResponse.LoginID = LoginID;
            var result = _master.GetISDCountGraph(getGraphResponse);
            ViewBag.ISDCountGraphData = JsonConvert.SerializeObject(result.ISDCountGraphList);
            return PartialView();
        }
        public JsonResult GetRegionWiseBranchListJson(GetDropDownResponse Modal)
        {
            List<DropDownlist> Result = new List<DropDownlist>();
            Modal.LoginID = LoginID;
            Result = Common_SPU.GetDropDownList(Modal);
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}