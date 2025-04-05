using hub_events.Models;

namespace hub_events.Services;

public interface IAuthService
{
    Task<bool> LoginAsync(LoginModel loginModel);
    Task<string?> GetErrorMessageAsync();
    void NavigateToHome();
} 