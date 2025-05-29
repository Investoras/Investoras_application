using BlazorApp.Models;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.Category;

public static class TransactionMapper
{
    public static TransactionModel ToModel(this TransactionDto dto, CategoryDto? category = null)
    {
        return new TransactionModel
        {
            TransactionId = dto.TransactionId,
            Date = dto.Date,
            Amount = dto.Amount,
            Description = dto.Description,
            AccountId = dto.AccountId,
            CategoryId = dto.CategoryId,
            CategoryName = category?.Name ?? "N/A",
            IsIncome = category?.IsIncome ?? false
        };
    }

    public static CreateTransactionDto ToCreateDto(this TransactionModel model)
    {
        return new CreateTransactionDto
        {
            Amount = model.Amount,
            Description = model.Description,
            AccountId = model.AccountId,
            CategoryId = model.CategoryId
        };
    }

    public static UpdateTransactionDto ToUpdateDto(this TransactionModel model)
    {
        return new UpdateTransactionDto
        {
            Amount = model.Amount,
            Description = model.Description,
            AccountId = model.AccountId,
            CategoryId = model.CategoryId
        };
    }
}
