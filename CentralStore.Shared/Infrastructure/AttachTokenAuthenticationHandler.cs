using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;

namespace CentralStore.Shared.Infrastructure
{
    public class AttachTokenAuthenticationHandler : DelegatingHandler
    {
        private readonly IAccessTokenProvider _tokenProvider;

        public AttachTokenAuthenticationHandler(IAccessTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri!.AbsoluteUri.StartsWith("http://localhost:5002"))
            {
                var tokenResult = await _tokenProvider.RequestAccessToken();

                if (tokenResult.TryGetToken(out var token))
                {
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token.Value);
                }
            }


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
