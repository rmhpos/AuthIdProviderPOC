using CentralStore.Shared;

namespace CentralStore.AdminManagement.HealthCheck
{
    public class HealthCheckEndpoint : IEndpoint
    {
        private const string Route = "api/health";
        private const string Tag = "HealthCheck";

        public void MapEndpoint(WebApplication app) 
            => app.MapGet(Route, () => Results.Ok("Healthy"))
               .WithTags(Tag);
    }
}
