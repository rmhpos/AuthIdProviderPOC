using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.LocalStore.Domain
{
    public class LocalStoreIdentityDbContext : IdentityDbContext<LocalStoreUser>
    {
        public LocalStoreIdentityDbContext(DbContextOptions<LocalStoreIdentityDbContext> options)
            : base(options)
        {
        }
    }
}
