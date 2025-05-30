using BlazorApp.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Layout
{
    public partial class NavMenu
    {
        [Inject]
        private IAuthService AuthService { get; set; } = default!;

        private bool collapseNavMenu = true;
        private string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

        protected override void OnInitialized()
        {
            AuthService.OnAuthStateChanged += AuthStateChanged;
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }




        private void AuthStateChanged()
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            AuthService.OnAuthStateChanged -= AuthStateChanged;
        }

    }
}
