using Domain.Model;
using Application.Service;
using Domain.DataContext;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Repository
{
    public class UserModelRepository : GenericRepository<UserModel>, IUserModelRepository
    {
        private readonly DataContext db;
        public UserModelRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        public UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            user = db.UserModel.Where(x => x.Username.ToLower() == login.Username.ToLower() && x.Password.ToLower() == login.Password.ToLower()).FirstOrDefault();
            return user;
        }

        public UserModel GetUserByUsernameOrEmail(UserModel user)
        {
            UserModel usr = null;
            usr = db.UserModel.Where(x => x.Username.ToLower() == user.Username.ToLower() && x.EmailAddress.ToLower() == user.EmailAddress.ToLower()).FirstOrDefault();

            return usr;
        }
        
        public SigningCredentials GetSigningCredentials(string Jwt)
        {
            var key = Encoding.UTF8.GetBytes(Jwt);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        public List<Claim> GetClaims(UserModel user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username)
    };

            return claims;
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims , string issuerConfig)
        {
            var tokenOptions = new JwtSecurityToken(
                issuer: issuerConfig,
                audience: issuerConfig,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(15)),
                signingCredentials: signingCredentials);

            return tokenOptions;
        }

        public string GenerateJSONWebToken(UserModel user , string JwtKey , string Issuer)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub , user.Username),
                new Claim(JwtRegisteredClaimNames.Email , user.EmailAddress),
                new Claim("DateOfJoin", user.DateOfJoin.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(Issuer, Issuer, claims, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
