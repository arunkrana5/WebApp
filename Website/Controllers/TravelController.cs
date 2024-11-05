using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class TravelController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        ITravelHelper trvl;
        public TravelController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            trvl = new TravelModal();
        }
        public ActionResult MyTravelRequestsList(string src)
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
        public ActionResult _MyTravelRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            Modal.Doctype = "My";
            return PartialView(trvl.GetTravel_Request_List(Modal));
        }

        public ActionResult AddRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            Travel.Add modal = new Travel.Add();
            getResponse.ID = TRID;
            getResponse.Doctype = ClsApplicationSetting.GetSessionValue("UserType");
            modal = trvl.GetTravel_Request(getResponse);
            return View(modal);
        }
        [HttpPost]
        public ActionResult AddRequest(string src, Travel.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            long TRID = 0, EMPID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            DateTime dtStart, dtEnd, CurrentDt = DateTime.Now;
            DateTime.TryParse(Modal.StartDate, out dtStart);
            DateTime.TryParse(Modal.EndDate, out dtEnd);
            Result.SuccessMessage = "Can't Update";
            if (EMPID == 0)
            {
                Result.SuccessMessage = "You can't give Travel request";
                ModelState.AddModelError("EMPID", Result.SuccessMessage);
            }
            if (dtStart.Date < CurrentDt.Date)
            {
                Result.SuccessMessage = "Start date must be greater than today's date";
                ModelState.AddModelError("StartDate", Result.SuccessMessage);
            }
            if (dtEnd.Date < dtStart.Date)
            {
                Result.SuccessMessage = "End date must be greater than Start date";
                ModelState.AddModelError("EndDate", Result.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                Modal.TRID = TRID;
                Modal.IPAddress = IPAddress;
                Modal.LoginID = LoginID;
                Modal.EMPID = EMPID;

                Result = trvl.fnSetTravel_Request(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Travel/MyTravelRequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/MyTravelRequestsList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MyApprovedTravelRequestsList(string src)
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
        public ActionResult _MyApprovedTravelRequestsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(trvl.GetTravel_ApprovedRequestList(Modal));
        }

        public ActionResult TravelRequestsWiseVisitEntry_List(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            Tab.Approval Modal = new Tab.Approval();
            Modal.ID = TRID;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            List<VisitEntry.List> ResultModal = new List<VisitEntry.List>();
            ResultModal = trvl.GetTravel_VisitEntry_List(Modal);
            return View(ResultModal);
        }

        public ActionResult TRVisitEntry_List(string src)
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
        public ActionResult _TRVisitEntry_List(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            List<VisitEntry.List> ResultModal = new List<VisitEntry.List>();
            ResultModal = trvl.GetTravel_VisitEntry_List(Modal);
            return PartialView(ResultModal);
        }


        public ActionResult _AddMoreTravelDealer(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            int.TryParse(ViewBag.OnDemand, out OnDemand);

            VisitEntry.AddMoreDealer Modal = new VisitEntry.AddMoreDealer();
            Modal.TRID = TRID;
            Modal.OnDemand = OnDemand;
            Modal.LoginID = LoginID;

            GetDropDownResponse drpRespose = new GetDropDownResponse();
            drpRespose.Doctype = "TravelAddMoreDealer";
            drpRespose.LoginID = LoginID;
            drpRespose.Values = TRID.ToString();
            Modal.DealerList = Common_SPU.GetDropDownList(drpRespose);
            return PartialView(Modal);
        }

        [HttpPost]
        public ActionResult _AddMoreTravelDealer(string src, VisitEntry.AddMoreDealer Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.OnDemand = GetQueryString[3];
            int OnDemand = 0;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            int.TryParse(ViewBag.OnDemand, out OnDemand);
            Result.SuccessMessage = "Dealer Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.TRID = TRID;
                Modal.OnDemand = OnDemand;
                Result = trvl.fnSetTravel_Dealers(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult VisitEntryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            ViewBag.DealerID = GetQueryString[4];
            ViewBag.SSR_EMPID = GetQueryString[5];
            long TRID = 0, DealerID = 0, SSR_EMPID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            long.TryParse(ViewBag.DealerID, out DealerID);
            long.TryParse(ViewBag.SSR_EMPID, out SSR_EMPID);

            Travel.Values Modal = new Travel.Values();
            Modal.DealerID = DealerID;
            Modal.TRID = TRID;
            Modal.SSR_EMPID = SSR_EMPID;
            Modal.LoginID = LoginID;
            Modal.VisitDate = DateTime.Now.ToString("dd-MMM-yyyy");

            VisitEntry.Add result = new VisitEntry.Add();
            result = trvl.GetTravel_Values(Modal);
            if (DealerID != 0 && result.SSRList.Count == 0)
            {
                VisitEntry.AddWithNoSSR SSRresult = new VisitEntry.AddWithNoSSR();
                string output = JsonConvert.SerializeObject(result);
                SSRresult = JsonConvert.DeserializeObject<VisitEntry.AddWithNoSSR>(output);
                return View("VisitEntryAdd_NOSSR", SSRresult);
            }
            return View(result);
        }

        [HttpPost]
        public ActionResult VisitEntryAdd(string src, VisitEntry.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            ViewBag.DealerID = GetQueryString[4];
            ViewBag.SSR_EMPID = GetQueryString[5];
            long TRID = 0, DealerID = 0, SSR_EMPID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            long.TryParse(ViewBag.DealerID, out DealerID);
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
                Modal.TRID = TRID;
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

                Result = trvl.fnSetTravel_VisitEntry(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.VisitID = Result.ID;
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


                        var a = trvl.fnSetTravel_VisitEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Travel/TravelRequestsWiseVisitEntry_List?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/TravelRequestsWiseVisitEntry_List*" + TRID + "*" + ViewBag.DocNo);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult VisitEntryAdd_NOSSR(string src, VisitEntry.AddWithNoSSR Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            ViewBag.DealerID = GetQueryString[4];
            ViewBag.SSR_EMPID = GetQueryString[5];
            long TRID = 0, DealerID = 0, SSR_EMPID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            long.TryParse(ViewBag.DealerID, out DealerID);
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
                Modal.TRID = TRID;
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


                Result = trvl.fnSetTravel_VisitEntryWithNoSSR(Modal);
                if (Result.Status && Modal.BrandEntryList != null)
                {
                    foreach (var item in Modal.BrandEntryList)
                    {
                        item.LoginID = LoginID;
                        item.VisitID = Result.ID;
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


                        var a = trvl.fnSetTravel_VisitEntry_Brand(item);
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Travel/TravelRequestsWiseVisitEntry_List?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/TravelRequestsWiseVisitEntry_List*" + TRID + "*" + ViewBag.DocNo);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult _VisitEntryView(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long VisitID = 0;
            ViewBag.VisitID = GetQueryString[2];
            long.TryParse(ViewBag.VisitID, out VisitID);
            VisitEntry.Add result = new VisitEntry.Add();
            getResponse.ID = VisitID;
            result = trvl.GetTravel_VisitEntry(getResponse);
            return PartialView(result);
        }




        public ActionResult Travel_RequestApproval(string src)
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
        public ActionResult _Travel_RequestApproval(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            Modal.Doctype = "";
            return PartialView(trvl.GetTravel_Request_List(Modal));
        }

        [HttpPost]
        public ActionResult ApproveTRequests(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            int Approved = 0;
            int.TryParse(Command, out Approved);
            string TRIDs = form["TRIDs"].ToString();
            if (!string.IsNullOrEmpty(TRIDs))
            {
                ApprovalAction Modal = new ApprovalAction();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = TRIDs.TrimEnd(',');
                Result = trvl.fnSetTravel_Requests_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult MyTravel_ExpenseList(string src)
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
        public ActionResult _MyTravel_ExpenseList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(trvl.GetTravel_MyExpenseList(Modal));
        }

        public ActionResult AddTExpense(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            getResponse.ID = TRID;
            TravelExpense Modal = new TravelExpense();
            Modal = trvl.GetTravel_ExpenseSummary(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult AddTExpense(string src, TravelExpense Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);

            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.TRID = TRID;
                Modal.IPAddress = IPAddress;

                if (Modal.AttachmentList != null)
                {
                    foreach (var item in Modal.AttachmentList)
                    {
                        if (item.ExpenseUpload != null)
                        {
                            UploadAttachment attachModal = new UploadAttachment();
                            attachModal.tableid = TRID;
                            attachModal.File = item.ExpenseUpload;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.AttachID = (item.ID ?? 0);
                            attachModal.Doctype = "travel";
                            attachModal.Description = item.Description;
                            var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                            item.ID = Attach.ID;

                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

                if (Modal.OtherExpenseList != null)
                {
                    foreach (var item in Modal.OtherExpenseList)
                    {
                        if (item.Upload != null)
                        {
                            UploadAttachment attachModal = new UploadAttachment();
                            attachModal.tableid = TRID;
                            attachModal.File = item.Upload;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.AttachID = (item.AttachID ?? 0);
                            attachModal.Doctype = "travel";
                            attachModal.Description = item.Description;
                            var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                            item.AttachID = Attach.ID;

                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                Result = trvl.fnSetTravel_Expense(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Travel/MyTravel_ExpenseList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/MyTravel_ExpenseList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _ViewExpense_ApprovalSummary(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            string DocNo = ViewBag.DocNo;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            getResponse.ID = TRID;
            return PartialView(trvl.GetTravel_Expense_ApprovalSummary(getResponse));
        }

        public ActionResult Travel_ExpenseList(string src)
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
        public ActionResult _Travel_ExpenseList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Modal.Doctype = ClsApplicationSetting.GetSessionValue("UserType");
            return PartialView(trvl.GetTravel_ExpenseList(Modal));
        }

        public ActionResult ViewTravelCompleteRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.DocNo = GetQueryString[3];
            ViewBag.Approval = GetQueryString[4];
            string DocNo = ViewBag.DocNo;
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            getResponse.ID = TRID;
            getResponse.Doctype = ClsApplicationSetting.GetSessionValue("UserType");
            TravelCompleteRequest Modal = new TravelCompleteRequest();
            Modal = trvl.GetTravelCompleteRequest(getResponse);
            return View(Modal);
        }

        public ActionResult TravelExpense_Approve(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TRID = GetQueryString[2];
            ViewBag.Approval = GetQueryString[3];
            long TRID = 0;
            long.TryParse(ViewBag.TRID, out TRID);
            int Approved = 0;
            int.TryParse(Command, out Approved);
            if (string.IsNullOrEmpty(form["ApproveRemarks"].ToString()))
            {
                Result.SuccessMessage = "Remarks can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (TRID != 0)
            {
                ApprovalAction Modal = new ApprovalAction();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = TRID.ToString();
                Modal.Doctype = ClsApplicationSetting.GetSessionValue("UserType");
                Result = trvl.fnSetTravelExpense_Approve(Modal);
            }
            if (Result.Status)
            {
                if (ViewBag.Approval == "Approval")
                {
                    Result.RedirectURL = "/Travel/Travel_ExpenseList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/Travel_ExpenseList");
                }
                else
                {
                    Result.RedirectURL = "/Travel/MyTravel_ExpenseList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Travel/MyTravel_ExpenseList");
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }




    }
}