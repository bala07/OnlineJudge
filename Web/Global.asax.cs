using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using DataAccessLayer.Interfaces;

using OnlineJudge.Service.CodeExecutionService;
using OnlineJudge.Service.Interfaces;
using OnlineJudge.Web.Controllers;

using StructureMap;

using Web;

namespace OnlineJudge.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            StructureMapConfiguration();

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        private void StructureMapConfiguration()
        {
            ObjectFactory.Initialize(InitializeUsingScanning);   
        }

        private void InitializeUsingScanning(IInitializationExpression obj)
        {
            obj.Scan(
                x =>
                    {
                        x.AssemblyContainingType<IService>();
                        x.AssemblyContainingType<IRepository>();
                        x.WithDefaultConventions();
                    }
                );

            // TODO: Should find a way to generically scan all service references
            obj.For<ICodeExecutionService>().Use(new CodeExecutionServiceClient());

        }
    }
}