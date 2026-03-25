using CentralStore.Domain;
using MassTransit;

namespace CentralStore
{
  public static class MassTransitConfig
  {
    public static IServiceCollection ConfigureMassTransit(this WebApplicationBuilder builder)
      => builder.Services.AddMassTransit(busConfig =>
      {
        busConfig.SetKebabCaseEndpointNameFormatter();
        busConfig.AddEntityFrameworkOutbox<CentralStoreDbContext>(options =>
        {
          options.QueryDelay = TimeSpan.FromSeconds(10);
          options.UseSqlServer();
          options.UseBusOutbox();
        });

        busConfig.AddConsumers(typeof(Program).Assembly);

        busConfig.UsingRabbitMq((context, config) =>
        {
            config.Host(builder.Configuration["QueueMetadata:Host"]);

            //config.Host(builder.Configuration["QueueMetadata:Host"],
            //    builder.Configuration["QueueMetadata:Port"],
            //    builder.Configuration["QueueMetadata:VirtualHost"],
            //    h =>
            //{
            //  h.Username(builder.Configuration["QueueMetadata:Username"]);
            //  h.Password(builder.Configuration["QueueMetadata:Password"]);
            //});

            var queueName = builder.Configuration["QueueMetadata:CentralStoreQueueName"] 
          ?? throw new ArgumentNullException("QueueMetadata:CentralStoreQueueName configuration is null");

          config.ReceiveEndpoint(queueName, e =>
          {
            e.ConfigureConsumers(context);
          });
        });
      });
  }
}
