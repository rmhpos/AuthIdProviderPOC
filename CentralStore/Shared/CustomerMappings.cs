using CentralStore.Domain;
using CentralStore.Shared.Dtos.Users;

namespace CentralStore.Shared
{
    public static class CustomerMappings
    {
        public static CustomerDto ToDto(this Customer customer)
            => new CustomerDto(id: customer.Id,
              firstName: customer.FirstName,
              lastName: customer.LastName,
              email: customer.Email,
              password: customer.Password,
              createdAt: customer.CreatedAt,
              updatedAt: customer.UpdatedAt,
              storeId: customer.StoreId,
              concurrencyToken: customer.ConcurrencyToken);

        public static Customer ToEntity(this CustomerDto customerDto)
          => new Customer()
          {
              Id = customerDto.Id,
              FirstName = customerDto.FirstName,
              LastName = customerDto.LastName,
              Email = customerDto.Email,
              Password = customerDto.Password,
              CreatedAt = customerDto.CreatedAt,
              UpdatedAt = customerDto.UpdatedAt,
              ConcurrencyToken = customerDto.ConcurrencyToken
          };
    }
}
