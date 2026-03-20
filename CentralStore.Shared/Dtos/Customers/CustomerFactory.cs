using CentralStore.Shared.Dtos.Users;

namespace CentralStore.Shared.Dtos.Admin
{
    public static class CustomerFactory
    {
        public static CustomerDto CreateDefault()
        {
            var now = DateTime.UtcNow;

            return new CustomerDto(
                id: Guid.NewGuid(),
                firstName: string.Empty,
                lastName: string.Empty,
                email: string.Empty,
                password: string.Empty,
                createdAt: now,
                updatedAt: now,
                storeId: Guid.Empty,
                concurrencyToken: Guid.NewGuid()
            );
        }
    }
}