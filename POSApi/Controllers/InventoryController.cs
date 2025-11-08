using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryRepository inventoryRepo;
        private readonly IGenericRepository<Inventory> genInventory;

        public InventoryController(IInventoryRepository inventoryRepo, IGenericRepository<Inventory> genInventory)
        {
            this.inventoryRepo = inventoryRepo;
            this.genInventory = genInventory;
        }

        [HttpGet]
        public async Task<IActionResult> GetInventory()
        {
            try
            {
                var inventory = inventoryRepo.GetAll();
                return Ok(inventory);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateInventory(Inventory data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    var inventory = new Inventory();
                    inventory = data;
                    await genInventory.AddAsync(inventory);
                    await genInventory.SaveAsync();
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
