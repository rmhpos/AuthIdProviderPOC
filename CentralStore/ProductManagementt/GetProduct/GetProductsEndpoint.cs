using CentralStore.Domain;
using CentralStore.Shared;
using CentralStore.Shared.Dtos.Products;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CentralStore.ProductManagementt.GetProduct
{
    public class GetCustomerEndpoint : IEndpoint
    {
        private const string Route = "api/products";
        private const string Tag = "Products";

        public void MapEndpoint(WebApplication app)
          => app.MapGet(Route, Handle)
          .WithTags(Tag);

        private static async Task<Results<
          Ok<List<ProductDto>>,
          BadRequest<string>>> Handle(
          [FromBody] PageParams pageParams,
          CentralStoreDbContext dbContext)
        {
            if (pageParams.Page < 1 || pageParams.PageSize < 1)
                return TypedResults.BadRequest("Page and PageSize must be greater than 0.");

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageParams.Page - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    MinPrice = p.MinPrice,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt,
                    ConcurrencyToken = p.ConcurrencyToken
                })
                .ToListAsync();

            return TypedResults.Ok(products);
        }
    }
}
