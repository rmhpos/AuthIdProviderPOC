using CentralStore.LocalStore.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace CentralStore.LocalStore.AdminManagement
{
    public static class LoginEndpoint
    {
        public record LoginRequest(string Email, string Password, bool RememberMe);

        public static void Map(WebApplication app)
        {
            app.MapPost("/login", async (LoginRequest request,
                SignInManager<LocalStoreUser> signInManager,
                IConfiguration config) =>
            {
                var user = await signInManager.UserManager.FindByEmailAsync(request.Email);

                if(user is null || !(await signInManager.UserManager.CheckPasswordAsync(user, request.Password))) 
                    return Results.Unauthorized();

                var result = await signInManager.PasswordSignInAsync(request.Email,
                    request.Password,
                    request.RememberMe,
                    false);

                if (!result.Succeeded)
                    return Results.Unauthorized();

                var roles = await signInManager.UserManager.GetRolesAsync(user);
                var secretKey = config["Jwt:SecretKey"] ?? throw new ArgumentNullException("Secret key is null or empty");
                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

                var expirationInMinutes = double.Parse(config["Jwt:ExpirationInMinutes"]!);

                List <Claim> claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(expirationInMinutes).ToString(), ClaimValueTypes.Integer64)
                };

                claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(expirationInMinutes),
                    SigningCredentials = credentials,
                    Issuer = config["Jwt:Issuer"],
                    Audience = config["Jwt:Audience"]
                };  

                var tokenHandler = new JsonWebTokenHandler();
                var accessToken = tokenHandler.CreateToken(tokenDescriptor);

                return Results.Ok(new { AccessToken = accessToken });
            }).WithTags("Users");
        }
    }
}
