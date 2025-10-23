using Domain.Model;
using Application.Service;
using Domain.DataContext;

namespace Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext db;
        public ProductRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        public List<Product> GetAll()
        {
            return db.Product.ToList();
        }
       
    }
}