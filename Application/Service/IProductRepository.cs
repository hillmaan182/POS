using Domain.Model;
using Domain.DTO;
namespace Application.Service
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetAll();
        Product GetById(long id);
        //Task CreateProduct(ProductDTO data);
        
    }
}