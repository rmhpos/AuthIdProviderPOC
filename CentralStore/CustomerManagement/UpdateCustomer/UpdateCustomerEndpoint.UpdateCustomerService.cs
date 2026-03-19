using CentralStore.Domain;
using CentralStore.Shared;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.CustomerManagement.UpdateCustomer
{
    public class UpdateCustomerService : ServiceBase, IUpdateCustomerService
    {
        private CentralStoreDbContext _dbContext;

        public UpdateCustomerService(CentralStoreDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsConflictAsync(Guid id, Guid concurrencyToken)
          => await _dbContext.Customers
            .AsNoTracking()
            .AnyAsync(p => p.Id == id
            && p.ConcurrencyToken != concurrencyToken);

        public async Task<Customer?> GetById(Guid id)
          => await _dbContext.Customers
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == id);

        public async Task<int> UpdateCustomerApiAsync(UpdateCustomerDto dto, Guid storeId)
          => await _dbContext.Customers
            .Where(u => u.Id == dto.Id)
            .ExecuteUpdateAsync(setters => setters
            .SetProperty(u => u.FirstName, dto.FirstName)
            .SetProperty(u => u.LastName, dto.LastName)
            .SetProperty(u => u.Email, dto.Email)
            .SetProperty(u => u.Password, dto.Password)
            .SetProperty(u => u.UpdatedAt, DateTime.UtcNow)
            .SetProperty(u => u.StoreId, storeId)
            .SetProperty(u => u.ConcurrencyToken, Guid.NewGuid()));
    }
}
