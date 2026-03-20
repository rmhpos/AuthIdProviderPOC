using CentralStore.Shared.Dtos.Users;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Net.Http.Json;

namespace CentralStore.CentralManager.customer_management.customers
{
    public partial class Customers : ComponentBase
    {
        private HttpClient _httpClient => HttpClientFactory.CreateClient("central-store-api");
        private List<CustomerDto> CustomerDtos = new();
        private HashSet<Guid> SelectedCustomers = new();
        private PaginationState Pagination = new()
        {
            ItemsPerPage = 10
        };

        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            var pageParams = new PageParams()
            {
                Page = Pagination.CurrentPageIndex + 1,
                PageSize = Pagination.ItemsPerPage
            };

            var url = $"api/customers";

            var result = await _httpClient.GetFromJsonAsync<List<CustomerDto>>(url);

            CustomerDtos = result ?? new();
        }

        private void ToggleSelection(Guid id)
        {
            if (!SelectedCustomers.Add(id))
                SelectedCustomers.Remove(id);
        }

        private void AddCustomer()
        {
            Nav.NavigateTo("/customers/new");
        }

        private async Task DeleteSelected()
        {
            foreach (var id in SelectedCustomers)
            {
                await _httpClient.DeleteAsync($"api/customers/{id}");
            }

            SelectedCustomers.Clear();

            await LoadCustomers();
        }

        private void NavigateToCustomer(Guid id)
        {
            Nav.NavigateTo($"/customerdetails/{id}");
        }
    }
}
