using CentralStore.ProductManagement.Filters;
using CentralStore.Shared;
using CentralStore.Shared.Requests.Central;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CentralStore.CustomerManagement.CreateCustomer
{
    public class CreateCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/customers";
        private const string Tag = "Customers";

        public void MapEndpoint(WebApplication app)
          => app.MapPost(Route, Handle)
          .WithTags(Tag)
          .AddEndpointFilter<ValidationFilter<CreateCustomerRequest>>()
          .RequireAuthorization(policy => policy.RequireRole("rmh.customers.create"));

        private static async Task<Results<
          Created<CreateCustomerResponse>,
            ValidationProblem>> Handle(
          [FromBody] CreateCustomerRequest request,
          IMassTransitSendResolver mtResolver,
          ICreateCustomerService service)
        {
            var customer = service.CreateCustomer(request.ToDto(), request.StoreId);
            var endpoint = await mtResolver.GetSendEndpoint(request.StoreId);

            //await endpoint.Send(new CreateUserMessage(customer.ToDto()));
            await service.SaveChangesAsync();

            return TypedResults.Created($"/{Route}/{customer?.Id}", customer?.ToResponse());
        }
    }
}
