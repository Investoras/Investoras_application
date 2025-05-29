using Microsoft.AspNetCore.Components;
using System.Text.Json.Nodes;
using ClassLibrary.Dto.Transaction;
using BlazorApp.Services;
using BlazorApp.Models;
using ClassLibrary.Dto.Category;


namespace BlazorApp.Pages.Transaction
{
    public partial class UpdateTransaction
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ITransactionService TransactionService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }
        public TransactionModel transaction = null;
        public TransactionModel TransactionData { set; get; } = new();
        public JsonNode Errors { set; get; } = new JsonObject();

        protected override async Task OnParametersSetAsync()
        {
            transaction = await TransactionService.GetByIdAsync(Id);
            TransactionData.Amount = transaction.Amount;
            TransactionData.Description = transaction.Description;
            TransactionData.AccountId = transaction.AccountId;
            TransactionData.CategoryId = transaction.CategoryId;
        }


        protected async Task SaveTransaction()
        {
            var response = await TransactionService.UpdateTransactionAsync(Id, TransactionData);
            if (response.IsSuccessStatusCode)
            {
                NavigationManager.NavigateTo("/Transactions");
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
