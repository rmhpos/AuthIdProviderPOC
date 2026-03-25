using CentralStore.Shared.Dtos.Products;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Json;

namespace CentralStore.CentralManager.products_management.products
{
    public partial class Products : ComponentBase
    {
        private HttpClient _httpClient => HttpClientFactory.CreateClient("central-store-api");
        private List<ProductDto> ProductsDtos = new();
        private HashSet<Guid> SelectedProducts = new();
        private PaginationState Pagination = new()
        {
            ItemsPerPage = 10
        };

        [Inject] private IHttpClientFactory HttpClientFactory { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;
        [Inject] private IAccessTokenProvider TokenProvider { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var resultToken = await TokenProvider.RequestAccessToken();

            if (resultToken.TryGetToken(out var token))
            {
                Console.WriteLine("TOKEN FOUND:");
                Console.WriteLine(token.Value);
            }
            else
            {
                Console.WriteLine("NO TOKEN");
            }

            var pageParams = new PageParams() 
            {
                Page = Pagination.CurrentPageIndex + 1,
                PageSize = Pagination.ItemsPerPage
            };

            var url = $"api/products?page={pageParams.Page}&pageSize={pageParams.PageSize}";

            var result = await _httpClient.GetFromJsonAsync<List<ProductDto>>(url);

            ProductsDtos = result ?? new();
        }

        private void ToggleSelection(Guid id)
        {
            if (!SelectedProducts.Add(id))
                SelectedProducts.Remove(id);
        }

        private void AddProduct()
        {
            Nav.NavigateTo($"/products/new");
        }

        private async Task DeleteSelected()
        {
            foreach (var id in SelectedProducts)
            {
                var prod = ProductsDtos.First(p => p.Id == id);
                var request = new HttpRequestMessage(HttpMethod.Delete, $"api/products/{prod.Id}")
                {
                    Content = JsonContent.Create(new
                    {
                        Id = prod.Id,
                        ConcurrencyToken = prod.ConcurrencyToken
                    })
                };

                await _httpClient.SendAsync(request);
            }

            SelectedProducts.Clear();
            await LoadProducts();
        }

        private void NavigateToProduct(Guid id)
        {
            Nav.NavigateTo($"/products/{id}");
        }
    }
}
