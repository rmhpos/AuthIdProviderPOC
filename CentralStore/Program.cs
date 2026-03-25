using CentralStore.Domain;
using CentralStore.Extensions;
using CentralStore.Shared;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CentralStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            builder.AddRMHDbSupport();

            builder.Services
                .AddValidatorsFromAssembly(typeof(Program).Assembly)
                .AddRMHOpenApi()
                .AddRMHAuthentication(builder.Configuration)
                .AddAuthorization(options => 
                {
                    options.AddPolicy("CanViewProducts", policy => policy.RequireRole("rmh.products.read"));
                    options.AddPolicy("CanUpdateProducts", policy => policy.RequireRole("rmh.products.update"));
                    options.AddPolicy("CanViewCustomers", policy => policy.RequireRole("rmh.customers.read"));
                    options.AddPolicy("CanUpdateCustomers", policy => policy.RequireRole("rmh.customers.update"));
                })
                .RegisterEndpoints()
                .AddRMHCustomServices()
                .ConfigureHttpJsonOptions(options =>
                {
                    options.SerializerOptions.PropertyNameCaseInsensitive = true;
                })
                .Configure<QueueMetadata>(builder.Configuration
                    .GetSection(QueueMetadata.SectionName))
                .AddCors(options => 
                {
                    options.AddPolicy("central-manager-ui", policy =>
                    {
                        policy.WithOrigins("http://localhost:7001")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
                });

            if (!builder.Environment.IsEnvironment(SharedConstants.IntegrationTestsEnvironement))
            {
                builder.ConfigureMassTransit();
            }

            var app = builder.Build();

            if (!app.Environment.IsEnvironment(SharedConstants.IntegrationTestsEnvironement))
            {
                using (var scope = app.Services.CreateAsyncScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<CentralStoreDbContext>();
                    await db.Database.MigrateAsync();
                }
            }

            app.UseCors("central-manager-ui");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            using (var scope = app.Services.CreateScope())
            {
                foreach (var endpoint in scope.ServiceProvider.GetServices<IEndpoint>())
                {
                    endpoint.MapEndpoint(app);
                }
            }
            
            app.Run();
        }
    }
}