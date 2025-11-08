using Domain.Model;

namespace Application.Service
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task InsertOrder(Domain.DTO.OrderTempDTO data);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllByDate(DateTime? startdate, DateTime? enddate);
        Order GetOrderByCode(string code);
        Order GetOrderById(long id);
    }
}
