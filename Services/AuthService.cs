using hub_events.Models;
using Microsoft.AspNetCore.Components;

namespace hub_events.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly NavigationManager _navigationManager;
    private string? _errorMessage;

    public AuthService(IUserService userService, NavigationManager navigationManager)
    {
        _userService = userService;
        _navigationManager = navigationManager;
    }

    public async Task<bool> LoginAsync(LoginModel loginModel)
    {
        _errorMessage = null;
        
        try
        {
            var user = await _userService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            
            if (user != null)
            {
                // Đăng nhập thành công
                return true;
            }
            else
            {
                // Đăng nhập thất bại
                _errorMessage = "Email hoặc mật khẩu không đúng";
                return false;
            }
        }
        catch (Exception ex)
        {
            _errorMessage = $"Lỗi đăng nhập: {ex.Message}";
            return false;
        }
    }

    public Task<string?> GetErrorMessageAsync()
    {
        return Task.FromResult(_errorMessage);
    }
    
    public void NavigateToHome()
    {
        _navigationManager.NavigateTo("/");
    }
} 