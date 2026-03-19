using CentralStore.Domain;
using CentralStore.ProductManagement.CreateProduct;
using CentralStore.ProductManagement.RemoveProduct;
using CentralStore.ProductManagement.UpdateProduct;
using CentralStore.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace CentralStore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRMHAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var authority = "http://localhost:6001/realms/rmh-realm";
                    options.Authority = authority;
                    options.Audience = "central-api";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidateLifetime = true;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                });

            return services;
        }

        public static IServiceCollection AddRMHOpenApi(this IServiceCollection services)
        {
            services.AddOpenApi();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection RegisterEndpoints(this IServiceCollection services)
        {
            ServiceDescriptor[] serviceDescriptors = Assembly.GetExecutingAssembly().DefinedTypes
              .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && t.IsClass && !t.IsAbstract)
              .Select(t => ServiceDescriptor.Transient(typeof(IEndpoint), t))
              .ToArray();

            services.TryAddEnumerable(serviceDescriptors);
            return services;
        }

        public static WebApplicationBuilder AddRMHDbSupport(this WebApplicationBuilder builder)
        {
            if (!builder.Environment.IsEnvironment(SharedConstants.IntegrationTestsEnvironement))
            {
                builder.Services.AddDbContext<CentralStoreDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

                return builder;
            }

            return builder;
        }

        public static IServiceCollection AddRMHCustomServices(this IServiceCollection services)
        {
            services.AddScoped<ICreateProductService, CreateProductsService>();
            services.AddScoped<IRemoveProductService, RemoveProductService>();
            services.AddScoped<IUpdateProductService, UpdateProductService>();
            services.AddScoped<IMassTransitSendResolver, EndpointUriResolver>();

            return services;
        }
    }
}
