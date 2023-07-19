using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProHealth.Helpers;

/// <summary>
/// Authorizes endpoints. 
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
internal sealed class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// Authorizes a user.
    /// </summary>
    /// <param name="context">Context of method or class being authorised.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        
    }
}
