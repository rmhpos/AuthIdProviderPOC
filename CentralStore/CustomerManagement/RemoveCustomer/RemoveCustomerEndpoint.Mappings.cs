using Microsoft.AspNetCore.Mvc;

namespace CentralStore.CustomerManagement.RemoveCustomer
{
    public record RemoveCustomerRequest([FromRoute] Guid Id,
    Guid ConcurrencyToken,
    Guid StoreId);

    public static class RemoveCustomerMappings
    {
        //public static RemoveCustomerMessage ToMessage(this CustomerDto customerDto)
        //  => new RemoveCustomerMessage(PreviousState: customerDto);
    }
}
