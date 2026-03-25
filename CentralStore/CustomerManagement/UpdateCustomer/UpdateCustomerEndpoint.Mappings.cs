namespace CentralStore.CustomerManagement.UpdateCustomer
{
    public record UpdateCustomerDto(Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        DateTime UpdatedAt,
        Guid ConcurrencyToken);

    public record UpdateCustomerRequest(Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        Guid StoreId,
        Guid ConcurrencyToken);

    public static class UpdateRmhUserMappings
    {
        public static UpdateCustomerDto ToDto(this UpdateCustomerRequest request)
          => new UpdateCustomerDto(
            Id: request.Id,
            FirstName: request.FirstName,
            LastName: request.LastName,
            Email: request.Email,
            Password: request.Password,
            UpdatedAt: DateTime.UtcNow,
            ConcurrencyToken: request.ConcurrencyToken);
    }
}
