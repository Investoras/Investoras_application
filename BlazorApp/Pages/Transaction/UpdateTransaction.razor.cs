using Microsoft.AspNetCore.Components;
using System.Text.Json;
using BlazorApp.Models.Transaction;
using BlazorApp.Services;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Pages.Transaction
{
    public partial class UpdateTransaction
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [Inject] private ITransactionService TransactionService { get; set; } = default!;

        [Parameter] public int Id { get; set; }

        public bool IsLoading { get; set; } = true;
        public bool IsSubmitting { get; set; } = false;
        public UpdateTransactionModel TransactionData { get; set; } = new();
        public List<string> ServerErrors { get; set; } = new();
        public DateTime TransactionDateLocal
        {
            get => TransactionData.CreatedAt.Kind == DateTimeKind.Utc
                ? TransactionData.CreatedAt.ToLocalTime()
                : TransactionData.CreatedAt;
            set => TransactionData.CreatedAt = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        protected override async Task OnParametersSetAsync()
        {
            var transaction = await TransactionService.GetTransactionByIdAsync(Id);

            if (transaction != null)
            {
                TransactionData = new UpdateTransactionModel
                {
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    AccountId = transaction.AccountId,
                    CategoryId = transaction.CategoryId,
                    CreatedAt = transaction.Date.Kind == DateTimeKind.Utc
                            ? transaction.Date
                            : DateTime.SpecifyKind(transaction.Date, DateTimeKind.Utc)
                };
            }
            else
            {
                ServerErrors.Add("Ошибка обновления: проверьте правильность введённых данных.");
            }

            IsLoading = false;
        }

        protected async Task SaveTransaction()
        {
            ServerErrors.Clear();
            IsSubmitting = true;

            try
            {
                await TransactionService.UpdateTransactionAsync(Id, TransactionData);
                NavigationManager.NavigateTo("/Transactions");
            }
            catch (Exception ex)
            {
                ServerErrors.Add($"Ошибка обновления. {ex}");
            }
            finally
            {
                IsSubmitting = false;
            }
        }
    }
}
