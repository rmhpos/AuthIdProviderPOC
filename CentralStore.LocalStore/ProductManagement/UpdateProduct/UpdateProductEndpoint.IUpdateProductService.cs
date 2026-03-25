using CentralStore.LocalStore.Domain;
using CentralStore.LocalStore.Shared;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.LocalStore.ProductManagement.UpdateProduct
{
  public interface IUpdateProductService : IService
  {
    Task<int> UpdateProductAsync(ProductDtoBase dto);
    Task<int> UpdateProductMqAsync(ProductDtoBase dto);
    Task<Product?> GetByIdAsync(Guid id);
    Task<bool> IsConflictAsync(Guid id, Guid concurrencyToken);
  }
}
