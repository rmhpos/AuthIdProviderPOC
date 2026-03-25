using CentralStore.CustomerManagement.CreateCustomer;
using CentralStore.CustomerManagement.RemoveCustomer;
using CentralStore.CustomerManagement.UpdateCustomer;
using CentralStore.Domain;
using CentralStore.ProductManagement.CreateProduct;
using CentralStore.ProductManagement.RemoveProduct;
using CentralStore.ProductManagement.UpdateProduct;
using CentralStore.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json;

namespace CentralStore.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRMHAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration["Keycloak:Authority"]
                    ?? throw new InvalidOperationException("Keycloak authority is not configured.");
                    options.Audience = configuration["Keycloak:Audience"]
                    ?? throw new InvalidOperationException("Keycloak audience is not configured.");
                    options.MetadataAddress = configuration["Keycloak:MetadataAddress"]
                    ?? throw new InvalidOperationException("Keycloak metadata address is not configured.");

                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Keycloak:Authority"],

                        ValidateAudience = true,
                        ValidAudience = configuration["Keycloak:Audience"],

                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("Authentication failed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            var identity = (ClaimsIdentity)context.Principal!.Identity!;

                            var resourceAccess = context.Principal.FindFirst("resource_access")?.Value;

                            if (resourceAccess != null)
                            {
                                var json = JsonDocument.Parse(resourceAccess);

                                if (json.RootElement.TryGetProperty("central-api", out var client))
                                {
                                    if (client.TryGetProperty("roles", out var roles))
                                    {
                                        foreach (var role in roles.EnumerateArray())
                                        {
                                            identity.AddClaim(new Claim(ClaimTypes.Role, role.GetString()!));
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("Token validated successfully.");
                            return Task.CompletedTask;
                        }
                    };
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
            services.AddScoped<ICreateCustomerService, CreateCustomerService>();
            services.AddScoped<IRemoveCustomerService, RemoveCustomerService>();
            services.AddScoped<IUpdateCustomerService, UpdateCustomerService>();

            return services;
        }
    }
}
