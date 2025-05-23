using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Layout;

public partial class MainLayout 
{
    [Inject]
    private IAuthService AuthService { get; set; } = default!;

    [Inject]
    private NavigationManager NavManager { get; set; } = default!;

    private async Task Logout()
    {
        await AuthService.LogoutAsync();
        NavManager.NavigateTo("/");
    }
}
