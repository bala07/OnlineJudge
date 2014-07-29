using System.Web.Mvc;
using System.Web.Routing;

namespace Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ProblemsList",
                url: "Problems/List",
                defaults: new { controller = "Problem", action = "List" });


            routes.MapRoute(
                name: "ProblemDetails", 
                url: "Problems/{problemCode}", 
                defaults: new { controller = "Problem", action = "GetProblemDetails", problemCode = UrlParameter.Optional });

            routes.MapRoute(
                name: "Submit",
                url: "Submit/{problemCode}",
                defaults: new { controller = "Submission", action = "Submit", problemCode = UrlParameter.Optional });

        
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}