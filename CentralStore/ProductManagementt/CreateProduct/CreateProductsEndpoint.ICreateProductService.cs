using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.ProductManagement.CreateProduct
{
  public interface ICreateProductService : IService
  {
    Product CreateProduct(ProductDto dto, Guid storeId);
    Task<int> RemoveProductAsync(Guid id);
  }
}
