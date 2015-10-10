using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace SimpleWeb
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Routes.MapHttpRoute(name: "DefaultApi1", routeTemplate: "api/{controller}/{cmd}/{id}");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
