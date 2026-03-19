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
                .AddRMHAuthentication()
                .AddAuthorization()
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
                        policy.WithOrigins("http://localhost:5087")
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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();

            if (app.Environment.IsDevelopment())
            {
                app.UseCors("central-manager-ui");
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