using BlazorApp.Models.Category;
using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Category;


public partial class Index
{
    [Inject] private ICategoryService CategoryService { get; set; } = default!;

    private IEnumerable<CategoryModel>? Categories;

    protected override async Task OnInitializedAsync()
    {
        Categories = await CategoryService.GetAllAsync();
    }
}