namespace CentralStore.Shared.Dtos.Products
{
    public record ProductDto
    {
        public ProductDto() { }

        public ProductDto(Guid id,
            string name,
            string description,
            decimal price,
            decimal minPrice,
            DateTime createdAt,
            DateTime updatedAt,
            Guid concurrencyToken)
            : base()
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            MinPrice = minPrice;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            ConcurrencyToken = concurrencyToken;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal MinPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid ConcurrencyToken { get; set; }
    }
}
