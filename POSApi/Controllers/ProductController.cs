using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;
namespace POSApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = productRepository.GetAll().ToList();
            return Ok(products);
        }
    }
}