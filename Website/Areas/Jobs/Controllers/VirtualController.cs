using DataModal.Models;
using DataModal.ModelsMaster;
using DataModal.ModelsMasterHelper;
using System.Web.Mvc;

namespace Website.Areas.Jobs.Controllers
{
    public class VirtualController : Controller
    {
        long LoginID = 0;
        string IPAddress = "";
        GetResponse getResponse;
        IOnboardingHelper onboard;

        public VirtualController()
        {
            getResponse = new GetResponse();
            long.TryParse(ClsApplicationSetting.GetSessionValue("LoginID"), out LoginID);
            IPAddress = ClsApplicationSetting.GetIPAddress();
            getResponse.IPAddress = IPAddress;
            getResponse.LoginID = LoginID;
            onboard = new OnboardingModal();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        public ActionResult InfoPage()
        {
            return View();
        }

    }
}