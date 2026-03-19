using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Users;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.AdminManagement.CreateUser
{
    public class CreateCustomerService : ServiceBase, ICreateCustomerService
    {
        private CentralStoreDbContext _dbContext;

        public CreateCustomerService(CentralStoreDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer CreateCustomer(CustomerDto dto, Guid storeId)
        {
            var customer = dto.ToEntity();
            customer.StoreId = storeId;
            _dbContext.Customers.Add(customer);

            return customer;
        }

        public async Task<int> RemoveCustomerAsync(Guid id)
          => await _dbContext.Customers.Where(p => p.Id == id)
          .ExecuteDeleteAsync();
    }
}
