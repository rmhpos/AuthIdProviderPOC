using CentralStore.LocalStore.Domain;
using Microsoft.AspNetCore.Identity;

namespace CentralStore.LocalStore.AdminManagement
{
    public static class LogoutEndpoint
    {
        public static void Map(WebApplication app)
        {
            app.MapPost("/logout", async (SignInManager<LocalStoreUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return Results.Ok("Logged out successfully.");
            })
            .RequireAuthorization()
            .WithTags("Users");
        }
    }
}
