using Entities.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace PsychoQuest.Presentation.Constraints;

public class TypeTestConstraint : IRouteConstraint
{
    public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        object routeValue;
        if (values.TryGetValue(routeKey, out routeValue) && routeValue != null)
        {
            return Enum.TryParse<TypeTest>(routeValue.ToString(), out _);
        }
        return false;
    }
}