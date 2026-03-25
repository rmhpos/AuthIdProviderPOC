using CentralStore.CentralManager;
using CentralStore.CentralManager.Infrastructure;
using CentralStore.Shared.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "http://localhost:6001/realms/rmh-realm";
    options.ProviderOptions.ClientId = "central-manager-ui";
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.MetadataUrl = "http://localhost:6001/realms/rmh-realm/.well-known/openid-configuration";
}).AddAccountClaimsPrincipalFactory<RmhClaimsPrincipalFactory>();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("CanViewProducts", policy => policy.RequireRole("rmh.products.read"));
    options.AddPolicy("CanUpdateProducts", policy => policy.RequireRole("rmh.products.update"));
    options.AddPolicy("CanViewCustomers", policy => policy.RequireRole("rmh.customers.read"));
    options.AddPolicy("CanUpdateCustomers", policy => policy.RequireRole("rmh.customers.update"));
});

builder.Services.AddScoped<AttachTokenAuthenticationHandler>();

builder.Services
    .AddHttpClient("central-store-api", client => client.BaseAddress = new Uri("http://localhost:5002/"))
    .AddHttpMessageHandler<AttachTokenAuthenticationHandler>();

await builder.Build().RunAsync();
