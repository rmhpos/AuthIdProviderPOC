using CentralStore.Domain;
using CentralStore.Shared;

namespace CentralStore.CustomerManagement.UpdateCustomer
{
    public interface IUpdateCustomerService : IService
    {
        Task<bool> IsConflictAsync(Guid id, Guid concurrencyToken);
        Task<int> UpdateCustomerApiAsync(UpdateCustomerDto dto, Guid storeId);
        Task<Customer?> GetById(Guid id);
    }
}
