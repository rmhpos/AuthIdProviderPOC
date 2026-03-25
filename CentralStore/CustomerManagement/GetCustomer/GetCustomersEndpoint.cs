using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Customers;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.CustomerManagement.GetCustomer
{
    public class GetCustomersEndpoint : IEndpoint
    {
        private const string Route = "api/customers";
        private const string Tag = "Customers";

        public void MapEndpoint(WebApplication app)
          => app.MapGet(Route, Handle)
          .WithTags(Tag)
          .RequireAuthorization("CanViewCustomers");

        private static async Task<Results<
          Ok<List<CustomerDto>>,
          BadRequest<string>>> Handle(
          [AsParameters] PageParams pageParams,
          CentralStoreDbContext dbContext)
        {
            if (pageParams.Page < 1 || pageParams.PageSize < 1)
                return TypedResults.BadRequest("Page and PageSize must be greater than 0.");

            var customers = await dbContext.Customers
                .AsNoTracking()
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName)
                .Skip((pageParams.Page - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(p => p.ToDto())
                .ToListAsync();

            return TypedResults.Ok(customers);
        }
    }
}
