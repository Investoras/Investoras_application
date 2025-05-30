using BlazorApp.Models.Account;
using ClassLibrary.Dto.Account;

namespace BlazorApp.Mappings
{
    public static class AccountMapping
    {
        public static AccountModel ToModel(this AccountDto dto) => new()
        {
            AccountId = dto.AccountId,
            Name = dto.Name,
            Balance = dto.Balance,
            UserId = dto.UserId,
            CreatedAt = dto.CreatedAt
        };

        public static List<AccountModel> ToModel(this List<AccountDto> dtos)
            => dtos.Select(d => d.ToModel()).ToList();

        public static CreateAccountDto ToDto(this CreateAccountModel model) => new()
        {
            Name = model.Name,
            Balance = model.Balance,
            UserId = model.UserId
        };

        public static UpdateAccountDto ToDto(this UpdateAccountModel model) => new()
        {
            Name = model.Name,
            Balance = model.Balance,
            UserId = model.UserId,
            CreatedAt = model.CreatedAt
        };
    }
}
