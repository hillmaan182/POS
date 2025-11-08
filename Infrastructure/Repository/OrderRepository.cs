using Domain.Model;
using Application.Service;
using Domain.DataContext;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext db;
        public OrderRepository(DataContext _db) : base(_db)
        {
            this.db = _db;
        }

        public async Task InsertOrder(Domain.DTO.OrderTempDTO data)
        {
            var order = new Order
            {
                customername = data.customername,
                ordercode = data.ordercode,
                orderdate = data.orderdate,
                totalprice = data.totalprice,
                OrderDetail = data.OrderDetail.Select(x => new OrderDetail
                {
                    qty = x.qty,
                    productid = x.productid
                }).ToList()
            };

            db.Order.Add(order);
            db.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            //var query = from q in db.Order
            //            join x in db.OrderDetail on q.id equals x.orderid
            //            select q;

            //return query.ToList();

            return db.Order.Include(x => x.OrderDetail).ThenInclude(x => x.Product).ToList();

            //return db.Order.ToList();
        }

        public IEnumerable<Order> GetAllByDate(DateTime? startdate, DateTime? enddate)
        {
            return db.Order.Include(x => x.OrderDetail).ThenInclude(x => x.Product).Where(x=> x.orderdate >= startdate && x.orderdate <= enddate).ToList();
        }

        public Order GetOrderByCode(string code)
        {
            return db.Order.Where(x => x.ordercode == code).FirstOrDefault();
        }
        public Order GetOrderById(long id)
        {
            return db.Order.Where(x => x.id == id).FirstOrDefault();
        }


    }
}
