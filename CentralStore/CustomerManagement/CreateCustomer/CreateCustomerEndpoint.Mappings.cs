using CentralStore.Domain;
using CentralStore.Shared.Dtos.Users;
using MassTransit;

namespace CentralStore.AdminManagement.CreateUser
{
    public record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    Guid StoreId);

    public record CreateCustomerResponse(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Password,
    DateTime CreatedAt,
    DateTime UpdateAt,
    Guid StoreId,
    Guid ConcurrencyToken);

    public static class CreateCustomerEndpointMappings
    {
        public static CustomerDto ToDto(this CreateCustomerRequest request)
            => new CustomerDto(id: NewId.NextSequentialGuid(),
               firstName: request.FirstName,
               lastName: request.LastName,
               email: request.Email,
               password: request.Password,
               createdAt: DateTime.UtcNow,
               updatedAt: DateTime.UtcNow,
               concurrencyToken: Guid.NewGuid()
               );

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
