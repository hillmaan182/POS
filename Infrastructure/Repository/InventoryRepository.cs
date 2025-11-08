using Domain.Model;
using Application.Service;
using Domain.DataContext;

namespace Infrastructure.Repository
{
    public class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
    {
        private readonly DataContext db;
        public InventoryRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        public List<Inventory> GetAll()
        {
            return db.Inventory.ToList();
        }
    }
}
