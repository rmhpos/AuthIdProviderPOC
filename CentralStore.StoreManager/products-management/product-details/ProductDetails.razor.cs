using CentralStore.Shared.Dtos.Products;
using CentralStore.StoreManager.Auth2;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace CentralStore.StoreManager.products_management.product_details
{
    public partial class ProductDetails : ComponentBase
    {
        [Parameter]
        public Guid Id { get; set; }

        private HttpClient _httpClient => HttpClientFactory.CreateClient("local-store-api");
        private ProductDtoBase? product;
        private ProductDtoBase editModel = ProductFactory.CreateDefault();

        private bool isEditable = false;
        private bool isLoading = true;
        private bool isSaving = false;

        private string? errorMessage;

        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private AuthService AuthService { get; set; } = default!;

        //for demo purposes when we want to see how to check authorization in code
        //[Inject] private AuthenticationStateProvider _authStateProvider { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProduct();
        }

        private bool IsInRole(string role)
        {
            return AuthService.User.IsInRole(role);
        }

        private async Task LoadProduct()
        {
            isLoading = true;

            try
            {
                product = await _httpClient.GetFromJsonAsync<ProductDtoBase>($"api/products/{Id}");

                if (product != null)
                {
                    editModel = Clone(product);
                }
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task EnableEdit()
        {
            if (product == null)
                return;

            // How to do it in code
            //var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;

            //if (user.IsInRole("rmh.products.update"))
            //{
            //    // show delete button

            //    editModel = Clone(product);
            //    isEditable = true;
            //}

            editModel = Clone(product);
            isEditable = true;
        }

        private void CancelEdit()
        {
            if (product == null)
                return;

            editModel = Clone(product);
            isEditable = false;
            errorMessage = null;
        }

        private async Task Save()
        {
            if (product == null)
                return;

            isSaving = true;
            errorMessage = null;

            try
            {
                var response = await _httpClient.PutAsJsonAsync(
                    $"api/products/{product.Id}",
                    editModel);

                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    errorMessage = "The product was modified by another user. Reloading latest data.";
                    await LoadProduct();
                    return;
                }

                response.EnsureSuccessStatusCode();

                product = Clone(editModel);
                isEditable = false;
            }
            catch (Exception ex)
            {
                errorMessage = $"Save failed: {ex.Message}";
            }
            finally
            {
                isSaving = false;
            }
        }

        private void Back()
        {
            Navigation.NavigateTo("/products");
        }

        // needs deep copy herem, but not gonna introduce it becuase it's too much of a hassle for this example.
        private static ProductDtoBase Clone(ProductDtoBase source)
        {
            return new ProductDtoBase
            {
                Id = source.Id,
                Name = source.Name,
                Description = source.Description,
                Price = source.Price,
                MinPrice = source.MinPrice,
                CreatedAt = source.CreatedAt,
                UpdatedAt = source.UpdatedAt,
                ConcurrencyToken = source.ConcurrencyToken
            };
        }
    }
}
