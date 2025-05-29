using Microsoft.AspNetCore.Components;
using BlazorApp.Services;

namespace BlazorApp.Pages.Transaction
{
    public partial class DeleteTransaction
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private ITransactionService TransactionService { get; set; } = default!;

        [Parameter]
        public int Id { set; get; }

        protected async Task Delete_Transaction()
        {
            await TransactionService.DeleteTransactionAsync(Id);
            Navigation.NavigateTo("Transactions");
        }
    }
}