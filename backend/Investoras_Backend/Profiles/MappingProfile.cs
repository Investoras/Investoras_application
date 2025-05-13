namespace Investoras_Backend;
// Profiles/MappingProfile.cs
using AutoMapper;
using Investoras_Backend.Data.Dto;
using Investoras_Backend.Data.Entities;
using Investoras_Backend.Data.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, UserModel>();
        CreateMap<UpdateUserDto, UserModel>();
        CreateMap<UpdateUserDto, User>();
        CreateMap<UserModel, User>();
        CreateMap<UserModel, UpdateUserDto>();
        CreateMap<UserDto, UserModel>();
        CreateMap<User, UserModel>();

        CreateMap<CreateTransactionDto, TransactionModel>();
        CreateMap<UpdateTransactionDto, TransactionModel>();
        CreateMap<UpdateTransactionDto, Transaction>();
        CreateMap<TransactionModel, Transaction>();
        CreateMap<TransactionModel, UpdateTransactionDto>();
        CreateMap<TransactionDto, TransactionModel>();
        CreateMap<Transaction, TransactionModel>();

        CreateMap<CreateAccountDto, AccountModel>();
        CreateMap<UpdateAccountDto, AccountModel>();
        CreateMap<UpdateAccountDto, Account>();
        CreateMap<AccountModel, Account>();
        CreateMap<AccountModel, UpdateAccountDto>();
        CreateMap<AccountDto, AccountModel>();
        CreateMap<Account, AccountModel>();

        CreateMap<CreateCategoryDto, CategoryModel>();
        CreateMap<UpdateCategoryDto, CategoryModel>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<CategoryModel, Category>();
        CreateMap<CategoryModel, UpdateCategoryDto>();
        CreateMap<CategoryDto, CategoryModel>();
        CreateMap<Category, CategoryModel>();
    }
}
