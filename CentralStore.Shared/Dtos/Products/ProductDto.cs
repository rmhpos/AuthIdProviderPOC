namespace CentralStore.Shared.Dtos.Products
{
    public record ProductDto : ProductDtoBase
    {
        public ProductDto() { }

        public ProductDto(Guid id,
            string name,
            string description,
            decimal price,
            decimal minPrice,
            DateTime createdAt,
            DateTime updatedAt,
            Guid storeId,
            Guid concurrencyToken)
            : base(id, name, description, price, minPrice, createdAt, updatedAt, concurrencyToken)
        {
            StoreId = storeId;
        }

        public Guid StoreId { get; set; }
    }
}
