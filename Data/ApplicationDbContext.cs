using Microsoft.EntityFrameworkCore;
using hub_events.Models;

namespace hub_events.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Cấu hình các thực thể và mối quan hệ
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
            
        // Seed dữ liệu mẫu với giá trị cố định
        modelBuilder.Entity<User>().HasData(
            new User 
            { 
                Id = 1, 
                Email = "admin@example.com", 
                Password = "$2a$11$I2lTejQr/ieggT/y7NRfluc1oz4hBfN9JZ3HK9lmRtWZ2C3/espr6", // admin123
                FullName = "Quản trị viên", 
                CreatedAt = new DateTime(2024, 4, 5), 
                IsActive = true 
            },
            new User
            {
                Id = 2,
                Email = "thang.ph2146@gmail.com",
                Password = "$2a$11$I2lTejQr/ieggT/y7NRfluc1oz4hBfN9JZ3HK9lmRtWZ2C3/espr6", // user123
                FullName = "Quản trị viên",
                CreatedAt = new DateTime(2025, 4, 5),
                IsActive = true
            }
        );
    }
} 