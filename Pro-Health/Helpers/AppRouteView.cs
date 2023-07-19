using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ProHealth.Helpers;

public sealed class AppRouteView : RouteView
{
    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IAuthenticationService AuthenticationService { get; set; } = default!;

    protected override void Render(RenderTreeBuilder builder)
    {
        // Checks if the page has the authorize attribute
        bool authorize = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) is not null;

        if (authorize && !AuthenticationService.Authenticated)
        {
            NavigationManager.NavigateTo($"login");
        }
        else
        {
            base.Render(builder);
        }
    }
}
