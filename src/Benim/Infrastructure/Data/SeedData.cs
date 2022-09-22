using Benim.Domain.Interfaces;
using Benim.Features.User.Commands;
using Microsoft.AspNetCore.Identity;

namespace Benim.Infrastructure.Data;

public static class SeedData
{
    public static void AddDefaultData(IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<BenimContext>>();
        var context = services.GetService<BenimContext>();
        var userService = services.GetRequiredService<IUserService>();

        if (context is null)
        {
            logger.LogError("DbContext is null");
            return;
        }

        try
        {
            AddDefaultRoles(context);
            AddDefaultUser(userService);
        }
        catch (Exception e)
        {
            logger.LogError(e.Message);
        }
    }

    private static void AddDefaultRoles(BenimContext context)
    {
        if (context.Roles.Any()) return;
        context.Roles.Add(new IdentityRole<int>("Admin"));
        context.Roles.Add(new IdentityRole<int>("User"));
        context.SaveChanges();
    }

    private static void AddDefaultUser(IUserService userService)
    {
        var user = new RegisterUserCommand
        {
            UserName = "admin",
            Password = "Admin@1234",
            ConfirmPassword = "Admin@1234"
        };

        userService.RegisterUserAsync(user);
    }
}