using CentralStore.Shared.Dtos.Admin.Customers;
using CentralStore.Shared.Dtos.Customers;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CentralStore.CentralManager.customer_management.customer_detail
{
    public partial class CustomerDetails : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        private CustomerDto? _customer;
        private CustomerDto _editModel = CustomerFactory.CreateDefault();

        private bool _isEditable = false;
        private bool _isLoading = true;
        private bool _isSaving = false;
        private HttpClient _httpClient = new();
        private string? _errorMessage;

        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            _httpClient = HttpClientFactory.CreateClient("central-store-api");
            await LoadCustomer();
        }

        private async Task LoadCustomer()
        {
            _isLoading = true;

            try
            {
                _customer = await _httpClient.GetFromJsonAsync<CustomerDto>($"api/customers/{Id}");

                if (_customer != null)
                {
                    _editModel = Clone(_customer);
                }
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void EnableEdit()
        {
            if (_customer == null)
                return;

            _editModel = Clone(_customer);
            _isEditable = true;
        }

        private void CancelEdit()
        {
            if (_customer == null)
                return;

            _editModel = Clone(_customer);
            _isEditable = false;
            _errorMessage = null;
        }

        private async Task Save()
        {
            if (_customer == null)
                return;

            _isSaving = true;
            _errorMessage = null;

            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    $"api/customers/{_customer.Id}",
                    _editModel);

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    _errorMessage = "The customer was modified by another user. Reloading latest data.";
                    await LoadCustomer();
                    return;
                }

                response.EnsureSuccessStatusCode();

                _customer = Clone(_editModel);
                _isEditable = false;
            }
            catch (Exception ex)
            {
                _errorMessage = $"Save failed: {ex.Message}";
            }
            finally
            {
                _isSaving = false;
            }
        }

        private void Back()
        {
            Navigation.NavigateTo("/customers");
        }

        // needs deep copy herem, but not gonna introduce it becuase it's too much of a hassle for this example.
        private static CustomerDto Clone(CustomerDto source)
        {
            return new CustomerDto
            {
                Id = source.Id,
                FirstName = source.FirstName,
                LastName = source.LastName,
                Email = source.Email,
                Password = source.Password,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                ConcurrencyToken = source.ConcurrencyToken
            };
        }
    }
}
