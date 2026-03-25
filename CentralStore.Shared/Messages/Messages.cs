using CentralStore.Shared.Dtos.Products;

namespace CentralStore.Shared.Messages
{
  public record CreateProductMessage(ProductDtoBase CurrentState);
  public record RemoveProductMessage(ProductDtoBase PreviousState);
  public record UpdateProductMessage(ProductDtoBase PreviousState, ProductDtoBase CurrentState);

  public record CreationFailedMessage(Guid ProductId);
  public record RemovalFailedMessage(ProductDtoBase PreviousState);
  public record UpdateFailedMessage(ProductDtoBase PreviousState);
}
