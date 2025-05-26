using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using ClassLibrary.Dto.Category;
using BlazorApp.Services;


namespace BlazorApp.Pages.Category
{
    public partial class UpdateCategory
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ICategoryService CategoryService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }
        public CategoryDto Category = null;
        public UpdateCategoryDto CategoryData { set; get; } = new();
        public JsonNode Errors { set; get; } = new JsonObject();

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                Category = await CategoryService.GetByIdAsync(Id);
                CategoryData.Name = Category?.Name;
                CategoryData.IsIncome = Category.IsIncome;
                CategoryData.Description = Category.Description;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }


        protected async Task SaveCategory()
        {
            var response = await CategoryService.UpdateAsync(Id, CategoryData);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Categories");
            }
            else
            {
                var strResponse = await response.Content.ReadAsStringAsync();
                try
                {
                    var jsonResponse = JsonNode.Parse(strResponse);
                    Errors = jsonResponse?["errors"] ?? new JsonObject();

                }
                catch
                {
                    Console.WriteLine("Category save error.");
                }
            }

        }
    }
}
