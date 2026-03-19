using CentralStore.Shared.Dtos.Products;

namespace CentralStore.Domain
{
  public class Product : ProductBase
  {
    public Guid StoreId { get; set; }
  }
}
