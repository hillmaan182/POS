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
        private readonly IGenericRepository<Product> genProd;
        public ProductController(IProductRepository productRepository, IGenericRepository<Product> genProd)
        {
            this.productRepository = productRepository;
            this.genProd = genProd;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var products = productRepository.GetAll().ToList();
                return Ok(products);
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    var prod = new Product();
                    prod = data;
                    await genProd.AddAsync(prod);
                    await genProd.SaveAsync();
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