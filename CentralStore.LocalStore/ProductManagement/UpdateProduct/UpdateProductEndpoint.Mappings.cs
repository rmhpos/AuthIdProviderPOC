using CentralStore.Shared.Dtos.Products;
using CentralStore.Shared.Messages;

namespace CentralStore.LocalStore.ProductManagement.UpdateProduct
{
  public record UpdateProductRequest(Guid Id,
  string Name,
  string Description,
  decimal Price,
  decimal MinPrice,
  Guid ConcurrencyToken);

  public static class UpdateProductMappings
  {
    public static ProductDto ToDto(this UpdateProductRequest request)
      => new ProductDto(
        id: request.Id,
        name: request.Name,
        description: request.Description,
        price: request.Price,
        minPrice: request.MinPrice,
        createdAt: DateTime.UtcNow, // or fetch from DB if updating
        updatedAt: DateTime.UtcNow,
        concurrencyToken: request.ConcurrencyToken);

    public static UpdateProductMessage ToMessage(ProductDto previousState, ProductDto currentState)
      => new UpdateProductMessage(previousState, currentState);
  }
}
