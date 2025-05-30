using BlazorApp.Models.Category;
using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Category;


public partial class Index
{
    [Inject] private ICategoryService CategoryService { get; set; } = default!;
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;

    [Parameter] public int Id { set; get; }
    private IEnumerable<CategoryModel>? Categories;
    protected bool showDeleteModal = false;

    protected void ShowDeleteModal(int categoryId) { showDeleteModal = true; Id = categoryId; }
    protected void HideDeleteModal() => showDeleteModal = false;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            Categories = await CategoryService.GetAllAsync();
        }
        catch
        {
            NavigationManager.NavigateTo("/login");
            return;
        }
    }

    protected async Task HandleDelete()
    {
        var response = await CategoryService.DeleteAsync(Id);
        NavigationManager.NavigateTo("/Categories");
        showDeleteModal = false;
        await OnInitializedAsync();
    }
}