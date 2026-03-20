using CentralStore.Shared.Dtos.Products;
using CentralStore.Shared.Requests;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
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

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var pageParams = new PageParams() 
            {
                Page = Pagination.CurrentPageIndex + 1,
                PageSize = Pagination.ItemsPerPage
            };

            var url = $"api/products";

            var result = await _httpClient.PostAsJsonAsync<PageParams>(url, pageParams);
            var productDtos = await result.Content.ReadFromJsonAsync<List<ProductDto>>() ?? new();

            ProductsDtos = productDtos;
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
                await _httpClient.DeleteAsync($"api/products/{id}");
            }

            SelectedProducts.Clear();

            await LoadProducts();
        }

        private void NavigateToProduct(Guid id)
        {
            Nav.NavigateTo($"/productdetails/{id}");
        }
    }
}
