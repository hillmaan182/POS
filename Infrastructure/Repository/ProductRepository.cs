using Domain.Model;
using Application.Service;
using Domain.DataContext;
using Domain.DTO;
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

        public Product GetById(long id)
        {
            return db.Product.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}