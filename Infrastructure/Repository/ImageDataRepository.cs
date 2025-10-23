using Domain.Model;
using Application.Service;
using Domain.DataContext;
namespace Infrastructure.Repository
{
    
    public class ImageDataRepository : GenericRepository<ImageData>, IImageDataRepository
    {
        private readonly DataContext db;
        public ImageDataRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        //public List<Product> GetAll()
        //{
        //    return db.Product.ToList();
        //}
    }
}
