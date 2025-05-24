using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Category
{
    public partial class DeleteCategory
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }

        protected async Task Delete_Category()
        {
            var response = await CategoryService.DeleteAsync(Id);
            Navigation.NavigateTo("/Categories");
        }
    }
}