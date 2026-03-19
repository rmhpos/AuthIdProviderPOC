using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Users;

namespace CentralStore.CustomerManagement.RemoveCustomer
{
    public interface IRemoveCustomerService : IService
    {
        Task<Customer?> GetCustomerAsync(RemoveCustomerRequest request);
        Task<int> RemoveCustomerAsync(Guid customerId);
        Task<Customer> CreateCustomerAsync(CustomerDto dto);
    }
}
