using CentralStore.Shared.Dtos.Users;
using CentralStore.Shared.Requests.Central;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CentralStore.CentralManager.customer_management.customer_new
{
    public partial class CustomerNew : ComponentBase
    {
        private HttpClient _httpClient => HttpClientFactory.CreateClient("central-store-api");
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Parameter] public string? ReturnUrl { get; set; }

        private CustomerDto Customer = new()
        {
            Id = Guid.NewGuid(),
            ConcurrencyToken = Guid.NewGuid()
        };

        private string StoreId = Guid.NewGuid().ToString();

        private async Task SaveCustomer()
        {
            var now = DateTime.UtcNow;
            Customer.UpdatedAt = now;
            Customer.CreatedAt = now;

            var response = await _httpClient.PostAsJsonAsync("api/customers", Customer.ToCreateRequest(Guid.Parse(StoreId)));

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
                Navigation.NavigateTo("/customers"); // fallback
        }
    }
}