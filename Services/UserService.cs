using Microsoft.EntityFrameworkCore;
using hub_events.Data;
using hub_events.Models;

namespace hub_events.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    
    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return null;
            
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email == email && u.IsActive);
            
        // Kiểm tra nếu không tìm thấy người dùng
        if (user == null)
            return null;
            
        // Kiểm tra mật khẩu
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;
            
        return user;
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .SingleOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<bool> RegisterAsync(User user, string password)
    {
        // Kiểm tra người dùng đã tồn tại
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return false;
            
        // Mã hóa mật khẩu
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        // Thêm người dùng mới
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return true;
    }
} 