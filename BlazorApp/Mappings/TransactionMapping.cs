using BlazorApp.Models.Transaction;
using ClassLibrary.Dto.Transaction;
using BlazorApp.Models.Category;
using BlazorApp.Services;

namespace BlazorApp.Mappings
{
    public static class TransactionMappings
    {
        public static async Task<TransactionModel> ToModelAsync(
            this TransactionDto dto,
            ICategoryService categoryService)
        {
            var category = await categoryService.GetByIdAsync(dto.CategoryId);

            return new TransactionModel
            {
                TransactionId = dto.TransactionId,
                Date = dto.Date,
                Amount = dto.Amount,
                Description = dto.Description,
                AccountId = dto.AccountId,
                CategoryId = dto.CategoryId,
                IsIncome = category?.IsIncome ?? false
            };
        }

        public static async Task<List<TransactionModel>> ToModelAsync(
            this List<TransactionDto> dtos,
            ICategoryService categoryService)
        {
            var tasks = dtos.Select(dto => dto.ToModelAsync(categoryService));
            return (await Task.WhenAll(tasks)).ToList();
        }

        public static CreateTransactionDto ToDto(this CreateTransactionModel model)
        {
            return new CreateTransactionDto
            {
                Amount = model.Amount,
                Description = model.Description,
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                Date = model.Date
            };
        }

        public static UpdateTransactionDto ToDto(this UpdateTransactionModel model)
        {
            return new UpdateTransactionDto
            {
                Amount = model.Amount,
                Description = model.Description,
                AccountId = model.AccountId,
                CategoryId = model.CategoryId,
                Date = model.CreatedAt
            };
        }
    }
}
