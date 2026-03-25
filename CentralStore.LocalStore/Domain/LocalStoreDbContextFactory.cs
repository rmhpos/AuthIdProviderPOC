using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CentralStore.LocalStore.Domain
{
    public class LocalStoreDbContextFactory : IDesignTimeDbContextFactory<LocalStoreDbContext>
    {
        public LocalStoreDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<LocalStoreDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

            return new LocalStoreDbContext(optionsBuilder.Options);
        }
    }
}
