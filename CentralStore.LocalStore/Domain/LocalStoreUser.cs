using Microsoft.AspNetCore.Identity;

namespace CentralStore.LocalStore.Domain
{
    public class LocalStoreUser : IdentityUser
    {
        public string? AuthorizationPermissionsFile { get; set; }
    }
}
