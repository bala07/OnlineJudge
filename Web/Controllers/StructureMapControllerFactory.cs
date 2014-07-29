using System.Web.Mvc;

using StructureMap;

namespace OnlineJudge.Web.Controllers
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            if (controllerType == null)
            {
                return null;
            }

            return ObjectFactory.GetInstance(controllerType) as IController;
        }
    }
}