using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Users;

namespace CentralStore.AdminManagement.CreateUser
{
    public interface ICreateCustomerService : IService
    {
        Customer CreateCustomer(CustomerDto dto, Guid storeId);
        Task<int> RemoveCustomerAsync(Guid id);
    }
}
