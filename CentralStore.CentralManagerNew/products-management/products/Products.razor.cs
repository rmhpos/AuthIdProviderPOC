using CentralStore.Shared.Dtos.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;
using System.Net.Http.Json;

namespace CentralStore.CentralManagerNew.products_management.products
{
    public partial class Products : ComponentBase
    {
        private List<ProductDto> ProductsDtos = new();
        private HashSet<Guid> SelectedProducts = new();
        private PaginationState Pagination = new()
        {
            ItemsPerPage = 10
        };

        [Inject] private HttpClient Http { get; set; } = default!;
        [Inject] private NavigationManager Nav { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var url =
                $"api/products?page={Pagination.CurrentPageIndex + 1}&pageSize={Pagination.ItemsPerPage}";

            var result =
                await Http.GetFromJsonAsync<List<ProductDto>>(url);

            ProductsDtos = result ?? new();
        }

        private void ToggleSelection(Guid id)
        {
            if (!SelectedProducts.Add(id))
                SelectedProducts.Remove(id);
        }

        private void AddProduct()
        {
            Nav.NavigateTo("/productdetails/new");
        }

        private async Task DeleteSelected()
        {
            foreach (var id in SelectedProducts)
            {
                await Http.DeleteAsync($"api/products/{id}");
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
