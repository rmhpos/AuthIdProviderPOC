using CentralStore.Shared.Dtos.Admin;

namespace CentralStore.Domain
{
    public class Customer : CustomerBase
    {
        public Guid StoreId { get; set; }
    }
}
