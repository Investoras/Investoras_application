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

        // Другие отображения, если они есть
        CreateMap<User, UserDto>();

        CreateMap<UpdateUserDto, User>();
        // ... другие маппинги
    }
}
