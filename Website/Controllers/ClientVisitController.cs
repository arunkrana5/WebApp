using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;
using static DataModal.Models.ClientVisit;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ClientVisitController : Controller
    {
        long LoginID = 0, EMPID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        ClientVisitModal _clientVisit;

        public ClientVisitController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            long.TryParse(ClsApplicationSetting.GetSessionValue("EMPID"), out EMPID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            _clientVisit = new ClientVisitModal();
        }
        public ActionResult MyVisit(string src)
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
        public ActionResult _MyVisit(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            List<ClientVisit.List> result = new List<ClientVisit.List>();
            result = _clientVisit.GetClientVisitList(Modal);
            return PartialView(result);
        }

        [OutputCache(Duration = 0)]
        public ActionResult _AddVisitCheckIn(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ClientVisit.AddVisitCheckIn result = new ClientVisit.AddVisitCheckIn();
            result.IsMasterDealer = 0;
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "DealerList";
            result.DealerList = Common_SPU.GetDropDownList(getDropDownResponse);

            getDropDownResponse.Doctype = "CheckinOutStatusList";
            result.AttendenceStatusList = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);
        }


        [HttpPost]

        public ActionResult _AddVisitCheckIn(string src, ClientVisit.AddVisitCheckIn Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CVisitID = GetQueryString[2];
            long CVisitID = 0;
            long.TryParse(ViewBag.CVisitID, out CVisitID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("clientvisit");
            Result.SuccessMessage = "Attendence Can't Update";
            if ((Modal.StatusID == 1 || Modal.StatusID == 2) && string.IsNullOrEmpty(Modal.ImageBase64String))
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("ImageBase64String", Result.SuccessMessage);
            }

            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.CVisitID = CVisitID;
                Modal.Date = DateTime.Now.ToString("dd-MMM-yyyy");
                Modal.EMPID = EMPID;
                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "ClientVisit";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = _clientVisit.fnSetClient_Visit_CheckIn(Modal);
                if (Result.Status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "marked Successfully";
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        [OutputCache(Duration = 0)]
        public ActionResult _AddVisitCheckOut(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.C_TranID = GetQueryString[2];
            long C_TranID = 0;
            long.TryParse(ViewBag.C_TranID, out C_TranID);
            ClientVisit.AddVisitCheckOut result = new ClientVisit.AddVisitCheckOut();
            result.IsNoSale = 0;
            result.C_TranID = C_TranID;
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "CheckinOutStatusList";
            getDropDownResponse.Values = C_TranID.ToString();
            result.AttendenceStatusList = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);
        }

        [HttpPost]

        public ActionResult _AddVisitCheckOut(string src, ClientVisit.AddVisitCheckOut Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.C_TranID = GetQueryString[2];
            long C_TranID = 0;
            long.TryParse(ViewBag.C_TranID, out C_TranID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("clientvisit");
            Result.SuccessMessage = "Attendence Can't Update";
            if ((Modal.StatusID == 1 || Modal.StatusID == 2) && string.IsNullOrEmpty(Modal.ImageBase64String))
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("ImageBase64String", Result.SuccessMessage);
            }

            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.C_TranID = C_TranID;
                if (!string.IsNullOrEmpty(Modal.ImageBase64String))
                {
                    FileResponse attachModal = new FileResponse();
                    attachModal.ImageBase64String = Modal.ImageBase64String;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.ID = Modal.AttachmentID;
                    attachModal.Doctype = "ClientVisit";
                    var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = _clientVisit.fnSetClient_Visit_CheckOut(Modal);
                if (Result.Status)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "marked Successfully";
                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddSales(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.C_TranID = GetQueryString[2];
            long C_TranID = 0;
            long.TryParse(ViewBag.C_TranID, out C_TranID);
            SalesList result = new SalesList();
            List<ClientVisit.AddSales> salesList = new List<ClientVisit.AddSales>();

            ClientVisit.AddSales obj = new ClientVisit.AddSales();
            salesList.Add(obj);
            result.Sales = salesList;
            return View(result);
        }
        [HttpPost]
        public ActionResult AddSales(string src, SalesList Modal)
        {

            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.C_TranID = GetQueryString[2];
            ViewBag.CVisitID = GetQueryString[3];
            long C_TranID = 0, CVisitID = 0;
            long.TryParse(ViewBag.C_TranID, out C_TranID);
            long.TryParse(ViewBag.CVisitID, out CVisitID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("clientvisit");
            Result.SuccessMessage = "Attendence Can't Update";
            if (Modal.Sales.Any(x => string.IsNullOrEmpty(x.ImageBase64String)))
            {
                Result.SuccessMessage = "Image is mandiatory";
                ModelState.AddModelError("ImageBase64String", Result.SuccessMessage);
            }

            if (ModelState.IsValid)
            {
                foreach (var item in Modal.Sales)
                {
                    item.LoginID = LoginID;
                    item.IPAddress = IPAddress;
                    item.C_TranID = C_TranID;
                    item.CVisitID = CVisitID;
                    if (!string.IsNullOrEmpty(item.ImageBase64String))
                    {
                        FileResponse attachModal = new FileResponse();
                        attachModal.ImageBase64String = item.ImageBase64String;
                        attachModal.LoginID = LoginID;
                        attachModal.IPAddress = IPAddress;
                        attachModal.ID = item.AttachmentID;
                        attachModal.Doctype = "ClientVisit";
                        var Attach = ClsApplicationSetting.UploadCameraImage(attachModal);
                        item.AttachmentID = Attach.ID;
                        if (!Attach.Status)
                        {
                            Result.SuccessMessage = Attach.SuccessMessage;
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                    }
                    Result = _clientVisit.fnSetClient_Visit_Sales(item);
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/ClientVisit/MyVisit?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/ClientVisit/MyVisit");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

    }
}