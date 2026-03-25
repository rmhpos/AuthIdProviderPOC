using CentralStore.Domain;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.Shared
{
  public static class ProductMappings
  {
    public static ProductDto ToDto(this Product product)
      => new ProductDto(id: product.Id,
        name: product.Name,
        description: product.Description,
        price: product.Price,
        minPrice: product.MinPrice,
        createdAt: product.CreatedAt,
        updatedAt: product.UpdatedAt,
        storeId: product.StoreId,
        concurrencyToken: product.ConcurrencyToken);

    public static Product ToEntity(this ProductDto productDto)
      => new Product()
      {
        Id = productDto.Id,
        Name = productDto.Name,
        Description = productDto.Description,
        Price = productDto.Price,
        MinPrice = productDto.MinPrice,
        CreatedAt = productDto.CreatedAt,
        UpdatedAt = productDto.UpdatedAt,
        ConcurrencyToken = productDto.ConcurrencyToken
      };
  }
}
