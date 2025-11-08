using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;
using Domain.DTO;
using AutoMapper;

namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository orderDetailRepository;
        private readonly IGenericRepository<OrderDetail> genOrderDetail;
        private readonly IMapper mapper;
        public OrderDetailController(IOrderDetailRepository orderDetailRepository, IGenericRepository<OrderDetail> genOrderDetail , IMapper mapper)
        {
            this.orderDetailRepository = orderDetailRepository;
            this.genOrderDetail = genOrderDetail;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            try
            {
                var orderDetails = orderDetailRepository.GetAll().ToList();
                return Ok(orderDetails);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderDetailByOrderId(long id)
        {
            try
            {
                var orderDetails = orderDetailRepository.GetOrderDetailByOrderId(id).ToList();
                return Ok(orderDetails);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpGet("DTO/{id}")]
        public async Task<IActionResult> GetOrderDetailDTOByOrderId(long id)
        {
            try
            {
                var orderDetails = orderDetailRepository.GetOrderDetailByOrderId(id).ToList();


                List<OrderDetailGetDTO> dto = mapper.Map<List<OrderDetailGetDTO>>(orderDetails);
                //dto = orderDetails;
                return Ok(dto);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

      

        [HttpPost]
        public async Task<IActionResult> CreateOrderDetail(OrderDetail data)
        {
            try
            {
                if (data == null)
                {
                    throw new InvalidOperationException("data is null");
                }
                else
                {
                    var orderDetail = new OrderDetail();
                    orderDetail = data;
                    await genOrderDetail.AddAsync(orderDetail);
                    await genOrderDetail.SaveAsync();
                    return Ok();
                }
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }
    }
}
