using Domain.Model;

namespace Application.Service
{
    public interface IOrderDetailRepository : IGenericRepository<OrderDetail>
    {
        List<OrderDetail> GetAll();

        List<OrderDetail> GetOrderDetailByOrderId(long orderid);
    }
}
