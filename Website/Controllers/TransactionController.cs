using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class TransactionController : Controller
    {
        long LoginID = 0, EMPID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        ITransactionHelper Transaction;
        public TransactionController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Transaction = new TransactionModal();
        }
        public ActionResult PJPPlanList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            ViewBag.Import = "True";
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _PJPPlanList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan.List> Result = new List<PJPPlan.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Transaction.GetPJPPlanList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddPJPPlan(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            PJPPlan.Add Modal = new PJPPlan.Add();
            long ID = 0;
            long.TryParse(ViewBag.PJPID, out ID);

            getResponse.ID = ID;
            Modal = Transaction.GetPJPPlan(getResponse);


            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult _AddPJPPlan(string src, PJPPlan.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            long PJPID = 0;
            long.TryParse(ViewBag.PJPID, out PJPID);
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            Result.SuccessMessage = "PJP Can't Update";
            ModelState.Remove("src");
            ModelState.Remove("DealerTypeID");
            ModelState.Remove("DealerCategoryID");
            ModelState.Remove("RegionID");
            ModelState.Remove("StateID");
            ModelState.Remove("CityID");
            ModelState.Remove("AreaID");
            ModelState.Remove("DealerID");
            ModelState.Remove("Command");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PJPID = PJPID;
                Modal.OnDemand = OnDemand;
                Modal.DealerID = Modal.NewDealerID;
                Result = Transaction.fnSetPJPPlan(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PJPPlanList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PJPPlanList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SaleEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            ViewBag.Import = "True";
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }

        [HttpPost]
        public ActionResult _SaleEntryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<SaleEntry.List> Result = new List<SaleEntry.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetSaleEntryList(Modal);
            return PartialView(Result);
        }
        public ActionResult SaleEntryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SaleEntryID = GetQueryString[2];
            long SaleEntryID = 0;
            long.TryParse(ViewBag.SaleEntryID, out SaleEntryID);
            getResponse.ID = SaleEntryID;
            SaleEntry.Add Modal = new SaleEntry.Add();
            Modal = Transaction.GetSaleEntry(getResponse);
            return View(Modal);
        }

        [HttpPost]
        public ActionResult SaleEntryAdd(string src, SaleEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.SaleEntryID = GetQueryString[2];
            string CompanyCode = ClsApplicationSetting.GetSessionValue("CompanyCode");
            long SaleEntryID = 0, EMPID = 0;
            long.TryParse(ViewBag.SaleEntryID, out SaleEntryID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Result.SuccessMessage = "Sale Entry Can't Update";

            if (CompanyCode.ToLower() != "rock" && (Modal.AttachmentID == 0 && Modal.Upload == null))
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("Upload", Result.SuccessMessage);
            }
            //if (Company.ToLower() == "daikin")
            //{
            //    DateTime invoiceDate;
            //    DateTime.TryParse(Modal.InvoiceDate, out invoiceDate);
            //    if (Modal.Price < 20000 || Modal.Price > 150000)
            //    {
            //        Result.SuccessMessage = "Price must between 20,000 to 1,50,000";
            //        return Json(Result, JsonRequestBehavior.AllowGet);
            //    }
            //    else if(invoiceDate.Month!= DateTime.Now)
            //    {

            //    }
            //}

            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.SaleEntryID = SaleEntryID;
                Modal.EMPID = EMPID;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "SSR";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Transaction.fnSetSaleEntry(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/SaleEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/SaleEntryList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult MOPList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _MOPList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MOP.List> Result = new List<MOP.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetMOPList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddMOP(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);

            MOP.Add Modal = new MOP.Add();
            getResponse.ID = MOPID;
            Modal = Transaction.GetMOP(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddMOP(string src, MOP.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);
            Result.SuccessMessage = "MOP Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.MOPID = MOPID;
                Modal.EMPID = EMPID;
                Result = Transaction.fnSetMOP(Modal);

            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CounterDisplayList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _CounterDisplayList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CounterDisplay.List> Result = new List<CounterDisplay.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetCounterDisplayList(Modal);
            return PartialView(Result);
        }
        public ActionResult _AddCounterDisplay(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MOPID = GetQueryString[2];
            long MOPID = 0;
            long.TryParse(ViewBag.MOPID, out MOPID);

            CounterDisplay.Add Modal = new CounterDisplay.Add();
            getResponse.ID = MOPID;
            Modal = Transaction.GetCounterDisplay(getResponse);
            return PartialView(Modal);
        }
        [HttpPost]
        public ActionResult _AddCounterDisplay(string src, CounterDisplay.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CounterID = GetQueryString[2];
            long CounterID = 0;
            long.TryParse(ViewBag.CounterID, out CounterID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("SSREntry");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.CounterID = CounterID;
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

                Result = Transaction.fnSetCounterDisplay(Modal);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RFCRequestsList(string src)
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
        public ActionResult _RFCRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<RFCEntry.List> result = new List<RFCEntry.List>();
            Modal.LoginID = LoginID;
            result = Transaction.GetRFCEntryList(Modal);
            return PartialView(result);

        }

        public ActionResult _AddRFCRequests(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RFCID = GetQueryString[2];
            int RFCID = 0;
            int.TryParse(ViewBag.RFCID, out RFCID);
            RFCEntry.Add Modal = new RFCEntry.Add();
            getResponse.ID = RFCID;
            Modal = Transaction.GetRFCEntry(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddRFCRequests(string src, RFCEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long EMPID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = EMPID;

                Result = Transaction.fnSetRFCEntry(Modal);

            }

            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult CompetitionEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _CompetitionEntryList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<CompetitionEntry.List> Result = new List<CompetitionEntry.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetCompetitionEntryList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddCompetitionEntry(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompetitionID = GetQueryString[2];
            long CompetitionID = 0;
            long.TryParse(ViewBag.CompetitionID, out CompetitionID);
            CompetitionEntry.Add Modal = new CompetitionEntry.Add();
            getResponse.ID = CompetitionID;
            Modal = Transaction.GetCompetitionEntry(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddCompetitionEntry(string src, CompetitionEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompetitionID = GetQueryString[2];
            long CompetitionID = 0;
            long.TryParse(ViewBag.CompetitionID, out CompetitionID);
            string CompanyCode = ClsApplicationSetting.GetSessionValue("CompanyCode");
            Result.SuccessMessage = "Can't Update";

            if (CompanyCode == "Ogeneral")
            {
                if ((Modal.BrandID ?? 0) == 0)
                {
                    Result.SuccessMessage = "Brand is mandiatory";
                    ModelState.AddModelError("BrandID", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.Category))
                {
                    Result.SuccessMessage = "Category is mandiatory";
                    ModelState.AddModelError("Category", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.SubCategory))
                {
                    Result.SuccessMessage = "Tonnage is mandiatory";
                    ModelState.AddModelError("SubCategory", Result.SuccessMessage);
                }
                else if (string.IsNullOrEmpty(Modal.ModalNo))
                {
                    Result.SuccessMessage = "Modal No is mandiatory";
                    ModelState.AddModelError("ModalNo", Result.SuccessMessage);
                }
                else if ((Modal.StarRating ?? 0) == 0)
                {
                    Result.SuccessMessage = "Star Rating is mandiatory";
                    ModelState.AddModelError("StarRating", Result.SuccessMessage);
                }
                else if ((Modal.Qty ?? 0) == 0)
                {
                    Result.SuccessMessage = "Qty is mandiatory";
                    ModelState.AddModelError("Qty", Result.SuccessMessage);
                }
                else if ((Modal.Price ?? 0) == 0)
                {
                    Result.SuccessMessage = "Price is mandiatory";
                    ModelState.AddModelError("Price", Result.SuccessMessage);
                }
            }
            if (ModelState.IsValid)
            {
                Modal.CompetitionID = CompetitionID;
                Modal.IPAddress = IPAddress;
                Modal.LoginID = LoginID;
                Modal.EMPID = EMPID;
                Result = Transaction.fnSetCompetitionEntry(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/CompetitionEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/CompetitionEntryList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyPJPPlanList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _MyPJPPlanList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan.List> result = new List<PJPPlan.List>();
            Modal.LoginID = LoginID;
            result = Transaction.GetMyPJPPlanList(Modal);
            return PartialView(result);
        }

        public ActionResult PlanWisePJPEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPPlanID = GetQueryString[2];
            List<PJPEntry.List> result = new List<PJPEntry.List>();
            long PJPPlanID = 0;
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            getResponse.ID = PJPPlanID;
            result = Transaction.GetPlanWisePJPEntryList(getResponse);
            return View(result);
        }


        public ActionResult _PJPEntryView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);
            PJPEntry.Add result = new PJPEntry.Add();
            result = Transaction.GetPJPEntryAdd(getResponse, PJPPlanID, PJPEntryID, SSRTourPlanID);
            return PartialView(result);
        }

        public ActionResult PJPEntryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSR_EMPID = 0;

            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSR_EMPID = GetQueryString[4];

            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSR_EMPID, out SSR_EMPID);
            PJPEntry.Add result = new PJPEntry.Add();
            result = Transaction.GetPJPEntryAdd(getResponse, PJPPlanID, PJPEntryID, SSR_EMPID);
            if (result.SSRList.Count == 0)
            {
                PJPEntry.AddWithNoSSR SSRresult = new PJPEntry.AddWithNoSSR();
                string output = JsonConvert.SerializeObject(result);
                SSRresult = JsonConvert.DeserializeObject<PJPEntry.AddWithNoSSR>(output);
                return View("PJPEntryAdd_NOSSR", SSRresult);
            }


            return View(result);
        }

        [HttpPost]
        public ActionResult PJPEntryAdd(string src, PJPEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSR_EMPID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSR_EMPID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSR_EMPID, out SSR_EMPID);
            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                //if (Modal.ExpenseUpload != null)
                //{
                //    UploadAttachment attachModal = new UploadAttachment();
                //    attachModal.File = Modal.ExpenseUpload;
                //    attachModal.LoginID = LoginID;
                //    attachModal.IPAddress = IPAddress;
                //    attachModal.AttachID = Modal.ExpenseAttachmentID;
                //    attachModal.Doctype = "TL";
                //    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                //    Modal.ExpenseAttachmentID = Attach.ID;
                //    if (!Attach.Status)
                //    {
                //        Result.SuccessMessage = Attach.SuccessMessage;
                //        return Json(Result, JsonRequestBehavior.AllowGet);
                //    }
                //}
                Result = Transaction.fnSetPJPEntry(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;

                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntryList*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult PJPEntryAdd_NOSSR(string src, PJPEntry.AddWithNoSSR Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);

            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.ExpenseUpload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.ExpenseUpload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.ExpenseAttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.ExpenseAttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }


                Result = Transaction.fnSetPJPEntryWithNoSSR(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;
                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntryList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntryList*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult SaleEntry_Import(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<SaleEntry_Import.List> Result = new List<SaleEntry_Import.List>();
            Result = Transaction.GetSaleEntry_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult SaleEntry_Import(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadSaleEntryImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearSaleEntryImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetSaleEntryFromSaleEntryImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult PJPPlan_Import(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan_Import.List> Result = new List<PJPPlan_Import.List>();
            Result = Transaction.GetPJPPlan_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult PJPPlan_Import(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadPJPPlanImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearPJPPlanImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetPJPPlanFromPJPPlanImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }



        public ActionResult MyPJPEntryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        public ActionResult _MyPJPEntryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Transaction.GetMyPJPEntryList(Modal));
        }


        public ActionResult TargetsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Targets.List> Modal = new List<Targets.List>();
            ViewBag.Import = "True";
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.ToString("yyyy-MM");
            return View(result);
        }
        [HttpPost]
        public ActionResult _TargetsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;

            return PartialView(Transaction.GetTargets_List(Modal));
        }
        public ActionResult _AddTarget(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TID = GetQueryString[2];
            long TID = 0;
            long.TryParse(ViewBag.TID, out TID);
            getResponse.ID = TID;
            return PartialView(Transaction.GetTargets_Add(getResponse));
        }
        [HttpPost]
        public ActionResult _AddTarget(string src, Targets.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TID = GetQueryString[2];

            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Transaction.fnSetTarget(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult TargetTranList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long TID = 0;
            ViewBag.TID = GetQueryString[2];
            long.TryParse(ViewBag.TID, out TID);
            Targets.TranList result = new Targets.TranList();
            getResponse.ID = TID;
            result = Transaction.GetTargetsTran_List(getResponse);
            return View(result);
        }

        public ActionResult TargetImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<TargetImport.List> Result = new List<TargetImport.List>();
            Result = Transaction.GetTargets_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult TargetImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Transaction.UploadEMPTargetImportExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Transaction.ClearEMPTargetImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Transaction.SetTarget_FromTargetImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _TourPlanHistory(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long TourPlanID = 0;
            ViewBag.TourPlanID = GetQueryString[2];
            long.TryParse(ViewBag.TourPlanID, out TourPlanID);
            getResponse.ID = TourPlanID;
            return PartialView(Transaction.GetTourPlan_History(getResponse));
        }


        public ActionResult BrandDisplayList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _BrandDisplayList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<BrandDisplay.List> Result = new List<BrandDisplay.List>();
            Modal.LoginID = LoginID;
            Result = Transaction.GetBrandDisplayList(Modal);
            return PartialView(Result);
        }

        public ActionResult _AddBrandDisplay(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CompetitionID = GetQueryString[2];
            long CompetitionID = 0;
            long.TryParse(ViewBag.CompetitionID, out CompetitionID);
            BrandDisplay.Add Modal = new BrandDisplay.Add();
            getResponse.ID = CompetitionID;
            Modal = Transaction.GetBrandDisplay(getResponse);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddBrandDisplay(string src, BrandDisplay.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BrandID = GetQueryString[2];
            long BrandID = 0;
            long.TryParse(ViewBag.BrandID, out BrandID);
            string CompanyCode = ClsApplicationSetting.GetSessionValue("CompanyCode");
            Result.SuccessMessage = "Can't Update";
            if (ClsApplicationSetting.GetSessionValue("RoleName") == "TL" && (Modal.DealerID ?? 0) == 0)
            {
                Result.SuccessMessage = "Dealer is mandiatory";
                ModelState.AddModelError("DealerID", Result.SuccessMessage);
            }
            else if ((Modal.ItemID ?? 0) == 0)
            {
                Result.SuccessMessage = "Item is mandiatory";
                ModelState.AddModelError("ItemID", Result.SuccessMessage);
            }
            else if ((Modal.Qty ?? 0) == 0)
            {
                Result.SuccessMessage = "Qty Can not be blank";
                ModelState.AddModelError("Qty", Result.SuccessMessage);
            }
            else if (string.IsNullOrEmpty(Modal.ImageBase64String))
            {
                Result.SuccessMessage = "Image can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.Doctype = "SSR";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }

                Modal.BrandID = BrandID;
                Modal.IPAddress = IPAddress;
                Modal.LoginID = LoginID;
                Modal.EMPID = EMPID;
                Result = Transaction.fnSetBrandDisplay(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/BrandDisplayList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/BrandDisplayList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



         
        public ActionResult _AddPJPPlans(string src)

        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            PJPPlan.Add Modal = new PJPPlan.Add();
            long ID = 0;
            long.TryParse(ViewBag.PJPID, out ID);

            getResponse.ID = ID;
            Modal = Transaction.GetPJPPlans(getResponse);


            return PartialView(Modal);

        }
        public ActionResult MyPJPPlanLists(string src)
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
        public ActionResult _AddPJPPlans(string src, PJPPlan.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            long PJPID = 0;
            long.TryParse(ViewBag.PJPID, out PJPID);
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            Result.SuccessMessage = "PJP Can't Update";
            if (string.IsNullOrEmpty(Modal.RouteNumber))
            {
                Result.SuccessMessage = "Route Number can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            ModelState.Remove("src");
            ModelState.Remove("DealerTypeID");
            ModelState.Remove("DealerCategoryID");
            ModelState.Remove("RegionID");
            ModelState.Remove("StateID");
            ModelState.Remove("CityID");
            ModelState.Remove("AreaID");
            ModelState.Remove("DealerID");
            ModelState.Remove("Command");
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.PJPID = PJPID;
                Modal.OnDemand = OnDemand;
                Modal.DealerID = Modal.NewDealerID;
                Result = Transaction.fnSetPJPPlans(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PJPPlanLists?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PJPPlanLists");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _MyPJPPlanLists(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<PJPPlan.List> result = new List<PJPPlan.List>();
            Modal.LoginID = LoginID;
            ViewBag.Approved = Modal.Approved;
            result = Transaction.GetMyPJPPlanLists(Modal);
            return PartialView(result);
        }
        public ActionResult PlanWisePJPEntries(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPPlanID = GetQueryString[2];
            List<PJPEntries.List> result = new List<PJPEntries.List>();
            long PJPPlanID = 0;
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            getResponse.ID = PJPPlanID;
            result = Transaction.GetPlanWisePJPEntriesList(getResponse);
            return View(result);
        }
        public ActionResult _PJPEntriesView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);
            PJPEntries.Add result = new PJPEntries.Add();
            result = Transaction.GetPJPEntriesAdd(getResponse, PJPPlanID, PJPEntryID, SSRTourPlanID);
            return PartialView(result);
        }
        public ActionResult PJPEntriesAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSR_EMPID = 0;

            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSR_EMPID = GetQueryString[4];

            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSR_EMPID, out SSR_EMPID);
            PJPEntries.Add result = new PJPEntries.Add();
            result = Transaction.GetPJPEntriesAdd(getResponse, PJPPlanID, PJPEntryID, SSR_EMPID);
            if (result.SSRList.Count == 0)
            {
                PJPEntries.AddWithNoSSR SSRresult = new PJPEntries.AddWithNoSSR();
                string output = JsonConvert.SerializeObject(result);
                SSRresult = JsonConvert.DeserializeObject<PJPEntries.AddWithNoSSR>(output);
                return View("PJPEntriesAdd_NOSSR", SSRresult);
            }


            return View(result);
        }
        [HttpPost]
        public ActionResult PJPEntriesAdd(string src, PJPEntries.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSR_EMPID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSR_EMPID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSR_EMPID, out SSR_EMPID);
            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                //if (Modal.ExpenseUpload != null)
                //{
                //    UploadAttachment attachModal = new UploadAttachment();
                //    attachModal.File = Modal.ExpenseUpload;
                //    attachModal.LoginID = LoginID;
                //    attachModal.IPAddress = IPAddress;
                //    attachModal.AttachID = Modal.ExpenseAttachmentID;
                //    attachModal.Doctype = "TL";
                //    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                //    Modal.ExpenseAttachmentID = Attach.ID;
                //    if (!Attach.Status)
                //    {
                //        Result.SuccessMessage = Attach.SuccessMessage;
                //        return Json(Result, JsonRequestBehavior.AllowGet);
                //    }
                //}
                Result = Transaction.fnSetPJPEntries(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;

                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntries_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntries?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntries*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult PJPEntriesAdd_NOSSR(string src, PJPEntries.AddWithNoSSR Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long PJPPlanID = 0, PJPEntryID = 0, SSRTourPlanID = 0;
            ViewBag.PJPPlanID = GetQueryString[2];
            ViewBag.PJPEntryID = GetQueryString[3];
            ViewBag.SSRTourPlanID = GetQueryString[4];
            long.TryParse(ViewBag.PJPPlanID, out PJPPlanID);
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            long.TryParse(ViewBag.SSRTourPlanID, out SSRTourPlanID);

            Result.SuccessMessage = "Can't Update";
            string Company = ClsApplicationSetting.GetConfigValue("Company");
            if (Company.ToLower() == "daikin" && Modal.BrandEntryList.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Counter Display can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.PJPPlanID = PJPPlanID;
                Modal.PJPEntryID = PJPEntryID;
                Modal.IPAddress = IPAddress;

                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Modal.ExpenseUpload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.ExpenseUpload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.ExpenseAttachmentID;
                    attachModal.Doctype = "TL";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.ExpenseAttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }


                Result = Transaction.fnSetPJPEntriesWithNoSSR(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.PJPEntryID = Result.ID;
                        item.IPAddress = IPAddress;
                        if (!string.IsNullOrEmpty(item.ImageBase64String))
                        {
                            FileResponse attachModal = new FileResponse();
                            attachModal.ImageBase64String = item.ImageBase64String;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.ID = item.AttachID;
                            attachModal.Doctype = "TL";
                            var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                            item.AttachID = Attach.ID;
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }


                        var a = Transaction.fnSetPJPEntries_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Transaction/PlanWisePJPEntries?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Transaction/PlanWisePJPEntries*" + PJPPlanID + "*0*0");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult _PJPDocuments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPEntryID = GetQueryString[2];
            ViewBag.VisitType = GetQueryString[4];
            //ViewBag.Status = GetQueryString[5];
            long PJPEntryID = 0;
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            getResponse.ID = PJPEntryID;
            PJPDocuments result = new PJPDocuments();
            result.DocumentList = Common_SPU.GetPJPDocuments(getResponse);
            if (result.DocumentList.Any(doc => Convert.ToInt32(doc.Status) > 0))
            {
                ViewBag.showbutton = false;
            }
            else
            {
                ViewBag.showbutton = true;
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "PJPExpDocType";
            ViewBag.PJPExpDoc = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);
        }


        [HttpPost]
        public ActionResult _PJPDocuments(string src, PJPDocuments Modal, string Command)
        {                                                                                   
            PostResponse Result = new PostResponse();                   
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.PJPEntryID = GetQueryString[2];
            ViewBag.EMPCode = GetQueryString[3];
            ViewBag.VisitType = GetQueryString[4];
            Modal.DocumentList[0].ExpenseType = "Dealer Visit Exp";
            long PJPEntryID = 0;
            long.TryParse(ViewBag.PJPEntryID, out PJPEntryID);
            getResponse.ID = PJPEntryID;
            Result.SuccessMessage = "Entry Can't Update";
            if (Modal.DocumentList == null || Modal.DocumentList.Count == 0)
            {
                Result.SuccessMessage = "Please add document.";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            foreach (var item in Modal.DocumentList)
            {
                if (string.IsNullOrEmpty(item.ExpenseType))
                {
                    Result.Status = false;                              
                    Result.SuccessMessage = $"Please Select Expense Type";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (item.Amount == "" || item.Amount == null)
                {                                               //          ARUN  KUMAR   RANA
                    Result.Status = false;
                    Result.SuccessMessage = $"Please Enter Amount";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                if (item.ExpenseType != "Dealer Visit Exp")
                {
                    if (item.upload == null)
                    {
                        if (Convert.ToInt32(item.PJPExpensesID) < 1)
                        {
                            Result.Status = false;
                            Result.SuccessMessage = $"Please Upload File";
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            if (ViewBag.VisitType.ToLower() != "local")
            {
                PostResponse resp = new PostResponse();
                PJPExpenses.List expen = new PJPExpenses.List();
                expen.PJPExpensesID = Convert.ToInt32(Modal.DocumentList[0].PJPExpensesID);
                expen.Exp_Amount = Modal.DocumentList[0].Amount;
                expen.Description = Modal.DocumentList[0].Description;
                expen.PJPEntryID = Convert.ToInt32(ViewBag.PJPEntryID);
                expen.LoginID = LoginID;
                expen.IPAddress = IPAddress;
                expen.DocType = "firstwithoutattachment";
                expen.ExpenseType = Modal.DocumentList[0].ExpenseType;
                resp = Common_SPU.fnSetPJPLocalExp(expen);
                Result.ID = resp.ID;
                Result.SuccessMessage = resp.SuccessMessage;
                Result.Status = resp.Status;
                if (!resp.Status)
                {
                    Result.SuccessMessage = resp.SuccessMessage;
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }

            foreach (var item in Modal.DocumentList)
            {
                if (ViewBag.VisitType.ToLower() == "local")
                {
                    PostResponse res = new PostResponse();
                    PJPExpenses.List exp = new PJPExpenses.List();
                    exp.PJPExpensesID = Convert.ToInt32(item.PJPExpensesID);
                    exp.Exp_Amount = item.Amount;
                    exp.Description = item.Description;
                    exp.PJPEntryID = Convert.ToInt32(ViewBag.PJPEntryID);
                    exp.LoginID = LoginID;
                    exp.IPAddress = IPAddress;
                    exp.DocType = "localexp";
                    res = Common_SPU.fnSetPJPLocalExp(exp);
                    Result.ID = res.ID;
                    Result.SuccessMessage = res.SuccessMessage;
                    Result.Status = res.Status;
                    if (!res.Status)
                    {
                        Result.SuccessMessage = res.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }

                }
                //Modal.DocumentList.RemoveAt(0);
                if (item.ExpenseType != "Dealer Visit Exp")
                {
                    if (item.upload != null)
                    {
                        UploadAttachment attachModal = new UploadAttachment();
                        attachModal.FixFileName = Guid.NewGuid().ToString();
                        attachModal.Token = item.ExpenseType;
                        attachModal.Description = item.Description;
                        attachModal.File = item.upload;
                        attachModal.LoginID = PJPEntryID;
                        attachModal.IPAddress = IPAddress;
                        attachModal.AttachID = item.PJPExpensesID;
                        attachModal.Doctype = "pjpdoc";
                        attachModal.TableName = "PJPDoc";
                        attachModal.tableid = PJPEntryID;
                        attachModal.Message = item.Amount;
                        var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                        item.PJPExpensesID = Attach.ID;

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
                    else if (item.PJPExpensesID < 1)
                    {
                        Result.Status = false;
                        Result.SuccessMessage = $"Please Upload File";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                    else if (item.PJPExpensesID == null)
                    {
                        Result.Status = false;
                        Result.SuccessMessage = $"Please Upload File";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                    else if (item.PJPExpensesID > 0)
                    {
                        PostResponse res = new PostResponse();
                        PJPExpenses.List exp = new PJPExpenses.List();
                        exp.PJPExpensesID = Convert.ToInt32(item.PJPExpensesID);
                        exp.Exp_Amount = item.Amount;
                        exp.Description = item.Description;
                        exp.PJPEntryID = Convert.ToInt32(ViewBag.PJPEntryID);
                        exp.LoginID = LoginID;
                        exp.IPAddress = IPAddress;
                        exp.ExpenseType = item.ExpenseType;
                        exp.DocType = "UpdateWithoutDoc";
                        res = Common_SPU.fnSetPJPLocalExp(exp);
                        Result.ID = res.ID;
                        Result.SuccessMessage = res.SuccessMessage;
                        Result.Status = res.Status;
                        if (!res.Status)
                        {
                            Result.SuccessMessage = res.SuccessMessage;
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

    }
}