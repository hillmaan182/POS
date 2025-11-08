using Microsoft.AspNetCore.Mvc;
using Application.ServiceHelper;
using Domain.JwtAuthModel;
namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailHelper emailHelp;
        private readonly IConfiguration config;
        public EmailController(IEmailHelper emailHelp, IConfiguration config)
        {
            this.emailHelp = emailHelp;
            this.config = config;
        }

        [HttpPost]
        [Route("send-email")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel model)
        {
            var body = emailHelp.SetBodyEmailActivateAccount(model.Link);
            var result = await emailHelp.SendEmail(model.ToEmail, model.ToEmail, body, config);
            return Ok(new { message = result });
        }
    }
}
