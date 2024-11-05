using System.Web.Mvc;
using System.Web.Routing;

namespace Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
          name: "DashboardRoute",
          url: "",
          defaults: new { controller = "Home", action = "Dashboard" }
      );

            routes.MapRoute(
              name: "LogoutRoute",
              url: "Logout",
              defaults: new { controller = "Accounts", action = "Logout" }
          );

            routes.MapRoute(
              name: "LoginRoute",
              url: "Login",
              defaults: new { controller = "Accounts", action = "Login" }
          );

            routes.MapRoute(
              name: "DeactivateMyAccountRoute",
              url: "DeactivateMyAccount",
              defaults: new { controller = "Accounts", action = "DeactivateMyAccount" }
          );

            routes.MapRoute(
             name: "ProfileRoute",
             url: "Profile",
             defaults: new { controller = "Home", action = "MyProfile" }
         );
            routes.MapRoute(
             name: "MyDocRoute",
             url: "MyDoc",
             defaults: new { controller = "Home", action = "MyDoc" }
         );
            routes.MapRoute(
             name: "YTDGraphRoute",
             url: "YTDGraph",
             defaults: new { controller = "Home", action = "YTDGraph" }
         );

            routes.MapRoute(
          name: "ChangePasswordRoute",
          url: "ChangePassword",
          defaults: new { controller = "Home", action = "ChangePassword" }
      );


            routes.MapRoute(
          name: "SalarySlipRoute",
          url: "SalarySlip",
          defaults: new { controller = "Home", action = "SalarySlip" }
      );

            routes.MapRoute(
          name: "ReleaseLogRoute",
          url: "ReleaseLog",
          defaults: new { controller = "Home", action = "ReleaseLog" }
      );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
