using Application.Service;
using Domain.Model;
using Microsoft.AspNetCore.Mvc;

namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionDetailRepository transRepo;
        private readonly IGenericRepository<Transaction> genTrans;

        public TransactionController(ITransactionDetailRepository transRepo, IGenericRepository<Transaction> genTrans)
        {
            this.transRepo = transRepo;
            this.genTrans = genTrans;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction(Transaction data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    var trans = new Transaction();
                    trans = data;
                    await genTrans.AddAsync(trans);
                    await genTrans.SaveAsync();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
