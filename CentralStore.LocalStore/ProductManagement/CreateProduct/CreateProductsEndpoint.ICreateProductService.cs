using CentralStore.LocalStore.Domain;
using CentralStore.LocalStore.Shared;
using CentralStore.Shared.Dtos.Products;

namespace CentralStore.LocalStore.ProductManagement.CreateProduct
{
  public interface ICreateProductService : IService
  {
    Product CreateProduct(ProductDtoBase request);
    Task<int> RemoveProductAsync(Guid id);
  }
}
