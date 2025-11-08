using Domain.Model;
using Domain.DTO;
using Domain.JwtAuthModel;
using Microsoft.AspNetCore.Components.Authorization;

namespace Application.IService
{
    public interface IAuthenticationService
    {
        //Task<User> RegisterUser(UserForRegistrationDto userForRegistration);
        Task<ResponseAuthJwt> Login(LoginModel userForAuthentication);
        Task Logout();
        Task<AuthenticationState> GetAuthenticationStateAsync();
        Task<RegistrationResponseDTO> RegisterUser(UserModel userForRegistration);
    }
}
