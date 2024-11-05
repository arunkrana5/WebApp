using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System;
using System.Linq;
using System.Web.Mvc;
using Website.CommonClass;

namespace Website.Controllers
{

    [CheckLoginFilter]
    public class OnboardingController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IOnboardingHelper onboard;

        public OnboardingController()
        {

            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            onboard = new OnboardingModal();

        }

        public ActionResult OnboardingList(string src)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            MyTab.Approval Modal = new MyTab.Approval();
            Modal.Approved = 0;
            Modal.LoginID = LoginID;
            return View(Modal);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult _OnboardingList(string src, JqueryDatatableParam param)
        {
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];

            param.SearchText = Request.Form["search[value]"];
            param.sortColumn = Convert.ToInt32(Request.Form["order[0][column]"]);
            param.sortOrder = Request.Form["order[0][dir]"];
            param.LoginID = LoginID;
            param.Approved = Convert.ToInt32(Request.Form["Approved"]);


            var Result = onboard.GetOnboarding_Applications_List(param);

            int recordTotal = Result.Count > 0 ? Result.Select(x => x.TotalCount).FirstOrDefault() : 0;

            return Json(new
            {
                draw = param.draw,
                recordsFiltered = recordTotal,
                recordsTotal = recordTotal,
                aaData = Result
            }, JsonRequestBehavior.AllowGet); ;

        }

        [HttpPost]
        public ActionResult OnboardingApprove(string src, FormCollection form, string Command)
        {
            PostResponse Result = new PostResponse();
            ViewBag.src = src;
            string[] GetQueryString = ClsApplicationSetting.DecryptQueryString(src);
            ViewBag.GetQueryString = GetQueryString;
            ViewBag.MenuID = GetQueryString[0];
            int Approved = 0;
            int.TryParse(Command, out Approved);
            string AppID = form["OnBoardIDs"].ToString();
            if (!string.IsNullOrEmpty(AppID))
            {
                Onboarding.ApprovalAction Modal = new Onboarding.ApprovalAction();
                Modal.Approved = Approved;
                Modal.ApprovedRemarks = form["ApproveRemarks"].ToString();
                if (string.IsNullOrEmpty(Modal.ApprovedRemarks))
                {
                    Result.SuccessMessage = "Remarks can't be blank";
                    return Json(Result, JsonRequestBehavior.AllowGet);
                }
                Modal.LoginID = LoginID;
                Modal.IPAddress = IPAddress;
                Modal.IDs = AppID.ToString();
                Result = onboard.fnSetOnboarding_Approval(Modal);
            }
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

    }
}