using CentralStore.Domain;
using MassTransit;
using CentralStore.Shared.Messages;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.ProductManagement.CreateProduct
{
  public record CreateProductRequest(
  string Name,
  string Description,
  decimal Price,
  decimal MinPrice,
  Guid StoreId);

  public record CreateProductResponse(Guid Id,
    string Name,
    string Description,
    decimal Price,
    decimal MinPrice,
    DateTime CreatedAt,
    DateTime UpdateAt,
    Guid StoreId,
    Guid ConcurrencyToken);

  public static class CreateProductsMappings
  {
    public static CreateProductResponse ToResponse(this Product product)
      => new CreateProductResponse(Id: product.Id,
        Name: product.Name,
        Description: product.Description,
        Price: product.Price,
        MinPrice: product.MinPrice,
        CreatedAt: product.CreatedAt,
        UpdateAt: product.UpdatedAt,
        StoreId: product.StoreId,
        ConcurrencyToken: product.ConcurrencyToken);

    public static ProductDto ToDto(this CreateProductRequest request)
      => new ProductDto(id: NewId.NextSequentialGuid(),
        name: request.Name,
        description: request.Description,
        price: request.Price,
        minPrice: request.MinPrice,
        createdAt: DateTime.UtcNow,
        updatedAt: DateTime.UtcNow,
        concurrencyToken: Guid.NewGuid()
        );

    public static CreateProductMessage ToMessage(this ProductDto productDto)
      => new CreateProductMessage(CurrentState: productDto);
  }
}
