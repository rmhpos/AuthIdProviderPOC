using CentralStore.Shared.Dtos.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Net.Http.Json;

namespace CentralStore.CentralManagerNew.admin_management.customers
{
    public partial class Customers : ComponentBase
    {
        private List<CustomerDto> CustomerDtos = new();
        private HashSet<Guid> SelectedCustomers = new();
        private PaginationState Pagination = new()
        {
            ItemsPerPage = 10
        };

        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadCustomers();
        }

        private async Task LoadCustomers()
        {
            var url =
                $"api/customers?page={Pagination.CurrentPageIndex + 1}&pageSize={Pagination.ItemsPerPage}";

            var result =
                await Http.GetFromJsonAsync<List<CustomerDto>>(url);

            CustomerDtos = result ?? new();
        }

        private void ToggleSelection(Guid id)
        {
            if (!SelectedCustomers.Add(id))
                SelectedCustomers.Remove(id);
        }

        private void AddCustomer()
        {
            Nav.NavigateTo("/customerdetails/new");
        }

        private async Task DeleteSelected()
        {
            foreach (var id in SelectedCustomers)
            {
                await Http.DeleteAsync($"api/customers/{id}");
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
