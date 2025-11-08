using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Model
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string? customername { get; set; }
        public string? ordercode { get; set; }
        public int? totalprice { get; set; }
        public DateTime? orderdate { get; set; }
        public ICollection<OrderDetail>  OrderDetail { get; set; } = new List<OrderDetail>();
    }
}
