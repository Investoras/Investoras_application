using ClassLibrary.Dto.User;

namespace BlazorApp.Services;


public interface IAuthService
{
    Task<bool> LoginAsync(LoginUserDto loginModel);
    Task RegisterAsync(CreateUserDto loginModel);
    Task LogoutAsync();
    bool IsAuthenticated { get; }
    int? UserId { get; }
}