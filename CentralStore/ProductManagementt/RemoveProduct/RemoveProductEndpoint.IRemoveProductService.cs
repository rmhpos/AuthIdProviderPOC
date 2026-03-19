using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.ProductManagement.RemoveProduct
{
  public interface IRemoveProductService : IService
  {
    Task<Product?> GetProductAsync(RemoveProductRequest request);
    Task<int> RemoveProductAsync(Guid productId);
    Task<Product> CreateProductAsync(ProductDto dto);
  }
}
