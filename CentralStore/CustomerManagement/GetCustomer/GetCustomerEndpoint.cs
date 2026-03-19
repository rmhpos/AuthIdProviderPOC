using CentralStore.Domain;
using CentralStore.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.CustomerManagement.GetCustomer
{
    public class GetCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/customers/{id}/";
        private const string Tag = "Customers";

        public void MapEndpoint(WebApplication app)
          => app.MapGet(Route, Handle)
          .WithTags(Tag);

        private static async Task<Results<
          Ok<Customer>,
          NotFound>> Handle(
          [FromRoute] Guid id,
          CentralStoreDbContext dbContext)
        {
            var customer = await dbContext.Customers
              .AsNoTracking()
              .SingleOrDefaultAsync(p => p.Id == id);

            if (customer is null)
                return TypedResults.NotFound();

            return TypedResults.Ok(customer);
        }
    }
}
