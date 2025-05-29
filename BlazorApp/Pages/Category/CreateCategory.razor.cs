using Microsoft.AspNetCore.Components;
using BlazorApp.Models.Category;
using BlazorApp.Services;
using System;

namespace BlazorApp.Pages.Category
{
    public partial class CreateCategory
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        public CreateCategoryModel CategoryData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public bool IsSubmitting { get; set; } = false;

        protected async Task SaveCategory()
        {
            IsSubmitting = true;

            try
            {
                var response = await CategoryService.AddAsync(CategoryData);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/Categories");
                }
                else
                {
                    ServerErrors.Add("Category error.");
                }
            }
            catch
            {
                ServerErrors.Add("Category error.");
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
