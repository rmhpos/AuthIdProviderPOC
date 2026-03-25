using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace CentralStore.CentralManager.Infrastructure
{
    public class RmhClaimsPrincipalFactory
    : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public RmhClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor)
            : base(accessor) { }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            if (account == null)
                return new ClaimsPrincipal(new ClaimsIdentity());

            var user = await base.CreateUserAsync(account, options);
            var identity = (ClaimsIdentity)user.Identity!;

            // AdditionalProperties may also be null
            if (account.AdditionalProperties == null)
                return user;

            Console.WriteLine(JsonSerializer.Serialize(account.AdditionalProperties));

            if (account.AdditionalProperties.TryGetValue("realm_access", out var realm))
            {
                var realmRoles = ((JsonElement)realm)
                    .GetProperty("roles")
                    .EnumerateArray()
                    .Select(r => r.GetString());

                foreach (var role in realmRoles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, role!));
            }

            if (account.AdditionalProperties.TryGetValue("resource_access", out var resource))
            {
                var clientRoles = ((JsonElement)resource)
                    .GetProperty("central-api")
                    .GetProperty("roles")
                    .EnumerateArray()
                    .Select(r => r.GetString());

                foreach (var role in clientRoles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, role!));
            }

            return user;
        }
    }
}