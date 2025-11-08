using Blazored.LocalStorage;
using Infrastructure.Features;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;

namespace Infrastructure.AuthProvider
{
    public class AuthStateProvider : AuthenticationStateProvider //: AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationState _anonymous;
        public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("jwtToken");
            var refreshToken = await _localStorage.GetItemAsync<string>("refreshToken");
            var expiration = await _localStorage.GetItemAsync<string>("expiration");
            if (string.IsNullOrWhiteSpace(token))
                return _anonymous;
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return _anonymous;
            }

            DateTime getExpiration = DateTime.Parse(expiration);
            DateTime now = DateTime.Now;
            if (now >= getExpiration) {

                //update revoke 
                return _anonymous;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }
        public void NotifyUserAuthentication(string email)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, email) }, "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }
        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
