
namespace Domain.DTO
{
    public class OrderTempDTO
    {
        public string? customername { get; set; }
        public string? ordercode { get; set; }
        public int? totalprice { get; set; }
        public DateTime? orderdate { get; set; }
        public ICollection<OrderDetailTempDTO> OrderDetail { get; set; }
    }
}
