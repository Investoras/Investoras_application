using ClassLibrary.Dto;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Pages.Clients;


public partial class Index
{
    [Inject] private HttpClient Http { get; set; } = default!;

    private List<UserDto>? users;

    protected override async Task OnInitializedAsync()
    {
        users = await Http.GetFromJsonAsync<List<UserDto>>("https://localhost:7214/User/All");
    }
}