namespace CentralStore.Shared.Dtos.Products
{
    public static class ProductFactory
    {
        public static ProductDto CreateDefault()
        {
            var now = DateTime.UtcNow;

            return new ProductDto(
                id: Guid.NewGuid(),
                name: string.Empty,
                description: string.Empty,
                price: 0m,
                minPrice: 0m,
                createdAt: now,
                updatedAt: now,
                concurrencyToken: Guid.NewGuid()
            );
        }
    }
}
