using CentralStore.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CentralStore.CustomerManagement.RemoveCustomer
{
    public class RemoveCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/customers/{id}/";
        private const string Tag = "Customers";

        public void MapEndpoint(WebApplication app)
          => app.MapDelete(Route, Handle)
          .WithTags(Tag);

        private static async Task<Results<NoContent, NotFound, Conflict>> Handle(
          [FromRoute] Guid id,
          [FromBody] RemoveCustomerRequest request,
          IRemoveCustomerService service)
        {
            var previousState = await service.GetCustomerAsync(request);

            if (previousState is null)
                return TypedResults.Conflict();

            using (var trans = await service.BeginTransactionAsync())
            {
                try
                {
                    var result = await service.RemoveCustomerAsync(request.Id);

                    if (result == 0)
                        return TypedResults.Conflict();

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
