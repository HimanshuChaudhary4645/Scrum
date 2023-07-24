using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectScrum
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {            
            //routes.MapRoute(
            //    "Product",
            //    "{ViewPrdoct}/{id}",
            //    new { controller = "Product", action = "ViewPrdoct" }
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Product", action = "ViewPrdoct", id = UrlParameter.Optional }
            );
        }
    }
}
