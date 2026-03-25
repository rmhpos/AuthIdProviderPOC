using CentralStore.LocalStore.Domain;
using Microsoft.AspNetCore.Identity;

namespace CentralStore.LocalStore.AdminManagement
{
    public static class RegisterEndpoint
    {
        public record RegisterRequest(string Email, string Password, string ConfirmPassword);

        public static void Map(WebApplication app)
        {
            app.MapPost("/register", async (RegisterRequest request,
                UserManager<LocalStoreUser> userManager) =>
            {
                if (request.Password != request.ConfirmPassword)
                    return Results.BadRequest("Passwords do not match.");

                var existingUser = await userManager.FindByEmailAsync(request.Email);

                if (existingUser != null)
                    return Results.BadRequest("Email is already in use.");

                var user = new LocalStoreUser
                {
                    UserName = request.Email,
                    Email = request.Email
                };

                var result = await userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return Results.BadRequest(result.Errors.Select(e => e.Description));

                await userManager.AddToRoleAsync(user, "rmh.products.read");
                return Results.Ok("User registered successfully.");
            }).WithTags("Users");
        }
    }
}
