using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ProHealth;

public partial class MainLayout : IAsyncDisposable
{
    bool _sideBarOpen = true;

    IJSObjectReference _module = default!;

    [Inject] IJSRuntime JSRuntime { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./MainLayout.razor.js");

        await base.OnInitializedAsync();
    }

    async Task ToggleSideBar()
    {
        if (_sideBarOpen)
        {
            await _module.InvokeVoidAsync("close");
            _sideBarOpen = false;
            return;
        }

        await _module.InvokeVoidAsync("open");
        _sideBarOpen = true;
    }

    public ValueTask DisposeAsync()
    {
        return _module.DisposeAsync();
    }
}
