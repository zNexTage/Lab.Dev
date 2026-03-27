using Lab.MVC.AppSemTemplate.Data;
using Lab.MVC.AppSemTemplate.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lab.MVC.AppSemTemplate.Configuration;

public static class DbMigrationHelper {
    public static async Task EnsureSeedData(WebApplication serviceScope) {
        var services = serviceScope.Services.CreateScope().ServiceProvider;
        
        await EnsureSeedData(services);
    }

    private static async Task EnsureSeedData(IServiceProvider serviceProvider) {
        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();

        var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if(env.IsDevelopment() || env.IsEnvironment("Docker"))
        {
            await context.Database.EnsureCreatedAsync();
            await EnsureSeedProducts(context);

            await EnsureSeedUsers(context);
        }
    }

    private static async Task EnsureSeedProducts(AppDbContext context)
    {
        if(context.Produto.Any()) return;

        context.Produto.AddRange(new List<Produto> {
            new Produto { Name = "Livro CSS", Valor = 10.0m, Image = "CSS.jpg", },
            new Produto { Name = "Livro JavaScript", Valor = 9.5m, Image = "JavaScript.jpg" },
            new Produto { Name = "Livro Python", Valor = 8.0m, Image = "Python.jpg" },
            new Produto { Name = "Livro C#", Valor = 8.0m, Image = "CSharp.jpg" }
        });

        await context.SaveChangesAsync();
    }
    private static async Task EnsureSeedUsers(AppDbContext context)
    {
        if(context.Users.Any()) return;

        var userId = Guid.NewGuid().ToString();

        await context.Users.AddAsync(new IdentityUser()
         { 
            Id = userId,
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@example.com",
            NormalizedEmail = "ADMIN@EXAMPLE.COM",
            EmailConfirmed = true,
            PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin123!"),
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = Guid.NewGuid().ToString("D"),
            PhoneNumberConfirmed = false,
            TwoFactorEnabled = false,
            LockoutEnabled = true,
            AccessFailedCount = 0
        });
        
        await context.SaveChangesAsync();

        if(context.UserClaims.Any()) return;

        var adminUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        await context.UserClaims.AddAsync(new IdentityUserClaim<string>()
        {
            UserId = adminUser.Id,
            ClaimType = "Produtos",
            ClaimValue = "VI,ED,AD,EX"
        });

        await context.SaveChangesAsync();
    }
}