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
                name: "ProblemSubmit",
                url: "Problem/View/{problemCode}/submit",
                defaults: new { controller = "Problem", action = "Submit" },
                constraints: new { httpMethod = new HttpMethodConstraint("POST") });

            routes.MapRoute(
                name: "Problem", 
                url: "Problem/View/{problemCode}", 
                defaults: new { controller = "Problem", action = "Get", problemCode = UrlParameter.Optional });

        
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Home", id = UrlParameter.Optional }
            );
        }
    }
}