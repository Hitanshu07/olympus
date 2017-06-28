using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using UserAuth.Data.Core;

namespace UserAuth
{
  public class WebApiApplication : System.Web.HttpApplication
  {
      protected void Application_BeginRequest(object sender, EventArgs e)
      {
          if (!Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
          {
              UriBuilder builder = new UriBuilder(Request.Url);
              builder.Host = "www." + Request.Url.Host;
              Response.StatusCode = 301;
              Response.AddHeader("Location", builder.ToString());
              Response.End();
          }
      }
    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);
      FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
      RouteConfig.RegisterRoutes(RouteTable.Routes);
      BundleConfig.RegisterBundles(BundleTable.Bundles);
      //Trigger Db Context
      var firstUser = UserDbContext.Create().Users.FirstOrDefault();
    }
  }
}
