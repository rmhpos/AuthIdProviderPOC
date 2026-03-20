using CentralStore.Shared.Entities;

namespace CentralStore.Domain
{
    public class Customer : CustomerBase
    {
        public Guid StoreId { get; set; }
    }
}
