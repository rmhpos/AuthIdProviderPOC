using CentralStore.StoreManager;
using CentralStore.StoreManager.Auth2;
using CentralStore.StoreManager.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("Local", options.ProviderOptions);
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("CanViewProducts", policy => policy.RequireRole("rmh.products.read"));
    options.AddPolicy("CanUpdateProducts", policy => policy.RequireRole("rmh.products.update"));
    options.AddPolicy("CanViewCustomers", policy => policy.RequireRole("rmh.customers.read"));
    options.AddPolicy("CanUpdateCustomers", policy => policy.RequireRole("rmh.customers.update"));
});

builder.Services.AddSingleton<AuthService>();
builder.Services.AddTransient<TokenAuthHandler>();

builder.Services
    .AddHttpClient("local-store-api", client => client.BaseAddress = new Uri("http://localhost:5003/"))
    .AddHttpMessageHandler<TokenAuthHandler>();

await builder.Build().RunAsync();
