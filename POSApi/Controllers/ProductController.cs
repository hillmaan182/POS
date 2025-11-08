using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;
using Microsoft.AspNetCore.Authorization;

namespace POSApi.Controllers
{
    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            try
            {
                var products = productRepository.GetById(id);
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

        [HttpPut]
        public async Task<IActionResult> UpdsateProduct(Product data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    await genProd.UpdateAsync(data);
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