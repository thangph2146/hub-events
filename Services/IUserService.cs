using hub_events.Models;

namespace hub_events.Services;

public interface IUserService
{
    Task<User?> AuthenticateAsync(string email, string password);
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<bool> RegisterAsync(User user, string password);
} 