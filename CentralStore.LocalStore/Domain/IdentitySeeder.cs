using CentralStore.LocalStore.Domain;
using Microsoft.AspNetCore.Identity;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<LocalStoreUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Roles
        string[] roles = ["rmh.products.read", "rmh.products.write"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Reader user
        await IdentitySeeder.SeedUserAsync(userManager, "dimitar@rmhpos.com", "P@ssw0rd", new[] { "rmh.products.read" });
        // Writer user
        await IdentitySeeder.SeedUserAsync(userManager, "admin@rmhpos.com", "P@ssw0rd", new[] { "rmh.products.write" });
    }

    private static async Task SeedUserAsync(
        UserManager<LocalStoreUser> userManager,
        string email,
        string password,
        string[] roles)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            user = new LocalStoreUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            foreach (var role in roles)
            {
                await userManager.AddToRoleAsync(user, role);
            }
        }
    }
}