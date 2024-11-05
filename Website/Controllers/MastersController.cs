using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class MastersController : Controller
    {

        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IMasterHelper Master;
        IToolHelper Tools;
        public MastersController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Master = new MasterModal();
            Tools = new ToolsModal();
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
        public ActionResult RFCCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "RFCCategory";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }

        public ActionResult PaymentModeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "PaymentMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }

        public ActionResult TravelModeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "TravelMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }
        public ActionResult CountryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Country";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;           
            return View();
        }

        public ActionResult StateList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "State";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }

        public ActionResult CityList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "City";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }


        public ActionResult AreaList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Area";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _MasterList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            var Result = Master.GetMastersList(param);
            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;
            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult RegionList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TableName = "Region";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            return View();


        }

        public ActionResult _RFCCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "RFCCategory";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }

        public ActionResult _PaymentModeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "PaymentMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }

        public ActionResult _TravelModeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "TravelMode";
            ViewBag.Import = "True";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }

        public ActionResult _CountryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Country";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            return PartialView(result);
        }
        public ActionResult _StateAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "State";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllRegion";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);

        }
        public ActionResult _CityAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "City";
            getResponse.Doctype = ViewBag.TableName;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            getResponse.ID = MasterID;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }


            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllState";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);


            return PartialView(result);

        }



        public ActionResult _AreaAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Area";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllCity";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);
            return PartialView(result);

        }

        public ActionResult _RegionAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            ViewBag.TableName = "Region";
            getResponse.Doctype = ViewBag.TableName;
            getResponse.ID = MasterID;
            Masters.Add result = new Masters.Add();
            result.TableName = ViewBag.TableName;
            if (MasterID > 0)
            {
                result = Master.GetMasters(getResponse);
            }
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AllCountry";
            ViewBag.ddList = Common_SPU.GetDropDownList(getDropDownResponse);
            return PartialView(result);

        }

        [HttpPost]
        public ActionResult SaveMasterAll(string src, Masters.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MasterID = GetQueryString[2];
            long MasterID = 0;
            long.TryParse(ViewBag.MasterID, out MasterID);
            Result.SuccessMessage = "Masters Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.MasterID = MasterID;
                Result = Master.fnSetMasters(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult BranchList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Branch.List> result = new List<Branch.List>();
            result = Master.GetBranchList(getResponse);
            return View(result);
        }
        public ActionResult _BranchAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BranchID = GetQueryString[2];
            long BranchID = 0;
            long.TryParse(ViewBag.BranchID, out BranchID);
            getResponse.ID = BranchID;
            Branch.Add result = new Branch.Add();
            result = Master.GetBranch(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _BranchAdd(string src, Branch.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BranchID = GetQueryString[2];
            long BranchID = 0;
            long.TryParse(ViewBag.BranchID, out BranchID);
            Result.SuccessMessage = "Branch Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.BranchID = BranchID;
                Result = Master.fnSetBranch(Modal);

                if (Result.Status)
                {
                    // Delete Existing Record
                    getResponse.Doctype = "Branch_Mapping";
                    getResponse.ID = Result.ID;
                    Common_SPU.fnDelRecord(getResponse);
                    if (!string.IsNullOrEmpty(Modal.CityIDs))
                    {
                        if (Modal.CityIDs.Contains(','))
                        {
                            foreach (var item in Modal.CityIDs.Split(','))
                            {
                                Master.fnSetBranch_Mapping(Result.ID, item, getResponse);
                            }
                        }
                        else
                        {
                            Master.fnSetBranch_Mapping(Result.ID, Modal.CityIDs, getResponse);
                        }
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/BranchList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/BranchList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }





        public ActionResult DepartmentList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<Department.List> result = new List<Department.List>();
            result = Master.GetDepartmentList(getResponse);
            return View(result);
        }
        public ActionResult _DepartmentAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            long DeptID = 0;
            long.TryParse(ViewBag.DeptID, out DeptID);
            getResponse.ID = DeptID;
            Department.Add result = new Department.Add();
            if (DeptID > 0)
            {
                result = Master.GetDepartment(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DepartmentAdd(string src, Department.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DeptID = GetQueryString[2];
            long DeptID = 0;
            long.TryParse(ViewBag.DeptID, out DeptID);
            Result.SuccessMessage = "Department Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DeptID = DeptID;
                Result = Master.fnSetDepartment(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DepartmentList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DepartmentList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult DesignationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Designation.List> result = new List<Designation.List>();
            result = Master.GetDesignationList(getResponse);
            return View(result);
        }
        public ActionResult _DesignationAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignID = GetQueryString[2];
            long DesignID = 0;
            long.TryParse(ViewBag.DesignID, out DesignID);
            getResponse.ID = DesignID;
            Designation.Add result = new Designation.Add();
            if (DesignID > 0)
            {
                result = Master.GetDesignation(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DesignationAdd(string src, Designation.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DesignID = GetQueryString[2];
            long DesignID = 0;
            long.TryParse(ViewBag.DesignID, out DesignID);
            Result.SuccessMessage = "Designation Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DesignID = DesignID;
                Result = Master.fnSetDesignation(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DesignationList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DesignationList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult BrandList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Brand.List> result = new List<Brand.List>();
            result = Master.GetBrandList(getResponse);
            return View(result);
        }
        public ActionResult _BrandAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BrandID = GetQueryString[2];
            long BrandID = 0;
            long.TryParse(ViewBag.BrandID, out BrandID);
            getResponse.ID = BrandID;
            Brand.Add result = new Brand.Add();
            if (BrandID > 0)
            {
                result = Master.GetBrand(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _BrandAdd(string src, Brand.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.BrandID = GetQueryString[2];
            long BrandID = 0;
            long.TryParse(ViewBag.BrandID, out BrandID);
            Result.SuccessMessage = "Brand Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.BrandID = BrandID;
                Result = Master.fnSetBrand(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/BrandList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/BrandList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult EMPImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EMPImport.List> Result = new List<EMPImport.List>();
            Result = Master.GetEMPImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }




        public ActionResult EmployeeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            return View(Modal);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _EmployeeList(string src, JqueryDatatableParam param)
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
            var Result = Master.GetEmployeeList(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }


        public ActionResult EmployeeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            getResponse.ID = EMPID;
            Employee.Add result = new Employee.Add();
            result = Master.GetEMP(getResponse);
            return View(result);

        }

        [HttpPost]
        public ActionResult EmployeeAdd(string src, Employee.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);

            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Employee Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPID = EMPID;


                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachID;
                    attachModal.Doctype = "";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Master.fnSetEMP(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/EmployeeList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/EmployeeList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _EmployeeDocuments(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.EMPCode = GetQueryString[3];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            getResponse.ID = EMPID;
            EmployeeDocuments result = new EmployeeDocuments();
            result.DocumentList = Common_SPU.GetEmployeeDocuments(getResponse);

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "EmployeeDocType";
            ViewBag.EmployeeDocType = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(result);
        }


        [HttpPost]
        public ActionResult _EmployeeDocuments(string src, EmployeeDocuments Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.EMPID = GetQueryString[2];
            ViewBag.EMPCode = GetQueryString[3];
            long EMPID = 0;
            long.TryParse(ViewBag.EMPID, out EMPID);
            getResponse.ID = EMPID;
            Result.SuccessMessage = "Entry Can't Update";
            if (Modal.DocumentList == null || Modal.DocumentList.Count == 0)
            {
                Result.SuccessMessage = "Please add document.";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                foreach (var item in Modal.DocumentList)
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
                        attachModal.Doctype = "empdoc";
                        attachModal.TableName = ViewBag.EMPCode;
                        attachModal.tableid = EMPID;
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
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult DealerList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.MappingImport = "True";
            return View();

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _DealerList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;


            var Result = Master.GetDealerList(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }
        public ActionResult DealerImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<DealerImport.List> Result = new List<DealerImport.List>();
            Result = Master.GetDealerImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        public ActionResult DealerMappingImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<DealerMappingImport.List> Result = new List<DealerMappingImport.List>();
            Result = Master.GetDealer_Mapping_ImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }


        public ActionResult DealerAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerID = GetQueryString[2];
            long DealerID = 0;
            long.TryParse(ViewBag.DealerID, out DealerID);
            getResponse.ID = DealerID;

            Dealer.Add Result = new Dealer.Add();
            Result = Master.GetDealer(getResponse);
            return View(Result);

        }

        [HttpPost]
        public ActionResult DealerAdd(string src, Dealer.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerID = GetQueryString[2];
            long DealerID = 0;
            long.TryParse(ViewBag.DealerID, out DealerID);
            Result.SuccessMessage = "Dealer Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerID = DealerID;

                Result = Master.fnSetDealer(Modal);
                if (Result.ID > 0 && Result.Status)
                {
                    Modal.DealerID = Result.ID;
                    Result = Master.fnSetDealerMapping(Modal);
                }

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/DealerList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DealerList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }




        public ActionResult AttendenceStatusList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<AttendenceStatus.List> result = new List<AttendenceStatus.List>();
            result = Master.GetAttendenceStatusList(getResponse);
            return View(result);
        }
        public ActionResult _AttendenceStatusAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            getResponse.ID = ID;
            AttendenceStatus.Add result = new AttendenceStatus.Add();
            result = Master.GetAttendenceStatus(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _AttendenceStatusAdd(string src, AttendenceStatus.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Status Can't Update";
            if (!Modal.UseFor.Contains("Leave") && (Modal.MonthlyAccrued ?? 0) > 0)
            {
                Result.SuccessMessage = "Monthly Accrued must be zero, only availiable for leave";
                ModelState.AddModelError("UseFor", Result.SuccessMessage);

            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ID = ID;
                Result = Master.fnSetAttendenceStatus(Modal);
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/AttendenceStatusList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/AttendenceStatusList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MastersImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MastersImport.List> Result = new List<MastersImport.List>();
            Result = Master.GetMastersImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult MastersImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadMastersImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearMastersImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetMastersFromMastersImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyEMPTalentPoolList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            Modal.Approved = 0;
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            Modal.LoginID = LoginID;
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _MyEMPTalentPoolList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            ViewBag.Approved = Modal.Approved;
            return PartialView(Master.GetMyEMPTalentPoolList(Modal));
        }

        public ActionResult AddMyEMPTalentPool(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TPID = GetQueryString[2];
            long TPID = 0;
            long.TryParse(ViewBag.TPID, out TPID);
            EMPTalentPool.MyAdd result = new EMPTalentPool.MyAdd();
            getResponse.ID = TPID;
            getResponse.Doctype = "MyAdd";
            result = Master.GetEMPTalentPool_MyAdd(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult AddMyEMPTalentPool(string src, EMPTalentPool.MyAdd Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            if (Modal.AttachmentsList == null || Modal.AttachmentsList.Count == 0)
            {
                Result.SuccessMessage = "Please add document.";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            foreach (var group in Modal.AttachmentsList)
            {
                if (group.Upload == null && group.Attach_ID==0)
                {
                    Result.SuccessMessage = "Please add document.";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }

            if (ModelState.IsValid)
            {
                Modal.TPID= ID;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Master.fnSetEMP_TalentPool_MyAdd(Modal);
                if (Modal.AttachmentsList != null && Result.Status)
                {
                    foreach (var item in Modal.AttachmentsList)
                    {
                        if (item.Upload != null)
                        {
                            UploadAttachment attachModal = new UploadAttachment();
                            attachModal.AttachID = item.Attach_ID ?? 0;
                            attachModal.tableid = Result.ID;
                            attachModal.TableName = "EMPTalentPool";
                            attachModal.File = item.Upload;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.Doctype = item.Doctype;
                            var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/MyEMPTalentPoolList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/MyEMPTalentPoolList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EMPTalentPoolList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Import = "True";
            EMPTalentPool.Filter Modal = new EMPTalentPool.Filter();
            Modal = Master.GetEMPTalentPoolFilter(getResponse);
            Modal.Month = DateTime.Now.ToString("yyyy-MM-dd");
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _EMPTalentPoolList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approved = 0;
            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];

            int.TryParse(Request.Form["Approved"], out Approved);
            param.Approved = Approved;
            param.LoginID = LoginID;
            param.OtherValue = Request.Form["States"];
            param.OtherValue1 = Request.Form["City"];
            param.OtherValue2 = Request.Form["Pincodes"];
            param.OtherValue3 = Request.Form["Counters"];
            param.OtherValue4 = Request.Form["Location"];

            var Result = Master.GetEMP_TalentPoolList(param);

            long recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult AddEMPTalentPool(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TPID = GetQueryString[2];
            long TPID = 0;
            long.TryParse(ViewBag.TPID, out TPID);
            EMPTalentPool.Add result = new EMPTalentPool.Add();
            getResponse.ID = TPID;
            result = Master.GetEMPTalentPool(getResponse);
            return View(result);
        }
        [HttpPost]
        public ActionResult AddEMPTalentPool(string src, EMPTalentPool.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TPID = GetQueryString[2];
            long TPID = 0;
            long.TryParse(ViewBag.TPID, out TPID);
            Result.SuccessMessage = "Can't Update";
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            foreach (var group in Modal.AttachmentsList)
            {
                if (group.Upload == null && group.Attach_ID == 0)
                {
                    Result.SuccessMessage = "Please add document.";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
            }
            if (ModelState.IsValid)
            {
                Modal.TPID= TPID;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Master.fnSetEMP_TalentPool(Modal);

                if(Modal.AttachmentsList!=null)
                {
                    foreach (var item in Modal.AttachmentsList)
                    {
                        if (item.Upload != null)
                        {
                            UploadAttachment attachModal = new UploadAttachment();
                            attachModal.AttachID = item.Attach_ID ?? 0;
                            attachModal.tableid = Result.ID;
                            attachModal.TableName = "EMPTalentPool";
                            attachModal.File = item.Upload;
                            attachModal.LoginID = LoginID;
                            attachModal.IPAddress = IPAddress;
                            attachModal.Doctype = item.Doctype;
                            var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                            if (!Attach.Status)
                            {
                                Result.SuccessMessage = Attach.SuccessMessage;
                                return Json(Result, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Masters/EMPTalentPoolList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/EMPTalentPoolList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }
        public ActionResult EMPTalentPoolImport(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EMPTalentPoolImport.List> Result = new List<EMPTalentPoolImport.List>();
            Result = Master.GetEMPTalentPoolImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }

        [HttpPost]
        public ActionResult EMPTalentPoolImport(FormCollection Form, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string SheetName = Form.Get("txtSheet");
                if (Request.Files == null)
                {
                    Result.SuccessMessage = "please select file";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                foreach (string upload in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[upload];
                    Result = Master.UploadEMP_TalentPoolImportDataExcelFile(file, SheetName, getResponse);
                }
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearEMP_TalentPoolImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetEMP_TalentPoolFromImportTable(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChallanDocumentsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Date Modal = new Tab.Date();
            Modal.Month = DateTime.Now.ToString("yyyy-MM");
            return View(Modal);
        }
        public ActionResult _ChallanDocumentsList(string src, Tab.Date Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ChallanDocuments.List> Result = new List<ChallanDocuments.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Master.GetChallanDocumentsList(Modal);
            return PartialView(Result);
        }

        public ActionResult _ChallanDocumentsAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChallanID = GetQueryString[2];
            long ChallanID = 0;
            long.TryParse(ViewBag.ChallanID, out ChallanID);
            ChallanDocuments.Add result = new ChallanDocuments.Add();
            getResponse.ID = ChallanID;
            result = Master.GetChallanDocuments(getResponse);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _ChallanDocumentsAdd(ChallanDocuments.Add Modal, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ChallanID = GetQueryString[2];
            long ChallanID = 0;
            long.TryParse(ViewBag.ChallanID, out ChallanID);
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Can't Update";
            if (Modal.Upload == null && Modal.AttachmentID == 0)
            {
                Result.SuccessMessage = "please upload Attachment also";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Master.fnSetChallanDocuments(Modal);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }









       
       

        public ActionResult DealerCategoryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View(Master.GetDealerCategoryList(getResponse));
        }

        public ActionResult _DealerCategoryAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerCategoryID = GetQueryString[2];
            long DealerCategoryID = 0;
            long.TryParse(ViewBag.DealerCategoryID, out DealerCategoryID);
            DealerCategory.Add result = new DealerCategory.Add();
            getResponse.ID = DealerCategoryID;
            if (DealerCategoryID > 0)
            {
                result = Master.GetDealerCategory(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DealerCategoryAdd(string src, DealerCategory.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerCategoryID = GetQueryString[2];
            long DealerCategoryID = 0;
            long.TryParse(ViewBag.DealerCategoryID, out DealerCategoryID);
            Result.SuccessMessage = "Category Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerCategoryID = DealerCategoryID;
                Result = Master.fnSetDealerCategory(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult DealerTypeList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View(Master.GetDealerTypeList(getResponse));
        }

        public ActionResult _DealerTypeAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerTypeID = GetQueryString[2];
            long DealerTypeID = 0;
            long.TryParse(ViewBag.DealerTypeID, out DealerTypeID);
            DealerType.Add result = new DealerType.Add();
            getResponse.ID = DealerTypeID;
            if (DealerTypeID > 0)
            {
                result = Master.GetDealerType(getResponse);
            }
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _DealerTypeAdd(string src, DealerType.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerTypeID = GetQueryString[2];
            long DealerTypeID = 0;
            long.TryParse(ViewBag.DealerTypeID, out DealerTypeID);
            Result.SuccessMessage = "Type Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DealerTypeID = DealerTypeID;
                Result = Master.fnSetDealerType(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }





        public ActionResult MasterCatalogueList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();
            return View(Modal);
        }
        public ActionResult _MasterCatalogueList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<MasterCatalogue.List> Result = new List<MasterCatalogue.List>();
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            Result = Master.GetMasterCatalogueList(Modal);
            return PartialView(Result);
        }

        public ActionResult _MasterCatalogueAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CatID = GetQueryString[2];
            long CatID = 0;
            long.TryParse(ViewBag.CatID, out CatID);
            MasterCatalogue.Add result = new MasterCatalogue.Add();
            if (CatID > 0)
            {
                getResponse.ID = CatID;
                result = Master.GetMasterCatalogue(getResponse);
            }
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _MasterCatalogueAdd(MasterCatalogue.Add Modal, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.CatID = GetQueryString[2];
            long CatID = 0;
            long.TryParse(ViewBag.CatID, out CatID);
            string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("");
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                if (Modal.Upload != null)
                {
                    UploadAttachment attachModal = new UploadAttachment();
                    attachModal.File = Modal.Upload;
                    attachModal.LoginID = LoginID;
                    attachModal.IPAddress = IPAddress;
                    attachModal.AttachID = Modal.AttachmentID;
                    attachModal.Doctype = "";
                    var Attach = ClsApplicationSetting.UploadAttachment(attachModal);
                    Modal.AttachmentID = Attach.ID;
                    if (!Attach.Status)
                    {
                        Result.SuccessMessage = Attach.SuccessMessage;
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                }
                Result = Master.fnSetMasterCatalogue(Modal);
            }

            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult DealerImport(HttpPostedFileBase Fileupload, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {

                string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("import");
                var fileExt = System.IO.Path.GetExtension(Fileupload.FileName);
                var FileName = "DealerImport_" + DateTime.Now.ToString("dd-MMM-yyyy") + fileExt;
                var targetPath = Path.Combine(PhysicalPath, FileName);
                Fileupload.SaveAs(targetPath);
                Result = Master.DealerImport_UploadData(Fileupload, getResponse);

            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearDealerImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.UploadDealerImportDetailList(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DealerMappingImport(HttpPostedFileBase Fileupload, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {

                string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("import");
                var fileExt = System.IO.Path.GetExtension(Fileupload.FileName);
                var FileName = "DealerMappingImport_" + DateTime.Now.ToString("dd-MMM-yyyy") + fileExt;
                var targetPath = Path.Combine(PhysicalPath, FileName);
                Fileupload.SaveAs(targetPath);
                Result = Master.DealerMappingImport_UploadData(Fileupload, getResponse);

            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearDealerMappingImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.UploadDealerMappingImportDetailList(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EMPImport(HttpPostedFileBase Fileupload, string Command, string src)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Not completed";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            if (Command == "ImportData")
            {
                string PhysicalPath = ClsApplicationSetting.GetPhysicalPath("import");
                var fileExt = System.IO.Path.GetExtension(Fileupload.FileName);
                var FileName = "EmployeeImport_" + DateTime.Now.ToString("dd-MMM-yyyy") + fileExt;
                var targetPath = Path.Combine(PhysicalPath, FileName);
                Fileupload.SaveAs(targetPath);
                Result = Master.EmployeeImport_UploadData(Fileupload, getResponse);
            }
            else if (Command == "ClearData")
            {
                Result = Master.ClearEMPImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Master.SetEMPFromEMPImport(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult EMPDocumentsList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "EMPList_Linked";
            getDropDownResponse.LoginID = LoginID;
            ViewBag.EMPList = Common_SPU.GetDropDownList(getDropDownResponse);
            return View();
        }

        [HttpPost]
        public ActionResult _EMPDocumentsList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.SeperatedIDs = string.Join(",", Modal.CommmaSeperatedIDs);
            return PartialView(Common_SPU.GetEMPDocuments_List(Modal));
        }

        [HttpPost]
        public ActionResult EMP_TalentPoolApprove(string src, EMPTalentPool.ApprovalAction Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approved = 0;
            int.TryParse(Command, out Approved);
            if (!string.IsNullOrEmpty(Modal.TPIDs))
            {
                Modal.Approved = Approved;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.TPIDs = Modal.TPIDs.TrimEnd(',');
                Result = Master.fnSetEMP_TalentPool_Approved(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MyDealerRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();

        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _MyDealerRequestList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;


            var Result = Master.GetDealerRequests_MyList(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult DealerRequestList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _DealerRequestList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;


            var Result = Master.GetDealerRequests_List(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        public ActionResult DealerRequestAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.DealerID = GetQueryString[2];
            long DealerID = 0;
            long.TryParse(ViewBag.DealerID, out DealerID);
            getResponse.ID = DealerID;

            DealerRequest.Add Result = new DealerRequest.Add();
            Result = Master.GetDealerRequests(getResponse);
            return View(Result);

        }

        [HttpPost]
        public ActionResult DealerRequestAdd(string src, DealerRequest.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "Dealer Can't Update";
            if (ID>0 && string.IsNullOrEmpty(Modal.DealerCode))
            {
                Result.SuccessMessage = "Dealer Code can't be blank";
                return Json(Result, JsonRequestBehavior.AllowGet);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.ID = ID;

                Result = Master.fnSetDealerRequests(Modal);
            }
            if (Result.Status)
            {
               
                if (ID == 0)
                {
                    TempData["SuccessMsg"] = Result.SuccessMessage;
                    Result.RedirectURL = "/Masters/MyDealerRequestList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/MyDealerRequestList");
                }
                else
                {
                    Result.RedirectURL = "/Masters/DealerRequestList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Masters/DealerRequestList");

                }
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult _DealerSearch(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            var result=Master.GetDealerSearchFilter(getResponse);
            return PartialView(result);
        }


    }
}