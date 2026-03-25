using CentralStore.Shared;
using CentralStore.Shared.Messages;
using MassTransit;
using Microsoft.Extensions.Options;

namespace CentralStore.ProductManagement.CreateProduct
{
    public class CreateProductConsumer(ICreateProductService service,
      IMassTransitSendResolver mtResolver,
      IOptions<QueueMetadata> options) : IConsumer<CreateProductMessage>
    {
        //Based on the store id header send to the correct local store queue
        public async Task Consume(ConsumeContext<CreateProductMessage> context)
        {
            Guid.TryParse(context.GetHeader(options.Value.StoreIdHeaderKey), out var storeId);

            try
            {
                var createRslt = service.CreateProduct(context.Message.CurrentState.ToDto(storeId), storeId);
                await service.SaveChangesAsync();
            }
            catch (Exception)
            {
                var endpoint = await mtResolver.GetSendEndpoint(storeId);

                await endpoint.Send(new CreationFailedMessage(context.Message.CurrentState.Id));
                await service.SaveChangesAsync();
            }
        }
    }
}
