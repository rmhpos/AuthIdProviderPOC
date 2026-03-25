using CentralStore.LocalStore.Domain;
using CentralStore.LocalStore.Shared;
using CentralStore.Shared.Dtos.Products;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CentralStore.LocalStore.ProductManagementt.GetProduct
{
    public class GetProductsEndpoint : IEndpoint
    {
        private const string Route = "api/products";
        private const string Tag = "Products";

        public void MapEndpoint(WebApplication app)
          => app.MapGet(Route, Handle)
          .WithTags(Tag)
          .RequireAuthorization(policy => policy
            .RequireRole("rmh.products.read", "rmh.products.write"));

        private static async Task<Results<
          Ok<List<ProductDtoBase>>,
          BadRequest<string>>> Handle(
          [AsParameters] PageParams pageParams,
          LocalStoreDbContext dbContext,
          HttpContext httpContext,
          ClaimsPrincipal user)
        {
            if (pageParams.Page < 1 || pageParams.PageSize < 1)
                return TypedResults.BadRequest("Page and PageSize must be greater than 0.");

            var products = await dbContext.Products
                .AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((pageParams.Page - 1) * pageParams.PageSize)
                .Take(pageParams.PageSize)
                .Select(p => new ProductDtoBase
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

