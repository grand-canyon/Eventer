namespace Eventer.Web.Constraints
{
    using System;
    using System.Web;
    using System.Web.Routing;

    public class DateConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values,
                          RouteDirection routeDirection)
        {
            DateTime dateTime;

            return DateTime.TryParse(values[parameterName] as string, out dateTime);
        }
    }
}