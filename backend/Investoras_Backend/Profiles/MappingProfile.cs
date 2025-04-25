namespace Investoras_Backend;
// Profiles/MappingProfile.cs
using AutoMapper;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Добавьте это отображение
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<User, UserDto>();

        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<UpdateTransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>();

        CreateMap<CreateAccountDto, Account>();
        CreateMap<UpdateAccountDto, Account>();
        CreateMap<Account, AccountDto>();

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
    }
}
