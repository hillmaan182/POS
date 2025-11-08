using Microsoft.AspNetCore.Mvc;
using Domain.Model;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

using Application.Service;

namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IConfiguration configuration;
        private IUserModelRepository umRepo;
        private IGenericRepository<UserModel> genUserModel;
        public AccountController(IConfiguration configuration, IGenericRepository<UserModel> genUserModel, IUserModelRepository umRepo)
        {
            this.configuration = configuration;
            this.genUserModel = genUserModel;
            this.umRepo = umRepo;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel login)
        {
            //IActionResult response = Unauthorized();
            //var user = umRepo.AuthenticateUser(login);
            //string jwt = configuration["Jwt:Key"];
            //string issuer = configuration["Jwt:Issuer"];
            //if (user != null)
            //{
            //    var tokenString = umRepo.GenerateJSONWebToken(user,jwt,issuer);
            //    response = Ok(new { token = tokenString });
            //}

            //return response;

            string jwt = configuration["Jwt:Key"];
            string issuer = configuration["Jwt:Issuer"];
            var user = umRepo.AuthenticateUser(login);
            if (user == null) return BadRequest(new Domain.DTO.AuthResponseDTO { ErrorMessage = "Username or Password is wrong", IsAuthSuccessful = false });

            var signingCredentials = umRepo.GetSigningCredentials(jwt);
            var claims = umRepo.GetClaims(login);
            var tokenOptions = umRepo.GenerateTokenOptions(signingCredentials, claims, issuer);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { token = tokenString });

        }

        [AllowAnonymous]
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] UserModel userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var usr = umRepo.GetUserByUsernameOrEmail(userForRegistration);
            List<string> names = new List<string> { "Username or Password has been taken" };
            IEnumerable<string> err = names;
            if (usr != null)
            {
                return BadRequest(new RegistrationResponseDTO { Errors = err });
            }
            else
            {
                userForRegistration.DateOfJoin = DateTime.Now;
                await genUserModel.AddAsync(userForRegistration);
                await genUserModel.SaveAsync();
            }

            return StatusCode(201);
        }


    }
}
