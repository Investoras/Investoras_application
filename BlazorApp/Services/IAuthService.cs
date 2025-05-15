using ClassLibrary.Dto;

namespace BlazorApp.Services;


public interface IAuthService
{
    Task<bool> LoginAsync(UserDto loginModel);
    Task RegisterAsync(UserDto loginModel);
    Task LogoutAsync();
    bool IsAuthenticated { get; }
    int? UserId { get; }
}