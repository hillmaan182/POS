using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.ServiceHelper;
using Domain.DTO;
namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HitpayController : ControllerBase
    {
        const string urihitpay = "https://api.sandbox.hit-pay.com/v1/payment-requests";
        const string apikey = "test_373e37c28311f30c28a5b9dc1afe0f0850dd4a108271fb2061fbb12316d51a67";
        private readonly IHttpExtensionRepository _http;

        public HitpayController(IHttpExtensionRepository http)
        {
            this._http = http;
           
        }

        [HttpPost]
        public async Task<string> CreatePayment(HitpayPayload pl , string url , string apiKey)
        {
            try
            {
                string resp = await _http.PostAsync(pl, url, apiKey);
                return resp;
            }
            catch (Exception)
            {
                return "";
            }

        }
    }
}
