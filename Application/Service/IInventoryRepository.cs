using Domain.Model;

namespace Application.Service
{
    public interface IInventoryRepository : IGenericRepository<Inventory>
    {
        List<Inventory> GetAll();
    }
}
