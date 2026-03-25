using CentralStore.Shared.Dtos.Customers;

namespace CentralStore.Shared.Requests.Central
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

    public static class CustomerMappings
    {
        public static CustomerDto ToDto(this CreateCustomerRequest request)
            => new CustomerDto(id: Guid.NewGuid(),
               firstName: request.FirstName,
               lastName: request.LastName,
               email: request.Email,
               password: request.Password,
               createdAt: DateTime.UtcNow,
               updatedAt: DateTime.UtcNow,
               storeId: request.StoreId,
               concurrencyToken: Guid.NewGuid()
               );

        public static CreateCustomerRequest ToCreateRequest(this CustomerDto customerDto, Guid storeId)
            => new CreateCustomerRequest(
                FirstName: customerDto.FirstName,
                LastName: customerDto.LastName,
                Email: customerDto.Email,
                Password: customerDto.Password,
                StoreId: storeId);
    }
}
