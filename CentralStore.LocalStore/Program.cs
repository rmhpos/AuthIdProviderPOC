using CentralStore.LocalStore.AdminManagement;
using CentralStore.LocalStore.Domain;
using CentralStore.LocalStore.ProductManagement.CreateProduct;
using CentralStore.LocalStore.ProductManagement.RemoveProduct;
using CentralStore.LocalStore.ProductManagement.UpdateProduct;
using CentralStore.LocalStore.Shared;
using CentralStore.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace CentralStore.LocalStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            if (!builder.Environment.IsEnvironment(SharedConstants.IntegrationTestsEnvironement))
            {
                builder.Services.AddDbContext<LocalStoreDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

                builder.Services.AddDbContext<LocalStoreIdentityDbContext>(options
                => options.UseSqlServer(builder.Configuration.GetConnectionString("Identity")));

                builder.ConfigureMassTransit();
            }

            builder.Services.AddSwaggerGen();

            builder.Services.AddIdentity<LocalStoreUser, IdentityRole>()
            .AddEntityFrameworkStores<LocalStoreIdentityDbContext>();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:Issuer"];
                options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:Audience"];
                options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!));
                options.TokenValidationParameters.ValidateAudience = true;
                
                options.TokenValidationParameters.RoleClaimType = ClaimTypes.Role;
            });

            builder.Services.AddAuthorization();

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.PropertyNameCaseInsensitive = true;
            });

            builder.Services.AddScoped<ICreateProductService, CreateProductsService>();
            builder.Services.AddScoped<IRemoveProductService, RemoveProductService>();
            builder.Services.AddScoped<IUpdateProductService, UpdateProductService>();
            builder.Services.AddScoped<IMassTransitSendResolver, EndpointUriResolver>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("store-manager-ui", policy =>
                {
                    policy.WithOrigins("http://localhost:7002")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

            builder.Services.Configure<QueueMetadata>(builder.Configuration
              .GetSection(QueueMetadata.SectionName));

            ServiceDescriptor[] serviceDescriptors = Assembly.GetExecutingAssembly().DefinedTypes
              .Where(t => t.IsAssignableTo(typeof(IEndpoint)) && t.IsClass && !t.IsAbstract)
              .Select(t => ServiceDescriptor.Transient(typeof(IEndpoint), t))
              .ToArray();

            builder.Services.TryAddEnumerable(serviceDescriptors);

            var app = builder.Build();

            if (!app.Environment.IsEnvironment(SharedConstants.IntegrationTestsEnvironement))
            {
                using (var scope = app.Services.CreateAsyncScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<LocalStoreDbContext>();
                    await db.Database.MigrateAsync();
                }

                using (var scope = app.Services.CreateAsyncScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<LocalStoreIdentityDbContext>();
                    await db.Database.MigrateAsync();
                }

                using (var scope = app.Services.CreateAsyncScope())
                    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
            }

            using (var scope = app.Services.CreateScope())
            {
                foreach (var endpoint in scope.ServiceProvider.GetServices<IEndpoint>())
                {
                    endpoint.MapEndpoint(app);
                }
            }

            RegisterEndpoint.Map(app);
            LoginEndpoint.Map(app);
            LogoutEndpoint.Map(app);

            app.UseCors("store-manager-ui");

            app.UseAuthentication();
            app.UseAuthorization();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.Run();
        }
    }
}