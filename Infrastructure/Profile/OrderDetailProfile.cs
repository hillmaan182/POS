using AutoMapper;
using Domain.DTO;
using Domain.Model;
namespace Infrastructure.Profile
{
    public class OrderDetailProfile : AutoMapper.Profile
    {
        public OrderDetailProfile()
        {
            CreateMap<OrderDetail, OrderDetailGetDTO>();
            CreateMap<Order, OrderDTO>();
            //CreateMap<List<OrderDetail>, List<OrderDetailGetDTO>>();
        }
    }
}
