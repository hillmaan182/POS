using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Domain.DTO
{
    public class OrderDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }
        public string? customername { get; set; }
        public string? ordercode { get; set; }
        public int? totalprice { get; set; }
        public DateTime? orderdate { get; set; }
        public ICollection<OrderDetailGetDTO> OrderDetail { get; set; } //= new List<OrderDetailGetDTO>();
    }
}
