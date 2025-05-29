namespace Investoras_Backend;
// Profiles/MappingProfile.cs
using AutoMapper;
using Investoras_Backend.Data.Entities;
using ClassLibrary.Models;
using ClassLibrary.Dto.User;
using ClassLibrary.Dto.Transaction;
using ClassLibrary.Dto.Account;
using ClassLibrary.Dto.Category;

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
        CreateMap<User, UserDto>();

        CreateMap<CreateTransactionDto, TransactionModel>();
        CreateMap<UpdateTransactionDto, TransactionModel>();
        CreateMap<UpdateTransactionDto, Transaction>();
        CreateMap<TransactionModel, Transaction>();
        CreateMap<TransactionModel, UpdateTransactionDto>();
        CreateMap<TransactionDto, TransactionModel>();
        CreateMap<Transaction, TransactionModel>();
        CreateMap<Transaction, TransactionDto>();
            //.ForMember(dest => dest.IsIncome, opt => opt.MapFrom(src => src.Category != null && src.Category.IsIncome));

        CreateMap<CreateAccountDto, AccountModel>();
        CreateMap<UpdateAccountDto, AccountModel>();
        CreateMap<UpdateAccountDto, Account>();
        //CreateMap<AccountModel, Account>();
        CreateMap<AccountModel, UpdateAccountDto>();
        //CreateMap<AccountDto, AccountModel>();
        CreateMap<Account, AccountModel>();
        CreateMap<Account, AccountDto>();
        CreateMap<AccountDto, AccountModel>();
        CreateMap<AccountModel, Account>();


        CreateMap<CreateCategoryDto, CategoryModel>();
        CreateMap<UpdateCategoryDto, CategoryModel>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<CategoryModel, Category>();
        CreateMap<CategoryModel, UpdateCategoryDto>();
        CreateMap<CategoryDto, CategoryModel>();
        CreateMap<Category, CategoryModel>();
        CreateMap<Category, CategoryDto>();
    }
}
