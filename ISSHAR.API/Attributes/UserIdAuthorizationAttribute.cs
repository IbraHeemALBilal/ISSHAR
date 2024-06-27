using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

public class UserIdAuthorizationAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _userIdParamName;

    public UserIdAuthorizationAttribute(string userIdParamName)
    {
        _userIdParamName = userIdParamName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userIdFromToken = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userIdFromToken == null)
        {
            context.Result = new ForbidResult();
            return;
        }
        var userIdFromRoute = context.RouteData.Values[_userIdParamName]?.ToString();
        if (userIdFromRoute == null || userIdFromToken != userIdFromRoute)
        {
            context.Result = new ForbidResult();
        }
    }
}
