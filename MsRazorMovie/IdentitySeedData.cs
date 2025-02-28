using Microsoft.AspNetCore.Identity;
using MsRazorMovie.Models;

public static class IdentitySeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Define roles
        string[] roleNames = { "Admin", "User" };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
                Console.WriteLine($"Role '{roleName}' created.");
            }
        }

        // Define admin accounts
        var adminUsers = new[]
        {
            new { Email = "admin1@example.com", Password = "Admin@1234" },
            new { Email = "admin2@example.com", Password = "Admin@1234" }
        };

        foreach (var admin in adminUsers)
        {
            var existingUser = await userManager.FindByEmailAsync(admin.Email);
            if (existingUser == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = admin.Email,
                    Email = admin.Email,
                    EmailConfirmed = true // Confirm email by default
                };

                var result = await userManager.CreateAsync(newUser, admin.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "Admin");
                    Console.WriteLine($"Admin user '{admin.Email}' created and assigned to Admin role.");
                }
                else
                {
                    Console.WriteLine($"Failed to create admin '{admin.Email}': {string.Join(", ", result.Errors)}");
                }
            }
            else
            {
                Console.WriteLine($"Admin user '{admin.Email}' already exists.");
            }
        }
    }

}
