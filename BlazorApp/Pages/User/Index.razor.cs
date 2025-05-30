using ClassLibrary.Dto.User;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.User;


public partial class Index
{
    [Inject] private HttpClient Http { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IAuthService AuthService { get; set; } = default!;

    private int? UserId { get; set; }
    // FOR TESTING - TO ACCESS USER MANAGEMENT FUNCTION
    private readonly bool Superuser = false;

    private List<UserDto>? users;


    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (!Superuser)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            UserId = AuthService.GetUserId();
            users = await Http.GetFromJsonAsync<List<UserDto>>("User/All");
        }
        catch
        {
            NavigationManager.NavigateTo("/login");
            return;
        }
    }
}