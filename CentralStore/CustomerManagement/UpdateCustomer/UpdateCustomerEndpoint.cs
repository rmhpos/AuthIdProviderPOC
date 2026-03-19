using CentralStore.ProductManagement.Filters;
using CentralStore.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CentralStore.CustomerManagement.UpdateCustomer
{
    public class UpdateCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/customers/{id}/";
        private const string Tag = "Customers";

        public void MapEndpoint(WebApplication app)
          => app.MapPut(Route, Handle)
          .WithTags(Tag)
          .AddEndpointFilter<ValidationFilter<UpdateCustomerRequest>>();

        private static async Task<Results<NoContent, NotFound, Conflict>> Handle(
          [FromRoute] Guid id,
          [FromBody] UpdateCustomerRequest request,
          IMassTransitSendResolver mtResolver,
          IUpdateCustomerService service)
        {
            if (await service.IsConflictAsync(request.Id, request.ConcurrencyToken))
                return TypedResults.Conflict();

            using (var trans = await service.BeginTransactionAsync())
            {
                try
                {
                    var previousState = await service.GetById(request.Id);
                    var result = await service.UpdateCustomerApiAsync(request.ToDto(), request.StoreId);

                    if (result == 0 || previousState is null)
                    {
                        await trans.RollbackAsync();
                        return TypedResults.NotFound();
                    }

                    var currentState = await service.GetById(request.Id);

                    if (currentState is null)
                    {
                        await trans.RollbackAsync();
                        return TypedResults.NotFound();
                    }

                    await service.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            }

            return TypedResults.NoContent();
        }
    }
}
