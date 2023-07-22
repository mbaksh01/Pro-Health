using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ProHealth;

public partial class MainLayout : IAsyncDisposable
{
    bool _sideBarOpen = true;

    IJSObjectReference _module = default!;

    Dictionary<string, bool> _menuState = new Dictionary<string, bool>();

    [Inject] IJSRuntime JSRuntime { get; set; } = default!;

    [Inject] NavigationManager NavigationManager { get; set; } = default!;

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

    async Task Toggle(string item)
    {
        if (!_menuState.ContainsKey(item))
        {
            await _module.InvokeVoidAsync("collaps", item);
            _menuState.Add(item, false);
            return;
        }

        if (_menuState[item])
        {
            await _module.InvokeVoidAsync("collaps", item);
            _menuState[item] = false;
            return;
        }

        await _module.InvokeVoidAsync("expand", item);
        _menuState[item] = true;
    }

    void Navigate(string location)
    {
        NavigationManager.NavigateTo(location);
    }

    public ValueTask DisposeAsync()
    {
        return _module.DisposeAsync();
    }
}
