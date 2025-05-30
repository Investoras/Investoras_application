using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using BlazorApp.Models.Category;
using BlazorApp.Services;
using BlazorApp.Models.Transaction;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.User;
using static System.Net.WebRequestMethods;


namespace BlazorApp.Pages.Category
{
    public partial class UpdateCategory
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }
        public CategoryModel Category = null;
        public UpdateCategoryModel CategoryData { set; get; } = new();
        public List<string> ServerErrors { get; set; } = new();

        protected override async Task OnParametersSetAsync()
        {
            var category = await CategoryService.GetByIdAsync(Id);
            if (category != null)
            {
                CategoryData = new UpdateCategoryModel
                {
                    Name = category?.Name,
                    IsIncome = category.IsIncome,
                    Description = category.Description
                };
            }
            else
            {
                ServerErrors.Add("Ошибка категории.");
            }
        }

        protected async Task SaveCategory()
        {
            ServerErrors.Clear();
            try
            {
                var response = await CategoryService.UpdateAsync(Id, CategoryData);
                if (response.IsSuccessStatusCode)
                {
                    NavigationManager.NavigateTo("/Categories");
                }
                else
                {
                    ServerErrors.Add("Ошибка операции.");
                }
            }
            catch
            {
                ServerErrors.Add("Ошибка.");
            }
        }
    }
}
