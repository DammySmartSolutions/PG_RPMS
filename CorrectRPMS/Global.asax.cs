using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;


namespace CorrectRPMS
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        public void Register(HttpConfiguration config)
        {
            //config.Routes.MapHttpRoute(
            //    name: "default",
            //    routeTemplate: "api/{controller}/{action}",
            //    defaults: new { action = RouteParameter.Optional }
            //    );

            //config.Routes.MapHttpRoute(
            //   name: "default2",
            //   routeTemplate: "api/{controller}/{action}/{id}",
            //   defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional }
            //   );



            config.Routes.MapHttpRoute(
               name: "default",
               routeTemplate: "api/{controller}/{action}",
               defaults: new { action = RouteParameter.Optional }
               );

            config.Routes.MapHttpRoute(
               name: "default2",
               routeTemplate: "api/{controller}/{action}/{id}/{CompanyID}",
               defaults: new { action = RouteParameter.Optional, id = RouteParameter.Optional, CompanyID = RouteParameter.Optional }
               );






        }
        protected void Application_PostAuthorizeRequest()
        {

            if (IsWebApiRequest())
            {
                HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);

            }
        }



        void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 5;
        }



        private bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith("~/api");
        }


    }
}