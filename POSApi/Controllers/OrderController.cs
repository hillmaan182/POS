using Microsoft.AspNetCore.Mvc;
using Application.Service;
using Domain.Model;
using Domain.DTO;
using AutoMapper;
using ClosedXML.Excel;
namespace POSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IGenericRepository<Order> genOrder;
        private readonly IMapper mapper;
        public OrderController(IOrderRepository orderRepository, IGenericRepository<Order> genOrder, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.genOrder = genOrder;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                var orders = orderRepository.GetAll().ToList();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(ex.ToString());
            }

        }

        [HttpGet("{ordercode}")]
        public async Task<IActionResult> GetOrderByCode(string ordercode)
        {
            try
            {
                var order = orderRepository.GetOrderByCode(ordercode);
                return Ok(order);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost("DTO")]
        public async Task<IActionResult> CreateOrder([FromBody] Domain.DTO.OrderTempDTO dto)
        {
            // Create Order
            var order = orderRepository.InsertOrder(dto);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostParentEntity(Order data)
        {
            // EF Core automatically handles the related Children entities in the ICollection
            await genOrder.AddAsync(data);
            await genOrder.SaveAsync();

            return CreatedAtAction(nameof(GetOrderById), new { id = data.id }, data);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(long id)
        {
            var order = orderRepository.GetOrderById(id);
            return Ok(order);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpGet("DTO")]
        public IActionResult GetOrderDTOById()
        {
            var orders = orderRepository.GetAll().ToList();
            List<OrderDTO> dto = mapper.Map<List<OrderDTO>>(orders);
            return Ok(dto);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet("DTO/{startdate}/{enddate}")]
        public IActionResult GetOrderDTOByDate(string startdate, string enddate)
        {
            DateTime? start, end = DateTime.Now.Date;
            start = DateTime.Parse(startdate);
            end = DateTime.Parse(enddate);

            var orders = orderRepository.GetAllByDate(start, end).ToList();
            List<OrderDTO> dto = mapper.Map<List<OrderDTO>>(orders);
            return Ok(dto);

            if (dto == null)
            {
                return NotFound();
            }

            return Ok(dto);
        }

        [HttpGet("export-excel")]
        public IActionResult ExportExcel()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");
                var orders = orderRepository.GetAll().ToList();
                List<OrderDTO> dto = mapper.Map<List<OrderDTO>>(orders);
                int row = 1;
                int count = 0;

                var a = typeof(OrderDTO).GetProperties();

                worksheet.Cell(row, 1).Value = "Order Code";
                worksheet.Cell(row, 2).Value = "Customer";
                worksheet.Cell(row, 3).Value = "Date";
                worksheet.Cell(row, 4).Value = "Total Price";
                worksheet.Cell(row, 1).Style.Font.SetBold(true);
                worksheet.Cell(row, 2).Style.Font.SetBold(true);
                worksheet.Cell(row, 3).Style.Font.SetBold(true);
                worksheet.Cell(row, 4).Style.Font.SetBold(true);
                row++;
                foreach (var item in dto)
                {
                    worksheet.Cell(row, 1).Value = item.ordercode;
                    worksheet.Cell(row, 2).Value = item.customername;
                    worksheet.Cell(row, 3).Value = item.orderdate;
                    worksheet.Cell(row, 4).Value = item.totalprice;
                    
                    row++;

                    worksheet.Cell(row, 2).Value = "Product";
                    worksheet.Cell(row, 3).Value ="Qty";
                    worksheet.Cell(row, 4).Value = "Price Per Qty";
                    worksheet.Cell(row, 2).Style.Font.SetBold(true);
                    worksheet.Cell(row, 3).Style.Font.SetBold(true);
                    worksheet.Cell(row, 4).Style.Font.SetBold(true);

                    row++;
                    foreach (var x in dto[count].OrderDetail.ToList())
                    {
                        worksheet.Cell(row, 2).Value = x.Product.ProductName;
                        worksheet.Cell(row, 3).Value = x.qty;
                        worksheet.Cell(row, 4).Value = x.Product.ProductPrice;
                        row++;
                    }
                    count++;
                    //row++;
                }

                //worksheet.Cell(1, 1).InsertTable(orders);
                // Add headers (assuming properties of T)
                //var properties = typeof(Order).GetProperties();
                //for (int i = 0; i < properties.Length; i++)
                //{
                //    worksheet.Cell(1, i + 1).Value = properties[i].Name;
                //}

                //// Add data
                //for (int row = 0; row < orders.Count; row++)
                //{
                //    for (int col = 0; col < properties.Length; col++)
                //    {
                //        worksheet.Cell(row + 2, col + 1).Value = (XLCellValue)properties[col].GetValue(orders[row]);
                //    }
                //}

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderExcel.xlsx");
                    //return File(stream, contentType, "OrderExcel.xlsx");
                }
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(Order data)
        //{
        //    try
        //    {
        //        if (data == null)
        //        {
        //            throw new InvalidOperationException("data is null");
        //        }
        //        else
        //        {
        //            var order = new Order();
        //            order = data;
        //            await genOrder.AddAsync(order);
        //            await genOrder.SaveAsync();
        //            return Ok();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }

        //}
    }
}
