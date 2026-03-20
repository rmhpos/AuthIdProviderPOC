using CentralStore.Domain;
using CentralStore.Shared.Requests.Central;

namespace CentralStore.AdminManagement.CreateUser
{
    public static class CreateCustomerEndpointMappings
    {
        public static CreateCustomerResponse ToResponse(this Customer customer)
            => new CreateCustomerResponse(Id: customer.Id,
              FirstName: customer.FirstName,
              LastName: customer.LastName,
              Email: customer.Email,
              Password: customer.Password,
              CreatedAt: customer.CreatedAt,
              UpdateAt: customer.UpdatedAt,
              StoreId: customer.StoreId,
              ConcurrencyToken: customer.ConcurrencyToken);
    }
}
