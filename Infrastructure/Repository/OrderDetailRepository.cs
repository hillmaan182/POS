using Domain.Model;
using Application.Service;
using Domain.DataContext;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        private readonly DataContext db;
        public OrderDetailRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }
        public List<OrderDetail> GetAll()
        {
            return db.OrderDetail.ToList();
        }

        public List<OrderDetail> GetOrderDetailByOrderId(long orderid)
        {
            return db.OrderDetail.Include(x=> x.Product).Where(x=> x.orderid == orderid).ToList();
        }
    }
}
