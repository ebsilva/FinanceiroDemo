using Metasoft.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Metasoft
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Messages";
            DefaultModelBinder.ResourceClassKey = "Messages";
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
        }

        protected void Application_BeginRequest()
        {
            var currentCulture = (System.Globalization.CultureInfo)CultureInfo.CurrentCulture.Clone();
            currentCulture.NumberFormat.NumberDecimalSeparator = ",";
            currentCulture.NumberFormat.NumberGroupSeparator = ".";
            currentCulture.NumberFormat.CurrencyDecimalSeparator = ",";
            Thread.CurrentThread.CurrentCulture = currentCulture;
        }
        //protected void Application_ApplicationError()
        //{
        //    switch (Context.Response.StatusCode)
        //    {

        //        case 400:
        //            /* Bad Request*/
        //            Server.ClearError();
        //            Response.End();
        //            Response.Redirect("/Home/Error");
        //            break;
        //        case 404:
        //            /* Not Found */
        //            Server.ClearError();
        //            Response.End();
        //            Response.Redirect("/Home/Error");
        //            break;
        //        case 500:
        //            /* Not Found */
        //            Server.ClearError();
        //            Response.End();
        //            Response.Redirect("/Home/Error");
        //            break;
        //    }

        //}
    }
}
