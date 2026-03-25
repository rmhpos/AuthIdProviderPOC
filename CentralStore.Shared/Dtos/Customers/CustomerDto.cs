using CentralStore.Shared.Dtos.Admin.Customers;

namespace CentralStore.Shared.Dtos.Customers
{
    public record CustomerDto : CustomerDtoBase
    {
        public CustomerDto(): base() { }

        public CustomerDto(Guid id,
            string firstName,
            string lastName,
            string email,
            string password,
            DateTime createdAt,
            DateTime updatedAt,
            Guid storeId,
            Guid concurrencyToken)
            : base(id, firstName, lastName, email, password, createdAt, updatedAt, storeId, concurrencyToken)
        {
            StoreId = storeId;
        }

        public Guid StoreId { get; set; }
    }
}
