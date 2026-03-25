using CentralStore.Shared.Entities;

namespace CentralStore.Domain
{
  public class Product : ProductBase
  {
    public Guid StoreId { get; set; }
  }
}
