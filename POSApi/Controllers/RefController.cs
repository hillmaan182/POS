using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;

namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefController : ControllerBase
    {
        private readonly IMasterRefRepository refRepository;
        private readonly IGenericRepository<MasterRef> genRef;
        public RefController(IMasterRefRepository refRepository, IGenericRepository<MasterRef> genRef)
        {
            this.refRepository = refRepository;
            this.genRef = genRef;
        }

        //[HttpGet("{refName}")]
        //public async Task<string> GetRefByName(string refName)
        //{
        //    try
        //    {
        //        var refCode = refRepository.GetRef(refName);
        //        return refCode.ToString();
        //    }
        //    catch (Exception)
        //    {

        //        return "";
        //    }
        //}


        [HttpGet("{refName}")]
        public async Task<IActionResult> GetRefCodeByName(string refName)
        {
            try
            {
                var refCode = refRepository.GetRef(refName);
                return Ok(refCode);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRefByName(MasterRef data)
        {
            try
            {
                await genRef.UpdateAsync(data);
                await genRef.SaveAsync();
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
