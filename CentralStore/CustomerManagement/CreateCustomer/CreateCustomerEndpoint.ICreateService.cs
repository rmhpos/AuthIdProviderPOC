using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Customers;

namespace CentralStore.CustomerManagement.CreateCustomer
{
    public interface ICreateCustomerService : IService
    {
        Customer CreateCustomer(CustomerDto dto, Guid storeId);
        Task<int> RemoveCustomerAsync(Guid id);
    }
}
