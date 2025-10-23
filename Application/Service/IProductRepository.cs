using Domain.Model;

namespace Application.Service
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        List<Product> GetAll();
        
    }
}