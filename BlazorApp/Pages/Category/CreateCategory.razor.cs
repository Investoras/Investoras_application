using Microsoft.AspNetCore.Components;
using ClassLibrary.Dto.Category;
using BlazorApp.Services;


namespace BlazorApp.Pages.Category
{
    public partial class CreateCategory
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        public CreateCategoryDto CategoryData { get; set; } = new();

        protected async Task SaveCategory()
        {
            var response = await CategoryService.AddAsync(CategoryData);

            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Categories");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Json Response: \n" + strResponse);
            }
        }

    }
}
