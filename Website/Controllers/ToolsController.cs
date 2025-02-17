using DataModal.CommanClass;
using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    [CheckLoginFilter]
    public class ToolsController : Controller
    {
        IToolHelper Tools;
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        public ToolsController()
        {
            getResponse = new GetResponse();
            Tools = new ToolsModal();
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

        public ActionResult ErrorLogList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ErrorLog> Modal = new List<ErrorLog>();
            Modal = Tools.ErrorLogList(getResponse);
            return View(Modal);

        }

        [HttpPost]
        public ActionResult ErrorLogList(string src, FormCollection Form, string Command)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            string SQL = "", ChkAll = "", Chksingle = "";
            long SaveID = 0;
            try
            {
                if (Command == "Delete")
                {
                    if (Form.GetValue("ChkAll") != null)
                    {
                        ChkAll = Form.GetValue("ChkAll").AttemptedValue;
                    }
                    else if (Form.GetValue("Chksingle") != null)
                    {
                        Chksingle = Form.GetValue("Chksingle").AttemptedValue;
                    }

                    if (ChkAll == "All")
                    {
                        SQL = "truncate table Error_Log";
                        clsDataBaseHelper.ExecuteNonQuery(SQL);
                        SaveID = 1;
                    }
                    else if (!string.IsNullOrEmpty(Chksingle))
                    {
                        SQL = "delete from ErrorLog where ID in (" + Chksingle + ")";
                        SaveID = clsDataBaseHelper.ExecuteNonQuery(SQL);
                    }
                }

                if (SaveID > 0)
                {
                    TempData["Success"] = "Y";
                    TempData["Message"] = "Data Deleted Successfully";

                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["Message"] = "Data Can not be Delete";
                }
            }
            catch (Exception Ex)
            {
                Common_SPU.LogError("Error during ErrorLogList. The query was executed :", Ex.ToString(), "ToolsController()", "ToolsController", "ToolsController", LoginID, IPAddress);


            }
            return RedirectToAction("ErrorLogList", new { src = ClsCommon.Encrypt(ViewBag.MenuID + "*" + "/Tools/ErrorLogList") });

        }

        public ActionResult RoleList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<AdminUser.Role.List> Modal = new List<AdminUser.Role.List>();
            Modal = Tools.GetRolesList(getResponse);
            return View(Modal);
        }


        public ActionResult _AddRole(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RoleID = GetQueryString[2];
            AdminUser.Role.Add Modal = new AdminUser.Role.Add();
            long ID = 0;
            long.TryParse(ViewBag.RoleID, out ID);
            getResponse.ID = ID;
            Modal = Tools.GetRoles(getResponse);
            return PartialView(Modal);

        }
        [HttpPost]
        public ActionResult _AddRole(string src, AdminUser.Role.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RoleID = GetQueryString[2];
            long RoleID = 0;
            long.TryParse(ViewBag.RoleID, out RoleID);
            Result.SuccessMessage = "Role Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Tools.fnSetUserRole(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Tools/RoleList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Tools/RoleList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult RoleMenuManagement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RoleID = GetQueryString[2];
            int ID = 0;
            int.TryParse(ViewBag.RoleID, out ID);
            List<AdminModule> Modal = new List<AdminModule>();
            if (ID > 0)
            {
                getResponse.ID = ID;
                Modal = Tools.GetModuleListWithMenu(getResponse);
            }
            return View(Modal);
        }

        private ArrayList ChildMenuManagement(FormCollection Form, List<AdminMenu> ChildMenuList)
        {
            string SQL = "";
            ArrayList ArStr = new ArrayList();
            foreach (AdminMenu Mitem2 in ChildMenuList)
            {
                bool read = false, write = false, modify = false, delete = false, export = false, Import = false, app = false;
                if (Form.GetValue("Read_" + Mitem2.MenuID) != null)
                {
                    read = true;
                }

                SQL = "update Login_Menu_Role_Tran set r='" + read + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);

                if (Form.GetValue("Write_" + Mitem2.MenuID) != null)
                {
                    write = true;
                }

                SQL = "update Login_Menu_Role_Tran set w='" + write + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);

                if (Form.GetValue("Modify_" + Mitem2.MenuID) != null)
                {
                    modify = true;
                }

                SQL = "update Login_Menu_Role_Tran set m='" + modify + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);

                if (Form.GetValue("Delete_" + Mitem2.MenuID) != null)
                {
                    delete = true;
                }

                SQL = "update Login_Menu_Role_Tran set d='" + delete + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);

                if (Form.GetValue("Export_" + Mitem2.MenuID) != null)
                {
                    export = true;
                }

                SQL = "update Login_Menu_Role_Tran set e='" + export + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);


                if (Form.GetValue("Import_" + Mitem2.MenuID) != null)
                {
                    Import = true;
                }

                SQL = "update Login_Menu_Role_Tran set I='" + Import + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);
                if (Form.GetValue("App_" + Mitem2.MenuID) != null)
                {
                    app = true;
                }

                SQL = "update Login_Menu_Role_Tran set App='" + app + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem2.TranID;
                ArStr.Add(SQL);


                if (Mitem2.IsChild == "Y")
                {
                    ChildMenuManagement(Form, Mitem2.ChildMenuList);
                }

            }
            return ArStr;
        }

        [HttpPost]
        public ActionResult RoleMenuManagement(string src, FormCollection Form, string Command)
        {
            int SaveID = 0;
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.RoleID = GetQueryString[2];
            ViewBag.RoleName = GetQueryString[3];
            ArrayList ArStr = new ArrayList();
            long LoginID = 0;
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            string SQL = "";
            try
            {
                if (Command == "Update")
                {
                    int ID = 0;
                    int.TryParse(ViewBag.RoleID, out ID);

                    List<AdminModule> Modal = new List<AdminModule>();
                    if (ID > 0)
                    {
                        getResponse.ID = ID;
                        Modal = Tools.GetModuleListWithMenu(getResponse);
                    }
                    foreach (AdminModule item in Modal)
                    {
                        foreach (AdminMenu Mitem in item.MainMenuList)
                        {
                            bool read = false, write = false, modify = false, delete = false, Import = false, export = false, app = false;
                            if (Form.GetValue("Read_" + Mitem.MenuID) != null)
                            {
                                read = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set r='" + read + "', ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("Write_" + Mitem.MenuID) != null)
                            {
                                write = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set w='" + write + "', ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("Modify_" + Mitem.MenuID) != null)
                            {
                                modify = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set m='" + modify + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("Delete_" + Mitem.MenuID) != null)
                            {
                                delete = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set d='" + delete + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("Export_" + Mitem.MenuID) != null)
                            {
                                export = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set e='" + export + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("Import_" + Mitem.MenuID) != null)
                            {
                                Import = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set I='" + Import + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);

                            if (Form.GetValue("App_" + Mitem.MenuID) != null)
                            {
                                app = true;
                            }

                            SQL = "update Login_Menu_Role_Tran set App='" + app + "',ModifiedDate=getdate(), modifiedby=" + LoginID + " where TranID=" + Mitem.TranID;
                            ArStr.Add(SQL);


                            if (Mitem.IsChild == "Y")
                            {

                                var ChildList = ChildMenuManagement(Form, Mitem.ChildMenuList);
                                if (ChildList != null)
                                {
                                    foreach (var myitem in ChildList)
                                    {
                                        ArStr.Add(myitem);
                                    }
                                }
                            }
                        }
                    }

                    SaveID = clsDataBaseHelper.executeArrayOfSql(ArStr);


                }
                else if (Command == "Sync")
                {
                    clsDataBaseHelper.ExecuteNonQuery("exec spu_Update_Menu_Role_Tran");
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "All Menu Synced Successfully";
                    return RedirectToAction("RoleMenuManagement", new { src = ClsCommon.Encrypt(ViewBag.MenuID + "*" + "/Tools/RoleMenuManagement" + "*" + ViewBag.RoleID + "*" + ViewBag.RoleName) });
                }
                else if (Command == "JSON")
                {
                    ClsApplicationSetting.CreateMenuJSon();
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "Menu JSON Updated Successfully";
                    return RedirectToAction("RoleMenuManagement", new { src = ClsCommon.Encrypt(ViewBag.MenuID + "*" + "/Tools/RoleMenuManagement" + "*" + ViewBag.RoleID + "*" + ViewBag.RoleName) });

                }
                if (SaveID > 0)
                {
                    TempData["Success"] = "Y";
                    TempData["SuccessMsg"] = "Setting Saved Successfully";
                    ClsApplicationSetting.CreateMenuJSon();
                }
                else
                {
                    TempData["Success"] = "N";
                    TempData["SuccessMsg"] = "Setting is not Saved";
                }
            }
            catch (Exception ex)
            {
                Common_SPU.LogError("Error during GetModuleListWithMenu. The query was executed :", ex.ToString(), "spu_GetModuleListRoleWise()", "HomeModal", "HomeModal", LoginID, IPAddress);
            }
            return RedirectToAction("RoleList", new { src = ClsCommon.Encrypt(ViewBag.MenuID + "*" + "/Tools/RoleList") });

        }


        public ActionResult EmailTemplateList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<EmailTemplate.List> Modal = new List<EmailTemplate.List>();
            Modal = Tools.GetEmailTemplateList(getResponse);
            return View(Modal);
        }
        public ActionResult EmailTemplateAdd(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TemplateID = GetQueryString[2];

            long ID = 0;
            long.TryParse(ViewBag.TemplateID, out ID);
            EmailTemplate.Add Modal = new EmailTemplate.Add();
            if (ID > 0)
            {
                getResponse.ID = ID;
                Modal = Tools.GetEmailTemplate(getResponse);
            }
            return View(Modal);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EmailTemplateAdd(string src, EmailTemplate.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.TemplateID = GetQueryString[2];
            long TemplateID = 0;
            long.TryParse(ViewBag.TemplateID, out TemplateID);
            Result.SuccessMessage = "Email Template Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.Body=Regex.Replace(Modal.Body, @">\s+<", "><");
                Result = Tools.fnSetEmailTemplate(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Tools/EmailTemplateList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Tools/EmailTemplateList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }



        public ActionResult UserList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _UserList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;


            var Result = Tools.GetLoginUserList(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }
        public ActionResult _AddUser(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            Users.Add Modal = new Users.Add();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);

            getResponse.ID = ID;
            if (ID > 0)
            {
                Modal = Tools.GetUsers(getResponse);
            }

            GetResponse GetRoleResponses = new GetResponse();
            GetRoleResponses.LoginID = LoginID;
            GetRoleResponses.IPAddress = IPAddress;
            ViewBag.RoleList = Tools.GetRolesList(GetRoleResponses).Where(x => x.IsActive).ToList();

            return PartialView(Modal);

        }

        [HttpPost]
        public ActionResult _AddUser(string src, Users.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "User Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Tools.fnSetUsers(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Tools/UserList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Tools/UserList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult ContactQueryList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();

            GetDropDownResponse getRes = new GetDropDownResponse();
            getRes.Doctype = "MasterStatusList";
            ViewBag.StatusList = Common_SPU.GetDropDownList(getRes);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult _ContactQueryList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Approved = Modal.Approved;
            List<ContactUs_Query.List> result = new List<ContactUs_Query.List>();
            getResponse.Approved = Modal.Approved;
            getResponse.LoginID = LoginID;
            result = Tools.GetContactUs_QueryList(getResponse);
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult ApproveQuery(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            long StatusID = 0, ID = 0;
            int Approved = 0;
            int.TryParse(Command, out Approved);
            string QueryID = form["QueryID"].ToString();
            string MasterStatusID = form["MasterStatusID"].ToString();
            string Remarks = form["Remarks"].ToString();
            long.TryParse(MasterStatusID, out StatusID);
            long.TryParse(QueryID, out ID);
            if (!string.IsNullOrEmpty(QueryID))
            {
                ContactUs_Query.Approve Modal = new ContactUs_Query.Approve();
                Modal.ID = ID;
                Modal.Remarks = Remarks;
                Modal.MasterStatusID = StatusID;
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Tools.fnSetApproveContactUs_Query(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ExecuteAction(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();

        }
        [HttpPost]
        public ActionResult ExecuteAction(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            Result.SuccessMessage = "Can't Update";
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string txtDate = "";
            DateTime dt;
            if (Command == "Attendence")
            {
                txtDate = form["txtDate"].ToString();
                if (string.IsNullOrEmpty(txtDate))
                {
                    Result.SuccessMessage = "Date Can't blank";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    DateTime.TryParse(txtDate, out dt);
                    if (dt.Date >= DateTime.Now.Date)
                    {
                        Result.SuccessMessage = "Date Can't be Greater than today's date";
                        return Json(Result, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        Result = Common_SPU.fnSetAttendence(txtDate, getResponse);
                    }
                }
            }
            else if (Command == "Accrual")
            {
                txtDate = form["txtLeaveDate"].ToString();
                if (string.IsNullOrEmpty(txtDate))
                {
                    Result.SuccessMessage = "Date Can't blank";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                else
                {

                    Result = Common_SPU.fnAutoLeaveAccrual(txtDate, getResponse);

                }
            }
            else if (Command == "PJPPlan")
            {
                Result = Common_SPU.fnAuto_SetPJPPlan();
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DateConfigrationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval Modal = new Tab.Approval();

            GetDropDownResponse drpRespose = new GetDropDownResponse();
            drpRespose.Doctype = "FinancialYearList";
            drpRespose.LoginID = LoginID;
            Modal.List = Common_SPU.GetDropDownList(drpRespose);

            return View(Modal);
        }
        [HttpPost]
        public ActionResult _DateConfigrationList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Common_SPU.GetDateConfigrationList(Modal));
        }


        public ActionResult UserCurrentLocationList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            return View(result);
        }
        [HttpPost]
        public ActionResult _UserCurrentLocationList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Tools.GetUserCurrent_LocationList(getResponse));
        }

        public ActionResult AnnouncementList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            List<Announcement.List> Modal = new List<Announcement.List>();
            getResponse.ID = 0;
            Modal = Tools.GetAnnouncementList(getResponse);
            return View(Modal);
        }

        public ActionResult _AddAnnouncement(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            Announcement.Add Modal = new Announcement.Add();
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            if (ID > 0)
            {
                getResponse.ID = ID;
                Modal = Tools.GetAnnouncement(getResponse);
            }

            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "RoleList";
            Modal.RoleList = Common_SPU.GetDropDownList(getDropDownResponse);

            return PartialView(Modal);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult _AddAnnouncement(string src, Announcement.Add Modal, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.ID = GetQueryString[2];
            long ID = 0;
            long.TryParse(ViewBag.ID, out ID);
            Result.SuccessMessage = "User Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Result = Tools.fnSetAnnouncement(Modal);

            }
            if (Result.Status)
            {
                Result.RedirectURL = "/Tools/AnnouncementList?src=" + ClsCommon.Encrypt(ViewBag.MenuID.ToString() + "*/Tools/AnnouncementList");
            }
            return Json(Result, JsonRequestBehavior.AllowGet);

        }


        public ActionResult AutoReportLog(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Tab.Approval result = new Tab.Approval();
            result.Month = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            return View(result);
        }
        [HttpPost]
        public ActionResult _AutoReportLog(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            Modal.IPAddress = IPAddress;
            return PartialView(Tools.GetAutoReport_LogList(Modal));
        }

        public ActionResult _AutoReport_User(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ViewBag.Date = GetQueryString[2];
            return PartialView(Common_SPU.GetAutoMail_UsersList());
        }
        public ActionResult ManageActivities(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }


        public ActionResult ManageSales(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ManageActivities.Sale Modal = new ManageActivities.Sale();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult ManageSales(string src, ManageActivities.Sale Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Result.SuccessMessage = "Can't Update";
            if (Modal.Doctype == "DateChange" && string.IsNullOrEmpty(Modal.Date))
            {
                Result.SuccessMessage = "Date is mandiatory";
                ModelState.AddModelError("Date", Result.SuccessMessage);
            }
            else if (Modal.Doctype == "StatusChange" && (Modal.Approved ?? 0) == 0)
            {
                Result.SuccessMessage = "Status is mandiatory";
                ModelState.AddModelError("Status", Result.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                string source = Modal.SaleNo;
                string result = string.Concat(source.Where(c => !char.IsWhiteSpace(c)));


                Modal.SaleNo = result.Trim();
                Result = Tools.fnSet_Sales_Manage(Modal);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageAttendance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ManageActivities.Attendance Modal = new ManageActivities.Attendance();
            getResponse.Doctype = "Attendence";
            Modal.AttendenceStatusList = Common_SPU.GetAttendenceStatus(getResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult ManageAttendance(string src, ManageActivities.Attendance Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPCode = Modal.EMPCode.Trim();
                Result = Tools.fnSet_Attendance_Manage(Modal);

            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ManageEMPDOL(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ManageActivities.EMPDOL Modal = new ManageActivities.EMPDOL();
            return View(Modal);
        }

        [HttpPost]
        public ActionResult ManageEMPDOL(string src, ManageActivities.EMPDOL Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.EMPCode = Modal.EMPCode.Trim();
                Result = Tools.fnSet_EMPDOL(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserDeviceList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            return View();
        }
        [HttpPost]
        public ActionResult _UserDeviceList(string src, Tab.Approval Modal)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Modal.LoginID = LoginID;
            return PartialView(Common_SPU.GetUserDeviceList(Modal));
        }

        [HttpPost]
        public ActionResult _ClearDevice(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            string IDs = form["IDs"].ToString();
            string Remarks = form["Remarks"].ToString();
            if (!string.IsNullOrEmpty(IDs))
            {
                GetResponse Modal = new GetResponse();

                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.Param1 = IDs.TrimEnd(',');
                Modal.Param2 = Remarks;
                Result = Common_SPU.fnSetClearUserDevice(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImportLeaveBalance(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            List<ManageActivities.ImportLeaveBalance> Result = new List<ManageActivities.ImportLeaveBalance>();
            Result = Tools.GetLeaveBalanceImportList(getResponse);
            ViewBag.ListCount = Result.Count;
            return View(Result);
        }
        [HttpPost]
        public ActionResult ImportLeaveBalance(HttpPostedFileBase Fileupload, string Command, string src)
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
                var FileName = "LeaveBalanceImport_" + DateTime.Now.ToString("dd-MMM-yyyy") + fileExt;
                var targetPath = Path.Combine(PhysicalPath, FileName);
                Fileupload.SaveAs(targetPath);
                Result = Tools.LeaveBalanceImport_UploadData(Fileupload, getResponse);

            }
            else if (Command == "ClearData")
            {
                Result = Tools.ClearLeaveBalanceImportTemp(getResponse);
            }
            else if (Command == "UploadData")
            {
                Result = Tools.UpdateLeaveBalanceData(getResponse);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PendingLeave(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ManageActivities.PendingLeave Modal = new ManageActivities.PendingLeave();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult PendingLeave(string src, ManageActivities.PendingLeave Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.DocNo = Modal.DocNo.Trim();
                Result = Tools.fnSet_PendingLeave(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ManageAssignTo(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            ManageActivities.AssignTo Modal = new ManageActivities.AssignTo();
            GetDropDownResponse getDropDownResponse = new GetDropDownResponse();
            getDropDownResponse.Doctype = "AssignTo";
            ViewBag.AssignToList = Common_SPU.GetDropDownList(getDropDownResponse);
            return View(Modal);
        }
        [HttpPost]
        public ActionResult ManageAssignTo(string src, ManageActivities.AssignTo Modal)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            Result.SuccessMessage = "Can't Update";
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.RequestNos = Regex.Replace(Modal.RequestNos, @"\s+", "").Trim(',');
                Result = Tools.fnSet_AssignTo(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }
    }
}