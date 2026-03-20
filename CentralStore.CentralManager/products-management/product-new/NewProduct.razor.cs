using CentralStore.Shared.Dtos.Products;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CentralStore.CentralManager.products_management.product_new
{
    public partial class NewProduct : ComponentBase
    {
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;

        private HttpClient _httpClient => HttpClientFactory.CreateClient("central-store-api");

        [Parameter] public string? ReturnUrl { get; set; }

        private ProductDto Product = new()
        {
            Id = Guid.NewGuid(),
            ConcurrencyToken = Guid.NewGuid()
        };

        private async Task SaveProduct()
        {
            var now = DateTime.UtcNow;
            Product.UpdatedAt = now;
            Product.CreatedAt = now;

            var response = await _httpClient.PostAsJsonAsync("api/products", Product);

            if (response.IsSuccessStatusCode)
            {
                NavigateBack();
            }
            else
            {
                // Optional: show error message
            }
        }

        private void Cancel()
        {
            NavigateBack();
        }

        private void NavigateBack()
        {
            if (!string.IsNullOrWhiteSpace(ReturnUrl))
                Navigation.NavigateTo(ReturnUrl);
            else
                Navigation.NavigateTo("/products"); // fallback
        }
    }
}
