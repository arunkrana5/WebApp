using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.IO;
using System.Linq;
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
        //[HttpPost]
        //public ActionResult _MyRequestsList(string src, Tab.Approval Modal)
        //{
        //    ViewBag.src = src;
        //    string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
        //    ViewBag.GetQueryString = GetQueryString;
        //    ViewBag.MenuID = GetQueryString[0];
        //    Modal.LoginID = LoginID;
        //    Modal.IPAddress = IPAddress;
        //    ViewBag.Approved = Modal.Approved;
        //    return PartialView(recur.GetRequirement_MyRequest(Modal));
        //}
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _MyRequestsList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.Approved = Convert.ToInt32(GetQueryString[2]);
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = recur.GetRequirement_MyRequest(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult _AddRequest(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BranchCode = GetQueryString[2];
            long ReqID = 0;
            if (GetQueryString[3] != "0")
            {
                long.TryParse(GetQueryString[4], out ReqID);
                ViewBag.ReqID = ReqID;
            }
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
            ViewBag.BranchCode = GetQueryString[2];
            Result.SuccessMessage = "Request Can't Update";
            if (!string.IsNullOrEmpty(ClsApplicationSetting.GetConfigValue("Company")))
            {
                if (ClsApplicationSetting.GetConfigValue("Company").ToLower() != "thriverainhouse")
                {
                    ModelState.Remove("HiredFor");
                    ModelState.Remove("AssignToID");
                }
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                if (Modal.ReqID < 1)
                {
                    Modal.ReqID = 0;
                }
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
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "ExperienceList";
            ViewBag.ExperienceList = Common_SPU.GetDropDownList(getDropDownResponse);
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
            string BasePath = ClsApplicationSetting.GetConfigValue("ApplicationPhysicalPath");
            if (ModelState.IsValid)
            {
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.AttachID = 0;
                    attachModal.tableid = ReqID;
                    attachModal.TableName = "emptalentpool";
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.Doctype = "applicationresume";
                    attachModal.Description = "RequirementApplication";
                    Result = ClsApplicationSetting.UploadAttachment(attachModal);
                    if (!Result.Status)
                    {
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(Modal.FileName))
                    {
                        Result.AdditionalMessage = Modal.FileName;
                        Result.Status = true;
                    }
                    else
                    {
                        Result.SuccessMessage = "File can't be blank";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                if (Result.Status)
                {
                    Modal.FileName = Result.AdditionalMessage;
                    Modal.LoginID = LoginID;
                    Modal.IPAddress = IPAddress;
                    Modal.ReqID = ReqID;
                    Result = recur.fnSetRequirement_Application(Modal);
                    return Json(Result, JsonRequestBehavior.AllowGet);
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
            ViewBag.OtherExport = "True";
            return View(Modal);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _AllRequestsList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.Approved = Convert.ToInt32(GetQueryString[2]);
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = recur.GetRequirement_RequestList(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet);
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
                finalSubmit = (Command == "FinalSubmit" ? 1 : 0);
                if (Modal.ApplicationList.Any(item => !string.IsNullOrEmpty(item.IsChecked)) || Command == "FinalSubmit" || Command == "Update")
                {
                    foreach (var item in Modal.ApplicationList)
                    {
                        if (String.IsNullOrEmpty(item.Phone))
                        {
                            Result.SuccessMessage = "Phone can't be blank";
                            return Json(Result, JsonRequestBehavior.AllowGet);
                        }
                        var appro = 0;
                        if (Command == "Approve" || Command == "FinalSubmit")
                        {
                            appro = (!string.IsNullOrEmpty(item.IsChecked) ? 1 : value);
                            if (appro == 1)
                            {
                                if (String.IsNullOrEmpty(item.EmailID))
                                {
                                    Result.SuccessMessage = "Email can't be blank";
                                    return Json(Result, JsonRequestBehavior.AllowGet);
                                }
                                if (item.NetPay < 1)
                                {
                                    Result.SuccessMessage = "Net Pay can't be 0";
                                    return Json(Result, JsonRequestBehavior.AllowGet);
                                }
                                if (String.IsNullOrEmpty(item.DOJ))
                                {
                                    Result.SuccessMessage = "DOJ can't be blank";
                                    return Json(Result, JsonRequestBehavior.AllowGet);
                                }
                            }
                        }
                        if (Command == "Reject")
                        {
                            appro = (!string.IsNullOrEmpty(item.IsChecked) ? 2 : value);
                        }
                        if (Command == "Update")
                        {
                            appro = 3;
                        }
                        if (appro > 0 || finalSubmit == 1)
                        {
                            Result = recur.fnSetRequirementApplication_Approved(ReqID, item.ApplicationID, appro, item.ApprovedRemarks, item.Phone, item.EmailID, item.NetPay, item.DOJ, Modal.ApprovedRemarks, finalSubmit, LoginID, IPAddress);
                            if (!Result.Status)
                            {
                                Result.SuccessMessage = Result.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
                else
                {
                    Result.SuccessMessage = "Please Select atleast one.";
                }
            }
            Result.RedirectURL = "/Recurit/MyRequestsList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*0");
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
        public ActionResult _RequirementDashboard(string src)
        {
            GetResponse Modal = new GetResponse();
            return PartialView(Common_SPU.GetRequirementDashboard(Modal));
        }
    }
}