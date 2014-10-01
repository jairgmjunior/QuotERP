using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Fashion.ERP.Web.Helpers
{
    public class RouteConstraintIsEqual : IRouteConstraint
    {
        private string[] _match;

        public RouteConstraintIsEqual(string[] match)
        {
            _match = match;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _match.Any(controller => controller.Equals(values[parameterName].ToString()));
        }
    }
}