using CentralStore.StoreManager.Responses;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace CentralStore.StoreManager.Auth2
{
    public class AuthService
    {
        private IHttpClientFactory _httpClientFactory;
        private HttpClient _http => _httpClientFactory.CreateClient("local-store-api");
        private string? _accessToken;

        public event Action? OnAuthStateChanged;

        public ClaimsPrincipal User = new ClaimsPrincipal();

        public bool IsAuthenticated => !string.IsNullOrEmpty(_accessToken);

        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<bool> RegisterAsync(string email, string password)
        {
            var confirmPassword = password;

            var response = await _http.PostAsJsonAsync("/register", new
            {
                email,
                password,
                confirmPassword
            });

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var response = await _http.PostAsJsonAsync("/login", new
            {
                email,
                password
            });

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (string.IsNullOrEmpty(result!.AccessToken)) return false;

            Console.WriteLine(JsonSerializer.Serialize(result!.AccessToken));

            var identity = new ClaimsIdentity(ClaimsPrincipalHelper.ParseClaimsFromJwt(result!.AccessToken), "jwt");
            User = new ClaimsPrincipal(identity);

            _accessToken = result.AccessToken;

            OnAuthStateChanged?.Invoke();

            return true;
        }

        public string GetAccessToken() => _accessToken ?? throw new ArgumentNullException("access token is null");

        public async Task LogOutAsync()
        {
            await _http.PostAsync("/logout", null);
            _accessToken = null;
            User = new ClaimsPrincipal();

            OnAuthStateChanged?.Invoke();
        }
    }
}
