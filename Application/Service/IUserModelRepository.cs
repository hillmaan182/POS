using Domain.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.DTO;

namespace Application.Service
{
    public interface IUserModelRepository : IGenericRepository<UserModel>
    {
        UserModel AuthenticateUser(UserModel user);

        UserModel GetUserByUsernameOrEmail(UserModel user);
        //Task<RegistrationResponseDTO> RegisterUser(UserModel userForRegistration);
        string GenerateJSONWebToken(UserModel user, string JwtKey, string Issuer);
        SigningCredentials GetSigningCredentials(string Jwt);
        List<Claim> GetClaims(UserModel user);
        JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims, string issuerConfig);
    }
}
