using Application.Service;
using Microsoft.AspNetCore.Components.Authorization;
using System.Text.Json;
using Blazored.LocalStorage;
using Domain.Model;
using Domain.DTO;
using System.Text;
using System.Net.Http.Headers;
using Infrastructure.AuthProvider;
using Application.IService;
using System.Security.Claims;
using Domain.JwtAuthModel;

namespace Infrastructure.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        public AuthenticationService(HttpClient client, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _client = client;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        public async Task<ResponseAuthJwt> Login(LoginModel userForAuthentication)
        {
            var content = JsonSerializer.Serialize(userForAuthentication);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var authResult = await _client.PostAsync("api/Authenticate/Login", bodyContent);
            var authContent = await authResult.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ResponseAuthJwt>(authContent, _options);
            if (!authResult.IsSuccessStatusCode)
                return result;
            await _localStorage.SetItemAsync("jwtToken", result.Token);
            await _localStorage.SetItemAsync("refreshToken", result.RefreshToken);
            await _localStorage.SetItemAsync("expiration", result.Expiration);

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(userForAuthentication.Username);
            
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("refresh", result.RefreshToken);
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("expiration", result.Expiration.ToString());
            return new ResponseAuthJwt { Token = result.Token, RefreshToken = result.RefreshToken, Expiration = result.Expiration };
        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserModel userForRegistration)
        {
            var content = JsonSerializer.Serialize(userForRegistration);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var registrationResult = await _client.PostAsync("api/Account/Registration", bodyContent);
            var registrationContent = await registrationResult.Content.ReadAsStringAsync();
            if (!registrationResult.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<RegistrationResponseDTO>(registrationContent, _options);
                return result;
            }
            return new RegistrationResponseDTO { IsSuccessfulRegistration = true };
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("jwtToken");
            await _localStorage.RemoveItemAsync("refreshToken");
            await _localStorage.RemoveItemAsync("expiration");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _client.DefaultRequestHeaders.Authorization = null;
        }

        public Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, "testUser"),
            }, "TestUser authentication type");
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
