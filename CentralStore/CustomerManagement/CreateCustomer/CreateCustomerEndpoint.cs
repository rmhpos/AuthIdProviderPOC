using CentralStore.AdminManagement.CreateUser;
using CentralStore.ProductManagement.Filters;
using CentralStore.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CentralStore.ProductManagement.CreateProduct
{
    public class CreateCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/users";
        private const string Tag = "Users";

        public void MapEndpoint(WebApplication app)
          => app.MapPost(Route, Handle)
          .WithTags(Tag)
          .AddEndpointFilter<ValidationFilter<CreateCustomerRequest>>();

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
