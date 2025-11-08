using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.JwtAuthService
{
    public interface IJwtAuthRepository
    {
        JwtSecurityToken CreateToken(List<Claim> authClaims, IConfiguration _configuration);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token, IConfiguration _configuration);
    }
}
