using BlazorApp.Models.User;
using ClassLibrary.Dto.User;

namespace BlazorApp.Mappings
{
    public static class UserMapping
    {
        public static UserModel ToModel(this UserDto dto)
        {
            return new UserModel
            {
                UserId = dto.UserId,
                Username = dto.Username,
                Email = dto.Email,
                CreatedAt = dto.CreatedAt
            };
        }

        public static List<UserModel> ToModel(this List<UserDto> dtos)
            => dtos.Select(dto => dto.ToModel()).ToList();

        public static CreateUserDto ToDto(this CreateUserModel model)
        {
            return new CreateUserDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
        }

        public static UpdateUserModel ToUpdateModel(this UserDto dto)
        {
            return new UpdateUserModel
            {
                Username = dto.Username,
                Email = dto.Email,
                Password = string.Empty
            };
        }

        public static UpdateUserDto ToDto(this UpdateUserModel model)
        {
            return new UpdateUserDto
            {
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
        }


        public static LoginUserDto ToDto(this LoginUserModel model)
        {
            return new LoginUserDto
            {
                Username = model.Username,
                Password = model.Password
            };
        }
    }
}
