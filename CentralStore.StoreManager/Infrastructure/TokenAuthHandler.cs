using CentralStore.StoreManager.Auth2;
using System.Net.Http.Headers;

namespace CentralStore.StoreManager.Infrastructure
{
    public class TokenAuthHandler : DelegatingHandler
    {
        private readonly AuthService _authService;

        public TokenAuthHandler(AuthService authService)
        {
            _authService = authService;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (_authService.IsAuthenticated)
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _authService.GetAccessToken());
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}