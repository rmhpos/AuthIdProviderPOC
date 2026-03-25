using CentralStore.ProductManagement.CreateProduct;
using CentralStore.Shared;
using CentralStore.Shared.Messages;
using MassTransit;
using Microsoft.Extensions.Options;

namespace CentralStore.ProductManagement.RemoveProduct
{
    public class RemovalFailedConsumer(IRemoveProductService service,
            IOptions<QueueMetadata> options) : IConsumer<RemovalFailedMessage>
    {
        public async Task Consume(ConsumeContext<RemovalFailedMessage> context)
        {
            Guid.TryParse(context.GetHeader(options.Value.StoreIdHeaderKey), out var storeId);

            // Get the store id that you need so that you add it to the product store
            var removeRslt = await service.CreateProductAsync(context.Message.PreviousState.ToDto(storeId));
        }
    }
}
