﻿using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.User;
using System.Net.Http.Json;


namespace BlazorApp.Pages.User
{
    public partial class CreateUser
    {
        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;

        public CreateUserDto UserData { get; set; } = new();

        protected async Task SaveUser()
        {
            UserData.CreatedAt = DateTime.UtcNow;
            var response = await Http.PostAsJsonAsync("https://localhost:7214/User", UserData);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Users");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json Response: \n" + strResponse);
            }
        }

    }
}
