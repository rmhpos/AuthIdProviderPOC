using CentralStore.Shared.Messages;
using MassTransit;

namespace CentralStore.LocalStore.ProductManagement.RemoveProduct
{
    public class RemovalFailedConsumer(IRemoveProductService service) : IConsumer<RemovalFailedMessage>
    {
        public async Task Consume(ConsumeContext<RemovalFailedMessage> context)
        {
            var removeRslt = await service.CreateProductAsync(context.Message.PreviousState);
        }
    }
}
