using BlazorApp.Models.User;

namespace BlazorApp.Services
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string? Token { get; }
        int? UserId { get; }

        event Action? OnAuthStateChanged;

        int GetUserId();
        Task<bool> LoginAsync(LoginUserModel loginModel);
        Task LogoutAsync();
        Task RegisterAsync(CreateUserModel createUserModel);
        Task TryRestoreSessionAsync();
    }
}