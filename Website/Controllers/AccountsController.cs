using DataModal.CommanClass;
using DataModal.DataModal.ModelsMaster;
using DataModal.Models;
using DataModal.ModelsMasterHelper;
using System;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{
    public class AccountsController : Controller
    {

        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IAccountsHelper Account;
        public AccountsController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            Account = new AccountsModel();
        }
        public ActionResult Login(string ReturnURL)
        {
            var CompanyCode = ClsApplicationSetting.GetConfigValue("Company");
            ClsApplicationSetting.SetSessionValue("CompanyCode", CompanyCode);
            ViewBag.ReturnURL = ReturnURL;
            AdminUser.Login Modal = new AdminUser.Login();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult Login(AdminUser.Login Modal, string ReturnURL, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == "Submit")
                {

                    Modal.IPAddress = ClsCommon.GetIPAddress();
                    Modal.SessionID = HttpContext.Session.SessionID;
                    AdminUser.Details result = Account.GetLogin(Modal);
                    if (result.status)
                    {
                        ClsApplicationSetting.SetSessionValue("LoginID", result.LoginID.ToString());
                        ClsApplicationSetting.SetSessionValue("UserID", result.UserID.ToString());
                        ClsApplicationSetting.SetSessionValue("RoleID", result.RoleID.ToString());
                        ClsApplicationSetting.SetSessionValue("RoleName", result.RoleName.ToString());
                        ClsApplicationSetting.SetSessionValue("EMPID", result.EMPID.ToString());
                        ClsApplicationSetting.SetSessionValue("EMPName", result.EMPName.ToString());
                        ClsApplicationSetting.SetSessionValue("EMPCode", result.EMPCode.ToString());
                        ClsApplicationSetting.SetSessionValue("Phone", result.Phone.ToString());
                        ClsApplicationSetting.SetSessionValue("Email", result.Email.ToString());
                        ClsApplicationSetting.SetSessionValue("Gender", result.Gender.ToString());
                        ClsApplicationSetting.SetSessionValue("DesignName", result.DesignName.ToString());
                        ClsApplicationSetting.SetSessionValue("DeptName", result.DeptName.ToString());
                        ClsApplicationSetting.SetSessionValue("AttendenceStatus", result.AttendenceStatus.ToString());
                        ClsApplicationSetting.SetSessionValue("DealerID", result.DealerID.ToString());
                        ClsApplicationSetting.SetSessionValue("DealerName", result.DealerName.ToString());
                        ClsApplicationSetting.SetSessionValue("DealerCode", result.DealerCode.ToString());
                        ClsApplicationSetting.SetSessionValue("ImageURL", result.ImageURL.ToString());
                        ClsApplicationSetting.SetSessionValue("CompanyCode", result.CompanyCode.ToString());
                        ClsApplicationSetting.SetSessionValue("ConfigToken", result.ConfigToken.ToString());
                        ClsApplicationSetting.SetSessionValue("DealerArea", result.DealerArea.ToString());

                        if (!string.IsNullOrEmpty(ReturnURL))
                        {
                            return Redirect(ClsCommon.Decrypt(ReturnURL));
                        }
                        else
                        {
                            return RedirectToAction("Dashboard", "Home");

                        }

                    }
                    else
                    {
                        TempData["LoginSuccess"] = "N";
                        TempData["LoginSuccessMsg"] = result.Message;
                        return View(Modal);
                    }

                }
            }
            return View(Modal);

        }
        public ActionResult Logout()
        {

            ClsApplicationSetting.ClearSessionValues();
            // FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Accounts");
        }
        public ActionResult PageNotFound()
        {
            if (TempData["Message"] == null)
            {
                TempData["Message"] = "Page Not Found";
            }
            return View();
        }

        public ActionResult ForgotPassword()
        {
            ForgotPassword obj = new ForgotPassword();
            return View(obj);
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPassword Modal)
        {
            getResponse.Doctype = "ForgotPassword";
            getResponse.Param1 = Modal.UserID;


            var smtpMail = Common_SPU.GetMail_Data(getResponse);
            smtpMail.MailBody = smtpMail.TemplateData.Body;
            smtpMail.Subject = smtpMail.TemplateData.Subject;
            smtpMail.CC = smtpMail.TemplateData.CCMail;
            smtpMail.BCC = smtpMail.TemplateData.BCCMail;
            if (smtpMail.AutoReportUsersList != null)
            {
                smtpMail.ToEmail = smtpMail.AutoReportUsersList.Select(x => x.EmailID).FirstOrDefault();
            }
            if (!string.IsNullOrEmpty(smtpMail.ToEmail))
            {
                smtpMail.MailBody = smtpMail.MailBody.Replace("#WEBSITEURL#", smtpMail.TemplateData.ConfigSettingList.Where(x => x.ConfigKey == "WebsiteURL").Select(x => x.ConfigValue).FirstOrDefault());
                smtpMail.MailBody = smtpMail.MailBody.Replace("#WEBSITELOGOPATH#", smtpMail.TemplateData.ConfigSettingList.Where(x => x.ConfigKey == "WebsiteLogPath").Select(x => x.ConfigValue).FirstOrDefault());


                string ENCRYPTEDURL = smtpMail.TemplateData.ConfigSettingList.Where(x => x.ConfigKey == "WebsiteURL").Select(x => x.ConfigValue).FirstOrDefault();
                ENCRYPTEDURL += "/Accounts/SetPassword?Token=" + ClsCommon.Encrypt(Modal.UserID + "*" + DateTime.Now.ToString());
                smtpMail.MailBody = smtpMail.MailBody.Replace("#ENCRYPTEDURL#", ENCRYPTEDURL);
                smtpMail.MailBody = smtpMail.MailBody.Replace("#USERID#", smtpMail.AutoReportUsersList.Select(x => x.UserID).FirstOrDefault());
                MailConfigration.SendMail(smtpMail);
                TempData["LoginSuccess"] = "Y";
                TempData["LoginSuccessMsg"] = "Mail Sent Successfully";

            }
            else
            {
                TempData["LoginSuccess"] = "N";
                TempData["LoginSuccessMsg"] = "User ID not valid or Email Not found.";
            }
            return View(Modal);
        }
        public ActionResult SetPassword(string Token)
        {
            SetPassword obj = new SetPassword();
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(Token);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.Token = GetQueryString[0];
            ViewBag.Date = GetQueryString[1];
            DateTime dt;
            DateTime.TryParse(ViewBag.Date, out dt);
            if (dt.AddMinutes(15) <= DateTime.Now)
            {
                TempData["Message"] = "Link has been expired";
                return RedirectToAction("PageNotFound", "Accounts");
            }
            return View(obj);
        }
        [HttpPost]
        public ActionResult SetPassword(SetPassword modal)
        {
            PostResponse Result = new PostResponse();
            if (modal.NewPassword != modal.ConfirmPassword)
            {
                Result.SuccessMessage = "Confirm Password and New Password must be matched";
                ModelState.AddModelError("NewPassword", Result.SuccessMessage);
            }
            if (ModelState.IsValid)
            {
                ChangePassword getmodal = new ChangePassword();
                getmodal.OldPassword = ClsCommon.Encrypt(modal.ConfirmPassword);
                getmodal.NewPassword = ClsCommon.Encrypt(modal.NewPassword);
                getmodal.IPAddress = IPAddress;
                getmodal.LoginID = LoginID;
                getmodal.Doctype = "ByLink";
                Result = Common_SPU.fnSetChangePassword(getmodal);
            }
            TempData["LoginSuccess"] = (Result.Status ? "Y" : "N");
            TempData["LoginSuccessMsg"] = Result.SuccessMessage;

            if (Result.Status)
            {
                return RedirectToAction("Login", "Accounts");
            }
            else
            {
                return View(modal);

            }

        }


        public ActionResult DeactivateMyAccount()
        {
            TerminateRequest.Add Modal = new TerminateRequest.Add();
            return View(Modal);
        }
        [HttpPost]
        public ActionResult DeactivateMyAccount(TerminateRequest.Add Modal)
        {
            PostResponse Result = new PostResponse();
            if (ModelState.IsValid)
            {
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                // Result = Account.fnSetTerminationRequest(Modal);
            }
            TempData["LoginSuccess"] = (Result.Status ? "Y" : "N");
            TempData["LoginSuccessMsg"] = Result.SuccessMessage;
            return View(Modal);
        }
    }
}