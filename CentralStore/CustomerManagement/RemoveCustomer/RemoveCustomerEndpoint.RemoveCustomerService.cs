using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Customers;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.CustomerManagement.RemoveCustomer
{
    public class RemoveCustomerService : ServiceBase, IRemoveCustomerService
    {
        private readonly CentralStoreDbContext _dbContext;

        public RemoveCustomerService(CentralStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> GetCustomerAsync(RemoveCustomerRequest request)
          => await _dbContext.Customers
            .AsNoTracking()
            .Where(c => c.Id == request.Id
            && c.ConcurrencyToken == request.ConcurrencyToken)
            .SingleOrDefaultAsync();

        public async Task<int> RemoveCustomerAsync(Guid customerId)
          => await _dbContext.Customers
            .Where(c => c.Id == customerId)
            .ExecuteDeleteAsync();

        public async Task<Customer> CreateCustomerAsync(CustomerDto dto)
        {
            var customer = dto.ToEntity();
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();

            return customer;
        }
    }
}
